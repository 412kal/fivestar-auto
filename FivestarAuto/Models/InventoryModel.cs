
using FivestarAuto.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;


namespace FivestarAuto.Models
{
    public class InventoryModel
    {
        public int Year { get; set; }

        [Display(Name ="Stock #")]
        public string StockNumber { get; set; }

        [Display(Name ="Quantity in Stock")]
        public int QuantityInStock { get; set; }

        public List<int> Features { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        [Display(Name ="Type")]
        public string TypeText { get; set; }

        [Display(Name ="Retail Price")]
        public decimal RetailPrice { get; set; }


        public string FeaturesText
        {
            get;set;
        }


        public InventoryModel()
        {
            Year = DateTime.Now.Year;
            StockNumber = "";
            //todo: verify no collision on stock number
            QuantityInStock = 0;
            Features = new List<int>();
        }

        public InventoryModel(InventoryRecord record)
        {
            Year = record.Year;
            StockNumber = record.StockNumber;
            QuantityInStock = record.QuantityInStock;
            Features = new List<int>();
            foreach(var f in record.Features)
            {
                Features.Add(f.ID);
            }
            FeaturesText = record.FeaturesText;
            Make = record.VehicleRecord.Make;
            Model = record.VehicleRecord.Model;
            TypeText = record.VehicleRecord.TypeText;
            RetailPrice = record.CalcSalePrice;

        }


    }


}