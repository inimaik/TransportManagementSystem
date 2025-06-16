using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Dao
{
    public interface ITransportManagementService
    {
        bool AddVehicle(Vehicles vehicle);
        bool UpdateVehicle(Vehicles vehicle);
        bool DeleteVehicle(int vehicleId);
        bool ScheduleTrip(int vehicleId, int routeId, string departureDate, string arrivalDate);
        bool CancelTrip(int tripId);
        bool BookTrip(int tripId, int passengerId, string bookingDate);
        bool CancelBooking(int bookingId);
        bool AllocateDriver(int tripId, int driverId);
        bool DeallocateDriver(int tripId);
        List<Bookings> GetBookingsByPassenger(int passengerId);
        List<Bookings> GetBookingsByTrip(int tripId);
        List<Drivers> GetAvailableDrivers();

    }
}


