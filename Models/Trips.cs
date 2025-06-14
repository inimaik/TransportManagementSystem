using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Trips
    {
        public int TripID { get; set; }
        public int VehicleID { get; set; }
        public int RouteID { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Status { get; set; } // Scheduled, In Progress, Completed, Cancelled
        public string TripType { get; set; } = "Freight"; // Freight, Passenger
        public int? MaxPassengers { get; set; } // Nullable for freight trips
    }
}
