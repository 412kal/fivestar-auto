using FivestarAuto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FivestarAuto.Data
{
    public class InventoryRecord
    {
        public int Year { get; set; }

        public string StockNumber { get; set; }

        public int QuantityInStock { get; set; }

        public List<FeatureRecord> Features { get; set; }


        public VehicleModel VehicleRecord { get; set; }


        public string FeaturesText
        {
            get
            {
                return string.Join(", ", Features.Select(o => o.Description));
            }
        }

        public decimal CalcSalePrice
        {
            get
            {
                decimal salesPrice = VehicleRecord.RetailPrice;
                foreach (var f in Features)
                {
                    salesPrice += f.RetailPrice;
                }

                return salesPrice;
            }
        }

        public InventoryRecord()
        {
            Year = DateTime.Now.Year;
            StockNumber = GetRandomStockNumber(10);
            //todo: verify no collision on stock number
            QuantityInStock = 0;
            Features = new List<FeatureRecord>();
        }

        public InventoryRecord(int year, int vehicleId, int? quantityInStock)
        {
            Year = year;
            StockNumber = GetRandomStockNumber(10);
            //todo: verify no collision on stock number
            QuantityInStock = quantityInStock ?? 0;
            VehicleRecord = DataDriver.VehicleData.Where(p => p.ID == vehicleId).FirstOrDefault();
            Features = new List<FeatureRecord>();
        }

        public InventoryRecord(int year, int vehicleId, int? quantityInStock, List<int> featureIds)
        {
            Year = year;
            StockNumber = GetRandomStockNumber(10);
            //todo: verify no collision on stock number
            QuantityInStock = quantityInStock ?? 0;
            VehicleRecord = DataDriver.VehicleData.Where(p => p.ID == vehicleId).FirstOrDefault();
            AddFeatures(featureIds);
        }

        public void AddFeatures(List<int> featureIds)
        {
            Features = new List<FeatureRecord>();
            foreach (var i in featureIds)
            {
                Features.Add(DataDriver.FeaturesData.Where(p => p.ID == i).FirstOrDefault());
            }
        }

        public void SetVehicle(string make, string model)
        {
            VehicleRecord = DataDriver.VehicleData.Where(p => p.Make == make && p.Model == model).FirstOrDefault();
        }

        private static Random r = new Random();
        public static string GetRandomStockNumber(int length)
        {
            const string characterSet = "ABCDEFGHIJKLMNOP0123456789";
            return new string(Enumerable.Repeat(characterSet, length)
                .Select(s => s[r.Next(s.Length)]).ToArray());

        }

        public void Save()
        {
            //In a fully db-driven system, this would send our records to the database
            //To account for DB weirdness, there would also be error handling
            DataDriver.InventoryRecords.Add(this);
        }

    }

    public class FeatureRecord
    {
        public enum FeatureType { Doors, Fuel, Transmission, Interior };

        public int ID { get; set; }

        public FeatureType Type { get; set; }

        public string Description { get; set; }

        public decimal RetailPrice { get; set; }
    }
}