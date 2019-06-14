using FivestarAuto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static FivestarAuto.Data.FeatureRecord;
using static FivestarAuto.Models.VehicleModel;

namespace FivestarAuto.Data
{
    public static class DataDriver
    {

        public static List<VehicleModel> VehicleData = new List<VehicleModel>{
            new VehicleModel { ID=1, Type = VehicleType.Car, Make = "Ford", Model = "Focus", RetailPrice=(decimal)16500.00},
            new VehicleModel { ID=2, Type = VehicleType.Car, Make = "Ford", Model = "Fusion", RetailPrice=(decimal)22000.00},
            new VehicleModel { ID=3, Type = VehicleType.Truck, Make = "Ford", Model = "F-150", RetailPrice=(decimal)24500.00},
            new VehicleModel { ID=4, Type = VehicleType.Car, Make = "Lincoln", Model = "MKZ", RetailPrice=(decimal)34500.00},
            new VehicleModel { ID=5, Type = VehicleType.SUV, Make = "Lincoln", Model = "Navigator", RetailPrice=(decimal)56000.00},
            new VehicleModel { ID=6, Type = VehicleType.Car, Make = "Dodge", Model = "Avenger", RetailPrice=(decimal)20500.00},
            new VehicleModel { ID=7, Type = VehicleType.Car, Make = "Dodge", Model = "Dart", RetailPrice=(decimal)16000.00},
            new VehicleModel { ID=8, Type = VehicleType.SUV, Make = "Dodge", Model = "Durango", RetailPrice=(decimal)29500.00},

        };

        public static List<FeatureRecord> FeaturesData = new List<FeatureRecord>
        {
            new FeatureRecord { ID=1, Type = FeatureType.Doors, Description = "2-door", RetailPrice=(decimal)0.00},
            new FeatureRecord { ID=2, Type = FeatureType.Doors, Description = "4-door", RetailPrice=(decimal)2500.00},
            new FeatureRecord { ID=3, Type = FeatureType.Fuel, Description = "Gas", RetailPrice=(decimal)0.00},
            new FeatureRecord { ID=4, Type = FeatureType.Fuel, Description = "Hybrid", RetailPrice=(decimal)10000.00},
            new FeatureRecord { ID=5, Type = FeatureType.Fuel, Description = "Electric", RetailPrice=(decimal)15000.00},
            new FeatureRecord { ID=6, Type = FeatureType.Transmission, Description = "Automatic", RetailPrice=(decimal)1000.00},
            new FeatureRecord { ID=7, Type = FeatureType.Transmission, Description = "Manual", RetailPrice=(decimal)0.00},
            new FeatureRecord { ID=8, Type = FeatureType.Interior, Description = "Cloth", RetailPrice=(decimal)0.00},
        };

        public static List<InventoryRecord> InventoryRecords = new List<InventoryRecord>
        {
            new InventoryRecord(2016, 1, 1, new List<int>{2, 3, 7, 8}),
            new InventoryRecord(2017, 6, 1, new List<int>{1, 3, 6, 8 }),
            new InventoryRecord(2016, 8, 3, new List<int>{2, 3, 7, 8 }),
            new InventoryRecord(2018, 2, 2, new List<int>{2, 5, 6, 8 }),
            new InventoryRecord(2016, 1, 0, new List<int>{2, 4, 6, 8 }),
            new InventoryRecord(2017, 3, 1, new List<int>{1, 3, 7, 8 }),
            new InventoryRecord(2016, 4, 1, new List<int>{2, 4, 6, 8 }),
            new InventoryRecord(2018, 5, 3, new List<int>{2, 3, 6, 8 }),
            new InventoryRecord(2016, 7, 4, new List<int>{1, 3, 7, 8 }),
            new InventoryRecord(2018, 1, 2, new List<int>{2, 3, 6, 8 }),
            new InventoryRecord(2017, 2, 0, new List<int>{2, 4, 6, 8 })
        };



    }
}