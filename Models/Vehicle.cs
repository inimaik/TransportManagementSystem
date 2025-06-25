using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string Model { get; set; }
        public decimal Capacity { get; set; }
        public string Type { get; set; } // Truck, Van, Bus
        public string Status { get; set; } // Available, On Trip, Maintenance
    }
}
