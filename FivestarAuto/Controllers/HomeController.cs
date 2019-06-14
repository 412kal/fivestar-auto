using FivestarAuto.Data;
using FivestarAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FivestarAuto.Models.VehicleModel;

namespace FivestarAuto.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult OwnerView()
        {
            //parse "db" objects to models
            if (Session["CurrentInventory"] == null)
            {
                List<InventoryModel> modelList = new List<InventoryModel>();
                foreach (var x in DataDriver.InventoryRecords)
                {
                    modelList.Add(new InventoryModel(x));
                }

                Session["CurrentInventory"] = modelList;
            }

            var vehicleTypes = (Enum.GetValues(typeof(VehicleType)).OfType<Enum>()
              .Select(x =>
                    new SelectListItem
                    {
                        Text = Enum.GetName(typeof(VehicleType), x),
                        Value = (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text");

            ViewBag.VehicleTypes = vehicleTypes;

            return View();
        }

        public void SellStock(string stockNo)
        {
            if(!string.IsNullOrEmpty(stockNo))
            {
                InventoryRecord record = DataDriver.InventoryRecords.Where(m => m.StockNumber == stockNo).FirstOrDefault();
                if (record != null)
                {
                    record.QuantityInStock--;
                    record.Save();

                    List<InventoryModel> modelList = new List<InventoryModel>();
                    foreach (var x in DataDriver.InventoryRecords)
                    {
                        modelList.Add(new InventoryModel(x));
                    }

                    Session["CurrentInventory"] = modelList;
                }
            }
        }

        public JsonResult GetInventory()
        {
            var inv = Session["CurrentInventory"] as List<InventoryModel>;
            var result = Json(inv.OrderBy(m => m.Make).ThenBy(m => m.Model), "application/json", System.Text.Encoding.UTF8);

            return Json(inv, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManagerView()
        {
            //parse "db" objects to models
            //in a more persistent version, we would check
            //session first to see if data already exists
            if (Session["CurrentInventory"] == null)
            {
                List<InventoryModel> modelList = new List<InventoryModel>();
                foreach (var x in DataDriver.InventoryRecords)
                {
                    modelList.Add(new InventoryModel(x));
                }

                Session["CurrentInventory"] = modelList;
            }

            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var m in DataDriver.VehicleData.Select(o => o.Make).Distinct())
            {
                li.Add(new SelectListItem { Text = m, Value = m });
            }

            List<SelectListItem> fr = new List<SelectListItem>();
            foreach(var r in DataDriver.FeaturesData)
            {
                fr.Add(new SelectListItem { Text = r.Description, Value = r.ID + "|" + r.Type });
            }

            ViewBag.Features = fr;
            ViewBag.VehicleMakes = li;
            return View();
        }

        public ActionResult EditRecord(string stockNo)
        {

            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var m in DataDriver.VehicleData.Select(o => o.Make).Distinct())
            {
                li.Add(new SelectListItem { Text = m, Value = m });
            }
            ViewBag.VehicleMakes = li;
            
            //Normally we would pull this from the database by a unique ID
            InventoryModel model = new InventoryModel(); //catch the "create new" case
            if (!string.IsNullOrEmpty(stockNo))
            {
                var invRecord = DataDriver.InventoryRecords.Where(m => m.StockNumber == stockNo).FirstOrDefault();

                model = new InventoryModel(invRecord);
            }

            List<SelectListItem> fr = new List<SelectListItem>();
            foreach (var r in DataDriver.FeaturesData)
            {
                fr.Add(new SelectListItem { Text = r.Description, Value = r.ID + "|" + r.Type, Selected = model.Features.Contains(r.ID) });
            }

            ViewBag.Features = fr;
            return PartialView("_EditRecord", model);
        }

        public ActionResult SaveRecord(InventoryModel inv, string[] Feature)
        {
            //find vehicle ID
            //if fully db-driven, the model would return unique make and model IDs
            var modelId = DataDriver.VehicleData.Where(m => m.Make == inv.Make && m.Model == inv.Model).Select(m => m.ID).FirstOrDefault();

            
            foreach(var feat in Feature)
            {
                if(!string.IsNullOrEmpty(feat))
                {
                    var strSplit = feat.Split('|');
                    inv.Features.Add(Convert.ToInt32(strSplit[0]));
                }
            }

            InventoryRecord record = DataDriver.InventoryRecords.Where(m => m.StockNumber == inv.StockNumber).FirstOrDefault();
            if(record == null)
                record = new InventoryRecord();

            record.Year = inv.Year;
            record.QuantityInStock = inv.QuantityInStock;
            record.AddFeatures(inv.Features);
            record.SetVehicle(inv.Make, inv.Model);
            record.Save();


            List<InventoryModel> modelList = new List<InventoryModel>();
            foreach (var x in DataDriver.InventoryRecords)
            {
                modelList.Add(new InventoryModel(x));
            }

            Session["CurrentInventory"] = modelList;
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (var m in DataDriver.VehicleData.Select(o => o.Make).Distinct())
            {
                li.Add(new SelectListItem { Text = m, Value = m });
            }

            ViewBag.VehicleMakes = li;

            return RedirectToAction("ManagerView");
        }


        public JsonResult LoadModels(string make)
        {
            List<SelectListItem> models = new List<SelectListItem>();
            foreach (var m in DataDriver.VehicleData.Where(n => n.Make == make).Select(o => o.Model))
            {
                models.Add(new SelectListItem { Text = m, Value = m });
            }

            return Json(new SelectList(models, "Value", "Text"));
        }
    }
}