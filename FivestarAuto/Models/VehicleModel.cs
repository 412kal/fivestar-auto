using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FivestarAuto.Models
{
    public class VehicleModel
    {

        //Notes: In a completed system, I would likely have this model tweaked
        //to fit within whatever data access structure we're using. As this is
        //just a stub site however, I'm using it as a neater way to access the
        //hypothetical "Vehicles" table.
        public int ID { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

        public decimal RetailPrice { get; set; }

        public VehicleType Type { get; set; }

        public string TypeText
        {
            get
            {
                return Enum.GetName(typeof(VehicleType), Type);
            }
        }


        public enum VehicleType { Car, Truck, SUV }
    }
}