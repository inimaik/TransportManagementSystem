using Exceptions;
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
        private SqlConnection con;
        public TransportManagementServiceImpl() {
            string connectionString = DBPropertyUtil.GetConnectionString();
            con = DBConnUtil.GetDbConnection(connectionString);
        }

        #region AddVehicle
        public bool AddVehicle(Vehicle vehicle)
        {
            try
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
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region UpdateVehicle
        public bool UpdateVehicle(Vehicle vehicle)
        {
            try
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
                    throw new VechileNotFoundException("Vehicle not found with ID: " + vehicle.VehicleID);
                }
                return rowsAffected > 0;
            }
            //catch (SqlException ex)
            //{
            //    Console.WriteLine("Database error: " + ex.Message);
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //    return false;
            //}
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region DeleteVehicle
        public bool DeleteVehicle(int vehicleId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Vehicles WHERE VehicleId = @VehicleId", con);
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new VechileNotFoundException("Vehicle not found with ID: " + vehicleId);
                }
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region ScheduleTrip
        public bool ScheduleTrip(int vehicleId, int routeId, string departureDate, string arrivalDate) //Not completed
        {
            try
            {
                con.Open();
                SqlCommand checkCmd = new SqlCommand("SELECT Status FROM Vehicles WHERE VehicleID = @VehicleID", con);
                checkCmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                object status = checkCmd.ExecuteScalar();

                if (status == null || status.ToString() != "Available")
                {
                    throw new VechileNotFoundException("Vehicle not available or does not exist.");
                }
                string query = @"INSERT INTO Trips (VehicleID, RouteID, DepartureDate, ArrivalDate, Status, MaxPassengers)
                         VALUES (@VehicleID, @RouteID, @DepartureDate, @ArrivalDate, 'Scheduled', 0)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                cmd.Parameters.AddWithValue("@RouteID", routeId);
                cmd.Parameters.AddWithValue("@DepartureDate", DateTime.Parse(departureDate));
                cmd.Parameters.AddWithValue("@ArrivalDate", DateTime.Parse(arrivalDate));

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error in scheduleTrip: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in scheduleTrip: " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region CancelTrip
        public bool CancelTrip(int tripId) //Not completed
        {
            try
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
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region BookTrip
        public bool BookTrip(int tripId, int passengerId, string bookingDate)//Not completed
        {
            try
            {
                con.Open();

                string checkQuery = "SELECT COUNT(*) FROM Trips WHERE TripID = @TripID AND Status != 'Cancelled'";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@TripID", tripId);

                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                    throw new TripNotFoundException("Trip not found or is cancelled: " + tripId);

                string insertQuery = @"INSERT INTO Bookings (TripID, PassengerID, BookingDate, Status)
                               VALUES (@TripID, @PassengerID, @BookingDate, 'Confirmed')";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@TripID", tripId);
                insertCmd.Parameters.AddWithValue("@PassengerID", passengerId);
                insertCmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(bookingDate));

                int rowsAffected = insertCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error in bookTrip: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in bookTrip: " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region CancelBooking
        public bool CancelBooking(int bookingId)//Not completed
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Bookings SET Status = 'Cancelled' WHERE BookingID = @BookingID", con);
                cmd.Parameters.AddWithValue("@BookingID", bookingId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new BookingNotFoundException("Booking not found with ID: " + bookingId);
                }

                return true;
            }
            //catch (SqlException ex)
            //{
            //    Console.WriteLine("Database error: " + ex.Message);
            //    return false;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //    return false;
            //}
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region AllocateDriver
        public bool AllocateDriver(int tripId, int driverId)
        {
            try
            {
                using (var context = new TransportContext())
                {
                    var trip = context.Trips.SingleOrDefault(t => t.TripID == tripId);
                    if (trip == null)
                        throw new TripNotFoundException("Trip is not found: " + tripId);

                    var driver = context.Drivers.SingleOrDefault(d => d.DriverId == driverId);
                    if (driver == null)
                        throw new DriverNotFoundException("Driver is not found: " + driverId);

                    if (driver.Status != "Available" || trip.DriverId != null)
                        return false;
                    var conflictingTrip = context.Trips
                                                 .Where(t => t.DriverId == driverId && t.TripID != tripId)
                                                 .Any(t =>
                                                          t.DepartureDate < trip.ArrivalDate &&
                                                          t.ArrivalDate > trip.DepartureDate
                );

                    if (conflictingTrip)
                        return false;

                    trip.DriverId = driverId;
                    driver.Status = "On Trip";

                    context.SaveChanges();
                    return true;
                }
            }
            finally
            {
                Console.WriteLine("Driver block has completed running");
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error during driver allocation: " + ex.Message);
            //    return false;
            //}
        }
        #endregion

        #region DeallocateDriver
        public bool DeallocateDriver(int tripId)
        {
            try
            {
                using (var context = new TransportContext())
                {
                    var trip = context.Trips.SingleOrDefault(t => t.TripID == tripId);
                    if (trip == null)
                        throw new TripNotFoundException($"Trip not found: {tripId}");

                    if (trip.DriverId == null)
                        return false; // No driver to deallocate

                    var driver = context.Drivers.SingleOrDefault(d => d.DriverId == trip.DriverId);
                    if (driver == null)
                        throw new DriverNotFoundException($"Driver not found: {trip.DriverId}");

                    trip.DriverId = null;
                    driver.Status = "Available";

                    context.SaveChanges();
                    return true;
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error during driver deallocation: " + ex.Message);
            //    return false;
            //}
            finally
            {
                Console.WriteLine("Deallocate block has run");
            }
        }
        #endregion

        #region DisplayBookingsByPassenger
        public List<Booking> GetBookingsByPassenger(int passengerId) //Not completed
        {
            List<Booking> bookings = new List<Booking>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Bookings", con);

                //cmd.Parameters.AddWithValue("@PassengerId", passengerId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Booking booking = new Booking
                    {
                        BookingID = reader.GetInt32(0),
                        TripID = reader.GetInt32(1),
                        PassengerID = reader.GetInt32(2),
                        BookingDate = reader.GetDateTime(3),// date format must be handled properly
                        Status = reader.GetString(4)
                    };
                    bookings.Add(booking);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return bookings;
        }
        #endregion

        #region DisplayBookingsByTrip
        public List<Booking> GetBookingsByTrip(int tripId)//Not Completed
        {
            List<Booking> bookings = new List<Booking>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Bookings WHERE TripId = @TripId", con);
                cmd.Parameters.AddWithValue("@TripId", tripId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Booking booking = new Booking
                    {
                        BookingID = reader.GetInt32(0),
                        TripID = reader.GetInt32(1),
                        PassengerID = reader.GetInt32(2),
                        BookingDate = reader.GetDateTime(3)
                    };
                    bookings.Add(booking);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return bookings;
        }
        #endregion

        #region DisplayAvailableDrivers
        public List<Driver> GetAvailableDrivers()
        {
            try
            {
                using (var context = new TransportContext())
                {
                    return context.Drivers
                                  .Where(d => d.Status == "Available")
                                  .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving available drivers: " + ex.Message);
                return new List<Driver>(); // return empty list on failure
            }
        } 
        #endregion
    }
}
