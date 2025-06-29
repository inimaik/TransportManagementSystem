using MyExceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Dao
{
    public class TransportManagementServiceImpl : ITransportManagementService
    {
        private readonly string connectionString;

        public TransportManagementServiceImpl()
        {
            connectionString = DBPropertyUtil.GetConnectionString();
        }

        #region AddVehicle
        public bool AddVehicle(Vehicle vehicle)
        {
            using (SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Vehicles VALUES (@Model,@Capacity,@Type,@status)", con);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Capacity", vehicle.Capacity);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@Status", vehicle.Status);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region UpdateVehicle
        public bool UpdateVehicle(Vehicle vehicle)
        {
            using(SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Vehicles SET Type = @Type, Model=@Model, Capacity = @Capacity, Status=@status WHERE VehicleId = @VehicleId", con);
                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleID);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@Capacity", vehicle.Capacity);
                cmd.Parameters.AddWithValue("@Status", vehicle.Status);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new VehicleNotFoundException("Vehicle not found with ID: " + vehicle.VehicleID);
                }
                return true;
            }
        }
        #endregion

        #region DeleteVehicle
        public bool DeleteVehicle(int vehicleId)
        {
            using (SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Vehicles WHERE VehicleId = @VehicleId", con);
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new VehicleNotFoundException("Vehicle not found with ID: " + vehicleId);
                }
                return true;
            }
        }
        #endregion

        #region ScheduleTrip
        public bool ScheduleTrip(int vehicleId, int routeId, string departureDate, string arrivalDate)
        {
            using (SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                //Check if vehicle exsists
                SqlCommand checkVehicleCmd = new SqlCommand("SELECT Status FROM Vehicles WHERE VehicleID = @VehicleID", con);
                checkVehicleCmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                object vehicleStatus = checkVehicleCmd.ExecuteScalar();
                if (vehicleStatus == null || vehicleStatus.ToString() != "Available")
                {
                    throw new VehicleNotFoundException("Vehicle not available or does not exist.");
                }
                // Check if route exists
                SqlCommand checkRouteCmd = new SqlCommand("SELECT COUNT(*) FROM Routes WHERE RouteID = @RouteID", con);
                checkRouteCmd.Parameters.AddWithValue("@RouteID", routeId);
                int routeExists = (int)checkRouteCmd.ExecuteScalar();
                if (routeExists == 0)
                {
                    throw new RouteNotFoundException("Route does not exist.");
                }
                //Schedule trip after checking...Max passengers is 0..try to handle it at last
                string query = @"INSERT INTO Trips (VehicleID, RouteID, DepartureDate, ArrivalDate, Status, MaxPassengers)
                         VALUES (@VehicleID, @RouteID, @DepartureDate, @ArrivalDate, 'Scheduled', 0)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                cmd.Parameters.AddWithValue("@RouteID", routeId);
                cmd.Parameters.AddWithValue("@DepartureDate", DateTime.Parse(departureDate));
                cmd.Parameters.AddWithValue("@ArrivalDate", DateTime.Parse(arrivalDate));
                int rowsAffected = cmd.ExecuteNonQuery();
                //update vehicle status to 'On Trip'
                string updateVehicleStatusQuery = "UPDATE Vehicles SET Status = 'On Trip' WHERE VehicleID = @VehicleID";
                SqlCommand updateVehicleCmd = new SqlCommand(updateVehicleStatusQuery, con);
                updateVehicleCmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                int vehicleUpdated=updateVehicleCmd.ExecuteNonQuery();
                return rowsAffected > 0 && vehicleUpdated>0;
            }
        }
        #endregion

        #region CancelTrip
        public bool CancelTrip(int tripId) //Not completed
        {
            using (SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Trips SET Status = 'Cancelled' WHERE TripID = @TripID", con);
                cmd.Parameters.AddWithValue("@TripID", tripId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new TripNotFoundException("Trip not found with ID: " + tripId);
                }

                return true;
            }
        }
        #endregion

        #region BookTrip
        public bool BookTrip(int tripId, int passengerId, string bookingDate)
        {
            using (SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                //Check if trip id exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Trips WHERE TripID = @TripID AND Status = 'Scheduled'", con);
                checkCmd.Parameters.AddWithValue("@TripID", tripId);
                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0) { 
                throw new TripNotFoundException("Trip not found or is cancelled: " + tripId);
                }
                //check if passenger exists
                SqlCommand checkCmdPassenger = new SqlCommand("SELECT COUNT(*) FROM passengers WHERE PassengerId = @PassengerId", con);
                checkCmdPassenger.Parameters.AddWithValue("@PassengerId", passengerId);
                int countPassenger = (int)checkCmdPassenger.ExecuteScalar();
                if (countPassenger == 0)
                {
                    throw new PassengerNotFoundException("Passenger not found " + passengerId);
                }
                //insert record
                string insertQuery = @"INSERT INTO Bookings (TripID, PassengerID, BookingDate, Status)
                               VALUES (@TripID, @PassengerID, @BookingDate, 'Confirmed')";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@TripID", tripId);
                insertCmd.Parameters.AddWithValue("@PassengerID", passengerId);
                insertCmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(bookingDate));

                int rowsAffected = insertCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region CancelBooking
        public bool CancelBooking(int bookingId)
        {
            using (SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Bookings SET Status = 'Cancelled' WHERE BookingID = @BookingID AND Status != 'Cancelled'", con);
                cmd.Parameters.AddWithValue("@BookingID", bookingId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new BookingNotFoundException("Booking not found with ID: " + bookingId);
                }

                return true;
            }
        }
        #endregion

        #region AllocateDriver
        public bool AllocateDriver(int tripId, int driverId)
        {
            using (var context = new TransportContext())
            {
                var trip = context.Trips.SingleOrDefault(t => t.TripID == tripId);
                if (trip == null)
                    throw new TripNotFoundException("Trip is not found: " + tripId);

                var driver = context.Drivers.SingleOrDefault(d => d.DriverId == driverId);
                if (driver == null)
                    throw new DriverNotFoundException("Driver is not found: " + driverId);

                

                var conflictingTrip = context.Trips
                                             .Where(t => t.DriverId == driverId && t.TripID != tripId)
                                             .Any(t =>
                                                      t.DepartureDate < trip.ArrivalDate &&
                                                      t.ArrivalDate > trip.DepartureDate);

                if (trip.Status != "Scheduled"
                    || trip.DriverId != null
                    || driver.Status != "Available"
                    || conflictingTrip)
                    throw new InvalidOperationException("Verify the Trip and Driver status");

                trip.DriverId = driverId;
                driver.Status = "On Trip";

                context.SaveChanges();
                return true;
            }
        }
        #endregion

        #region DeallocateDriver
        public bool DeallocateDriver(int tripId)
        {
            using (var context = new TransportContext())
            {
                var trip = context.Trips.SingleOrDefault(t => t.TripID == tripId);
                if (trip == null)
                    throw new TripNotFoundException("Trip is not found: " + tripId);

                if (!trip.DriverId.HasValue)
                    throw new InvalidOperationException("No driver is currently allocated to this trip.");

                var driver = context.Drivers.SingleOrDefault(d => d.DriverId == trip.DriverId.Value);
                if (driver == null)
                    throw new DriverNotFoundException("Driver not found for the allocated DriverId: " + trip.DriverId.Value);

                trip.DriverId = null;
                driver.Status = "Available";

                context.SaveChanges();
                return true;
            }
        }
            
        #endregion

        #region DisplayBookingsByPassenger
        public List<Booking> GetBookingsByPassenger(int passengerId)
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                //check if passenger exists
                string checkPassengerQuery = "SELECT COUNT(*) FROM Passengers WHERE PassengerID = @PassengerId";
                SqlCommand checkCmd = new SqlCommand(checkPassengerQuery, con);
                checkCmd.Parameters.AddWithValue("@PassengerId", passengerId);

                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                {
                    throw new PassengerNotFoundException("Passenger not found with ID: " + passengerId);
                }

                string query = "SELECT BookingID, TripID, PassengerID, BookingDate, Status FROM Bookings WHERE PassengerID = @PassengerId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PassengerId", passengerId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingID = reader.GetInt32(0),
                            TripID = reader.GetInt32(1),
                            PassengerID = reader.GetInt32(2),
                            BookingDate = reader.GetDateTime(3),
                            Status = reader.GetString(4)
                        };
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }
        #endregion

        #region DisplayBookingsByTrip
        public List<Booking> GetBookingsByTrip(int tripId)//Not Completed
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection con = DBConnUtil.GetDbConnection(connectionString))
            {
                con.Open();
                //check if tripid exists
                string checkTripQuery = "SELECT COUNT(*) FROM Trips WHERE TripID = @TripId";
                SqlCommand checkCmd = new SqlCommand(checkTripQuery, con);
                checkCmd.Parameters.AddWithValue("@TripId", tripId);

                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                {
                    throw new TripNotFoundException("Trip not found with ID: " + tripId);
                }
                
                SqlCommand cmd = new SqlCommand("SELECT * FROM Bookings WHERE TripId = @TripId", con);
                cmd.Parameters.AddWithValue("@TripId", tripId);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingID = reader.GetInt32(0),
                            TripID = reader.GetInt32(1),
                            PassengerID = reader.GetInt32(2),
                            BookingDate = reader.GetDateTime(3),
                            Status = reader.GetString(4)
                        };
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }
        #endregion

        #region DisplayAvailableDrivers
        public List<Driver> GetAvailableDrivers()
        {
         
                using (var context = new TransportContext())
                {
                    return context.Drivers
                                  .Where(d => d.Status == "Available")
                                  .ToList();
                }
        } 
        #endregion
    }
}
