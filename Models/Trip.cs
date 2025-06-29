using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Trip
    {
        public int TripID { get; set; }
        public int VehicleID { get; set; }
        public int RouteID { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Status { get; set; } // Scheduled, In Progress, Completed, Cancelled
        public string TripType { get; set; } = "Freight"; // Freight, Passenger
        public int MaxPassengers { get; set; }
        
        //Drivers table is added using EF approach
        //foreign key added using EF approach after driver table creation
        [ForeignKey("Drivers")]
        public int? DriverId { get; set; }
        public Driver Drivers { get; set; }
        

    }
}
