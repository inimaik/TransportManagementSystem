using Dao;
using MyExceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagementSystem.Menu
{
    public class MenuManager
    {
        TransportManagementServiceImpl service=new TransportManagementServiceImpl();
        #region MainMenu
        public void ShowMainMenu()
        {
            Console.Clear();
            AddTopSpacing(3);
            List<string> menuLines = new List<string>
            {
                "╔════════════════════════════════════════════════╗",
                "║         TRANSPORT MANAGEMENT SYSTEM            ║",
                "╠════════════════════════════════════════════════╣",
                "║ 1. Vehicle Operations                          ║",
                "║ 2. Trip Management                             ║",
                "║ 3. Booking Management                          ║",
                "║ 4. Driver Allocation                           ║",
                "║ 5. Exit                                        ║",
                "╚════════════════════════════════════════════════╝"
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var line in menuLines)
                WriteCentered(line);
            Console.ResetColor();

            
        } 
        #endregion

        #region TripMenu
        public void ShowTripMenu()
        {
            while (true)
            {
                Console.Clear();
                AddTopSpacing(2);
                List<string> tripMenu = new List<string>
                {
                    "╔════════════════════════════════════╗",
                    "║         TRIP MANAGEMENT            ║",
                    "╠════════════════════════════════════╣",
                    "║ 1. Schedule Trip                   ║",
                    "║ 2. Cancel Trip                     ║",
                    "║ 3. Display trips with routes       ║",
                    "║ 4. Back to Main Menu               ║",
                    "╚════════════════════════════════════╝"
                };

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var line in tripMenu)
                    WriteCentered(line);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                int choice = GetIntInput("Enter ur choice: ");
                Console.ResetColor();

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Schedule Trip");
                            int vehicleId = GetIntInput("Enter Vehicle ID: ");

                            int routeId = GetIntInput("Enter Route ID: ");

                            Console.Write("Enter Departure Date (yyyy-MM-dd): ");
                            string departureDate = Console.ReadLine();

                            Console.Write("Enter Arrival Date (yyyy-MM-dd): ");
                            string arrivalDate = Console.ReadLine();

                            bool scheduled = service.ScheduleTrip(vehicleId, routeId, departureDate, arrivalDate);
                            Console.ForegroundColor = scheduled ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(scheduled ? "Trip scheduled successfully." : "Failed to schedule trip.");
                            Console.ResetColor();
                        }
                        catch (VehicleNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (RouteNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }
                        WriteCentered("Returning ");
                        break;
                    case 2:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Cancel Trip");

                            int tripId = GetIntInput("Enter Trip ID: ");

                            bool canceled = service.CancelTrip(tripId);
                            Console.ForegroundColor = canceled ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(canceled ? "Trip cancelled successfully." : "Failed to cancel trip.");
                            Console.ResetColor();
                        }
                        catch (TripNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }

                        WriteCentered("Returning");
                        break;
                    case 3:
                        try {
                            Console.Clear();
                            WriteCentered("Display trips with routes");
                            var tripRoutes = service.GetAllTripsWithRoutes();

                            if (tripRoutes.Count == 0)
                            {
                                Console.WriteLine("No trip records found.");
                            }
                            else
                            {
                                Console.WriteLine("----------------------------------------------------------");
                                Console.WriteLine("TripId | Status |RouteId | Source | Destination | Distance");
                                Console.WriteLine("----------------------------------------------------------");
                                foreach (var row in tripRoutes)
                                {
                                    Console.WriteLine($"  {row["TripId"]}  |  {row["Status"]}  |  {row["RouteId"]}  |  {row["StartDestination"]}  |  {row["EndDestination"]}  |  {row["Distance"]}");
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }
                        WriteCentered("Returning");
                        break;
                    case 4:
                        WriteCentered("Returning to main menu...");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        WriteCentered("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
        } 
        #endregion

        #region VehicleMenu
        public void ShowVehicleMenu()
        {
            while (true)
            {
                Console.Clear();
                AddTopSpacing(2);
                List<string> vehicleMenu = new List<string>
                {
                    "╔════════════════════════════════════╗",
                    "║        VEHICLE OPERATIONS          ║",
                    "╠════════════════════════════════════╣",
                    "║ 1. Add Vehicle                     ║",
                    "║ 2. Update Vehicle                  ║",
                    "║ 3. Delete Vehicle                  ║",
                    "║ 4. Back to Main Menu               ║",
                    "╚════════════════════════════════════╝"
                };

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var line in vehicleMenu)
                    WriteCentered(line);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                int choice = GetIntInput("Enter ur choice: ");
                Console.ResetColor();

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Add Vehicle");
                            Vehicle newVehicle = new Vehicle();

                            Console.Write("Enter Vehicle Model: ");
                            newVehicle.Model = Console.ReadLine();

                            Console.Write("Enter Vehicle Type: ");
                            newVehicle.Type = Console.ReadLine();

                            newVehicle.Capacity = GetIntInput("Enter Capactity: ");

                            Console.Write("Enter Status: ");
                            newVehicle.Status = Console.ReadLine();

                            bool added = service.AddVehicle(newVehicle);
                            Console.ForegroundColor = added ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(added ? "Vehicle added successfully." : "Failed to add vehicle.");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }
                        WriteCentered("Returning back");
                        break;
                    case 2:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Update Vehicle");
                            Vehicle updatedVehicle = new Vehicle();

                            updatedVehicle.VehicleID = GetIntInput("Enter Vehicle ID to update: ");
                            Console.Write("Enter Vehicle Model: ");
                            updatedVehicle.Model = Console.ReadLine();

                            Console.Write("Enter New Type: ");
                            updatedVehicle.Type = Console.ReadLine();

                            updatedVehicle.Capacity = GetIntInput("Enter new Capacity: ");
                            Console.Write("Enter Status: ");
                            updatedVehicle.Status = Console.ReadLine();

                            bool updated = service.UpdateVehicle(updatedVehicle);
                            Console.ForegroundColor = updated ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(updated ? "Vehicle updated successfully." : "Vehicle update failed.");
                            Console.ResetColor();
                        }
                        catch (VehicleNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }
                        WriteCentered("Returning");
                        break;
                    case 3:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Delete Vehicle");
                            int id = GetIntInput("Vehicle ID to delete: ");

                            bool deleted = service.DeleteVehicle(id);
                            Console.ForegroundColor = deleted ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(deleted ? "Vehicle deleted." : "Failed to delete vehicle.");
                            Console.ResetColor();
                        }
                        catch (VehicleNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }
                        WriteCentered("Returning");
                        break;
                    case 4:
                        WriteCentered("Returning to main menu...");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        WriteCentered("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
        } 
        #endregion

        #region BookingMenu
        public void ShowBookingMenu()
        {
            while (true)
            {
                Console.Clear();
                AddTopSpacing(2);
                List<string> bookingMenu = new List<string>
                {
                    "╔════════════════════════════════════╗",
                    "║        BOOKING MANAGEMENT          ║",
                    "╠════════════════════════════════════╣",
                    "║ 1. Book Trip                       ║",
                    "║ 2. Cancel Booking                  ║",
                    "║ 3. Display Bookings by Passenger   ║",
                    "║ 4. Display Bookings by Trip        ║",
                    "║ 5. Back to Main Menu               ║",
                    "╚════════════════════════════════════╝"
                };

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var line in bookingMenu)
                    WriteCentered(line);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                int choice = GetIntInput("Enter ur choice: ");
                Console.ResetColor();

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Book Trip");

                            int tripId = GetIntInput("Enter Trip ID: ");

                            int passengerId = GetIntInput("Enter Passenger ID: ");

                            Console.Write("Enter Booking Date (yyyy-MM-dd): ");
                            string bookingDate = Console.ReadLine();

                            bool booked = service.BookTrip(tripId, passengerId, bookingDate);
                            Console.ForegroundColor = booked ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(booked ? "Trip booked successfully." : "Failed to book trip.");
                            Console.ResetColor();
                        }
                        catch(TripNotFoundException ex) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (PassengerNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Passenger has already booked this trip.");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }
                        
                        WriteCentered("Returning");
                        break;
                    case 2:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Cancel Booking");

                            int bookingId = GetIntInput("Enter Booking ID to cancel: ");

                            bool canceled = service.CancelBooking(bookingId);
                            Console.ForegroundColor = canceled ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(canceled ? "Booking canceled." : "Failed to cancel booking.");
                            Console.ResetColor();
                        }
                        catch (BookingNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }
                        WriteCentered("Returning");
                        break;
                    case 3:
                        try
                        {
                            Console.Clear();
                            WriteCentered("View Bookings by Passenger");

                            int passengerId = GetIntInput("Enter Passenger ID: ");
                            List<Booking> bookings = service.GetBookingsByPassenger(passengerId);

                            if (bookings.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                WriteCentered("No bookings found for this passenger.");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                WriteCentered("Bookings Found:");
                                Console.ResetColor();
                                Console.WriteLine("----------------------------------------------------");
                                Console.WriteLine("BookingID | TripID | PassengerID |   Date   | Status");
                                Console.WriteLine("----------------------------------------------------");

                                foreach (var booking in bookings)
                                {
                                    Console.WriteLine($"   {booking.BookingID}   |   {booking.TripID}   |      {booking.PassengerID}      |  {booking.BookingDate:yyyy-MM-dd}  | {booking.Status}");
                                }
                            }
                        }
                        catch (PassengerNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }

                        WriteCentered("Returning...");
                        break;
                    case 4:
                        try
                        {
                            Console.Clear();
                            WriteCentered("View Bookings by Trip");

                            int tripId = GetIntInput("Enter Trip ID: ");
                            List<Booking> bookings = service.GetBookingsByTrip(tripId);

                            if (bookings.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                WriteCentered("No bookings found for this trip.");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                WriteCentered("Bookings Found:");
                                Console.ResetColor();
                                Console.WriteLine("----------------------------------------------------");
                                Console.WriteLine("BookingID | TripID | PassengerID |   Date   | Status");
                                Console.WriteLine("----------------------------------------------------");

                                foreach (var booking in bookings)
                                {
                                    Console.WriteLine($"   {booking.BookingID}   |   {booking.TripID}   |      {booking.PassengerID}      |  {booking.BookingDate:yyyy-MM-dd}  | {booking.Status}");
                                }
                            }
                        }
                        catch (TripNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }

                        WriteCentered("Returning...");
                        break;
                    case 5:
                        WriteCentered("Returning to main menu...");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        WriteCentered("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
        } 
        #endregion

        #region DriverMenu
        public void ShowDriverMenu()
        {
            while (true)
            {
                Console.Clear();
                AddTopSpacing(2);
                List<string> driverMenu = new List<string>
                {
                    "╔════════════════════════════════════╗",
                    "║        DRIVER ALLOCATION           ║",
                    "╠════════════════════════════════════╣",
                    "║ 1. Allocate Driver                 ║",
                    "║ 2. Deallocate Driver               ║",
                    "║ 3. Display Available Driver        ║",
                    "║ 4. Back to Main Menu               ║",
                    "╚════════════════════════════════════╝"
                };

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var line in driverMenu)
                    WriteCentered(line);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                int choice = GetIntInput("Enter ur choice: ");
                Console.ResetColor();

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Allocate Driver to Trip");

                            int tripId = GetIntInput("Enter Trip ID: ");

                            int driverId = GetIntInput("Enter Driver ID: ");

                            bool allocated = service.AllocateDriver(tripId, driverId);
                            Console.ForegroundColor = allocated ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(allocated ? "Driver allocated successfully." : "Failed to allocate driver.");
                            Console.ResetColor();
                        }
                        catch (TripNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch(DriverNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch(InvalidOperationException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }

                        WriteCentered("Returning");
                        break;
                    case 2:
                        try
                        {
                            Console.Clear();
                            WriteCentered("Deallocate Driver from Trip");

                            int tripId = GetIntInput("Enter Trip ID: ");

                            bool deallocated = service.DeallocateDriver(tripId);
                            Console.ForegroundColor = deallocated ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(deallocated ? "Driver deallocated successfully." : "Failed to deallocate driver.");
                            Console.ResetColor();
                        }
                        catch (TripNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (DriverNotFoundException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.ResetColor();
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }
                        
                        WriteCentered("Returning");
                        break;
                    case 3:
                        try
                        {
                            Console.Clear();
                            WriteCentered("View Available Drivers");

                            List<Driver> drivers = service.GetAvailableDrivers();
                            if (drivers.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                WriteCentered("No available drivers found.");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                WriteCentered("Available Drivers:");
                                Console.ResetColor();
                                Console.WriteLine("---------------------------------------------");
                                Console.WriteLine("DriverID |  Name |  License Number |  Status ");
                                Console.WriteLine("---------------------------------------------");

                                foreach (var driver in drivers)
                                {
                                    Console.WriteLine($" {driver.DriverId} | {driver.Name} | {driver.LicenseNumber} | {driver.Status}");
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Database error: " + ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }

                        WriteCentered("Returning...");
                        break;
                    case 4:
                        WriteCentered("Returning to main menu...");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        WriteCentered("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
        } 
        #endregion

        #region WriteCentered
        public  void WriteCentered(string line)
        {
            int padding = (Console.WindowWidth - line.Length) / 2;
            if (padding < 0) padding = 0; // Just in case
            Console.SetCursorPosition(padding, Console.CursorTop);
            Console.WriteLine(line);
        }
        #endregion

        #region TopSpacing
        public static void AddTopSpacing(int lines = 3)
        {
            for (int i = 0; i < lines; i++)
                Console.WriteLine();
        }
        #endregion

        #region GetIntInput
        public int GetIntInput(string message)
        {
            Console.Write(message);
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Invalid input. Enter a number: ");
            }
            return value;
        } 
        #endregion
    }
}
