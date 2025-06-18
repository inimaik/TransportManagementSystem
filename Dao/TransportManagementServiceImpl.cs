using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Models;

namespace Dao
{
    public class TransportManagementServiceImpl : ITransportManagementService
    {
        private static string connectionString = DBPropertyUtil.GetConnectionString();
        SqlConnection con=DBConnUtil.GetDbConnection(connectionString);
        public bool AddVehicle(Vehicles vehicle)
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
        public bool UpdateVehicle(Vehicles vehicle)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Vehicles SET Type = @Type, Capacity = @Capacity WHERE VehicleId = @VehicleId", con);
                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleID);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@Capacity", vehicle.Capacity);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public bool DeleteVehicle(int vehicleId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Vehicles WHERE VehicleId = @VehicleId", con);
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public bool ScheduleTrip(int vehicleId, int routeId, string departureDate, string arrivalDate)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Trips (VehicleId, RouteId, DepartureDate, ArrivalDate) VALUES (@VehicleId, @RouteId, @DepartureDate, @ArrivalDate)", con);
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                cmd.Parameters.AddWithValue("@RouteId", routeId);
                cmd.Parameters.AddWithValue("@DepartureDate", departureDate);
                cmd.Parameters.AddWithValue("@ArrivalDate", arrivalDate);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public bool CancelTrip(int tripId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Trips WHERE TripId = @TripId", con);
                cmd.Parameters.AddWithValue("@TripId", tripId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public bool BookTrip(int tripId, int passengerId, string bookingDate)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Bookings (TripId, PassengerId, BookingDate) VALUES (@TripId, @PassengerId, @BookingDate)", con);
                cmd.Parameters.AddWithValue("@TripId", tripId);
                cmd.Parameters.AddWithValue("@PassengerId", passengerId);
                cmd.Parameters.AddWithValue("@BookingDate", bookingDate);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public bool CancelBooking(int bookingId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Bookings WHERE BookingId = @BookingId", con);
                cmd.Parameters.AddWithValue("@BookingId", bookingId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public bool AllocateDriver(int tripId, int driverId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Trips SET DriverId = @DriverId WHERE TripId = @TripId", con);
                cmd.Parameters.AddWithValue("@TripId", tripId);
                cmd.Parameters.AddWithValue("@DriverId", driverId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public bool DeallocateDriver(int tripId)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Trips SET DriverId = NULL WHERE TripId = @TripId", con);
                cmd.Parameters.AddWithValue("@TripId", tripId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public List<Bookings> GetBookingsByPassenger(int passengerId)
        {
            List<Bookings> bookings = new List<Bookings>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Bookings WHERE PassengerId = @PassengerId", con);
                cmd.Parameters.AddWithValue("@PassengerId", passengerId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Bookings booking = new Bookings
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
        public List<Bookings> GetBookingsByTrip(int tripId)
        {
            List<Bookings> bookings = new List<Bookings>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Bookings WHERE TripId = @TripId", con);
                cmd.Parameters.AddWithValue("@TripId", tripId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Bookings booking = new Bookings
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
        public List<Drivers> GetAvailableDrivers()
        {
            List<Drivers> drivers = new List<Drivers>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Drivers WHERE Status = 'Available'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Drivers driver = new Drivers
                    {
                        DriverID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        LicenseNumber = reader.GetString(2),
                        Status = reader.GetString(3)
                    };
                    drivers.Add(driver);
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
            return drivers;
        }
    }
}
