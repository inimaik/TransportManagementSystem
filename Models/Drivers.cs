using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Drivers
    {
        public int DriverID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int TripTypeID { get; set; } // Foreign key to TripTypes
        public string Status { get; set; }  // Available, On Trip, Inactive
    }
}
