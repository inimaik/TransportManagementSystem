using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dao;

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

            Console.ForegroundColor = ConsoleColor.Green;
            WriteCentered("Enter your choice: ");
            Console.ResetColor();
        } 
        #endregion

        #region TripMenu
        public void ShowTripMenu()//handle input and exceptions too
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
                    "║ 3. Back to Main Menu               ║",
                    "╚════════════════════════════════════╝"
                };

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var line in tripMenu)
                    WriteCentered(line);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                WriteCentered("Enter your choice: ");
                Console.ResetColor();

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        try
                        {
                            Console.Clear();
                            WriteCentered("Schedule Trip");

                            Console.Write("Enter Vehicle ID: ");
                            int vehicleId = int.Parse(Console.ReadLine());

                            Console.Write("Enter Route ID: ");
                            int routeId = int.Parse(Console.ReadLine());

                            Console.Write("Enter Departure Date (yyyy-MM-dd): ");
                            string departureDate = Console.ReadLine();

                            Console.Write("Enter Arrival Date (yyyy-MM-dd): ");
                            string arrivalDate = Console.ReadLine();

                            bool scheduled = service.ScheduleTrip(vehicleId, routeId, departureDate, arrivalDate);
                            Console.ForegroundColor = scheduled ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(scheduled ? "Trip scheduled successfully." : "Failed to schedule trip.");
                            Console.ResetColor();
                        }
                        catch (VechileNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        //ScheduleTrip()
                        WriteCentered("Schedule Trip selected...");

                        break;
                    case "2":
                        try
                        {
                            Console.Clear();
                            WriteCentered("Cancel Trip");

                            Console.Write("Enter Trip ID: ");
                            int tripId = int.Parse(Console.ReadLine());

                            bool canceled = service.CancelTrip(tripId);
                            Console.ForegroundColor = canceled ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(canceled ? "Trip cancelled successfully." : "Failed to cancel trip.");
                            Console.ResetColor();
                        }
                        catch (TripNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        //CancelTrip()
                        WriteCentered("Cancel Trip  selected...");
                        break;
                    case "3":
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
        public void ShowVehicleMenu()//handle input and exceptions too
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
                WriteCentered("Enter your choice: ");
                Console.ResetColor();

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        WriteCentered("Add Vehicle");
                        Vehicle newVehicle = new Vehicle();

                        Console.Write("Enter Vehicle ID: ");
                        newVehicle.VehicleID = int.Parse(Console.ReadLine());

                        Console.Write("Enter Vehicle Model: ");
                        newVehicle.Model = Console.ReadLine();

                        Console.Write("Enter Vehicle Type: ");
                        newVehicle.Type = Console.ReadLine();

                        Console.Write("Enter Capacity: ");
                        newVehicle.Capacity = int.Parse(Console.ReadLine());

                        Console.Write("Enter Status");
                        newVehicle.Status = Console.ReadLine();


                        bool added = service.AddVehicle(newVehicle);
                        Console.ForegroundColor = added ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.WriteLine(added ? "Vehicle added successfully." : "Failed to add vehicle.");
                        Console.ResetColor();
                        //AddVehicle()
                        WriteCentered("Add Vehicle selected...");
                        break;
                    case "2":
                        try
                        {
                            Console.Clear();
                            WriteCentered("Update Vehicle");
                            Vehicle updatedVehicle = new Vehicle();

                            Console.Write("Enter Vehicle ID to update: ");
                            updatedVehicle.VehicleID = int.Parse(Console.ReadLine());
                            Console.Write("Enter Vehicle Model: ");
                            updatedVehicle.Model = Console.ReadLine();

                            Console.Write("Enter New Type: ");
                            updatedVehicle.Type = Console.ReadLine();

                            Console.Write("Enter New Capacity: ");
                            updatedVehicle.Capacity = int.Parse(Console.ReadLine());
                            Console.Write("Enter Status");
                            updatedVehicle.Status = Console.ReadLine();

                            bool updated = service.UpdateVehicle(updatedVehicle);
                            Console.ForegroundColor = updated ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(updated ? "Vehicle updated successfully." : "Vehicle update failed.");
                            Console.ResetColor();
                        }
                        catch (VechileNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        //UpdateVehicle()
                        WriteCentered("Update Vehicle selected...");
                        break;
                    case "3":
                        try
                        {
                            Console.Clear();
                            WriteCentered("Delete Vehicle");
                            Console.Write("Enter Vehicle ID to delete: ");
                            int id = int.Parse(Console.ReadLine());

                            bool deleted = service.DeleteVehicle(id);
                            Console.ForegroundColor = deleted ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(deleted ? "Vehicle deleted." : "Failed to delete vehicle.");
                            Console.ResetColor();

                        }
                        catch (VechileNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        //DeleteVehicle()
                        WriteCentered("Delete Vehicle selected...");
                        break;
                    case "4":
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
                WriteCentered("Enter your choice: ");
                Console.ResetColor();

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        WriteCentered("Book Trip");

                        Console.Write("Enter Trip ID: ");
                        int tripId = int.Parse(Console.ReadLine());

                        Console.Write("Enter Passenger ID: ");
                        int passengerId = int.Parse(Console.ReadLine());

                        Console.Write("Enter Booking Date (yyyy-MM-dd): ");
                        string bookingDate = Console.ReadLine();

                        bool booked = service.BookTrip(tripId, passengerId, bookingDate);
                        Console.ForegroundColor = booked ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.WriteLine(booked ? "Trip booked successfully." : "Failed to book trip.");
                        Console.ResetColor();
                        //BookTrip()
                        WriteCentered("BookTrip selected...");
                        break;
                    case "2":
                        try
                        {
                            Console.Clear();
                            WriteCentered("Cancel Booking");

                            Console.Write("Enter Booking ID to cancel: ");
                            int bookingId = int.Parse(Console.ReadLine());

                            bool canceled = service.CancelBooking(bookingId);
                            Console.ForegroundColor = canceled ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(canceled ? "Booking canceled." : "Failed to cancel booking.");
                            Console.ResetColor();
                            break;
                        }
                        catch (BookingNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        //CancelBooking()
                        WriteCentered("Cancel Booking selected...");
                        break;
                    case "3":
                        //GetBookingsbyPassenger()
                        WriteCentered("Display Bookings by Passenger selected...");
                        break;
                    case "4":
                        //GetBookingsbyTrip()
                        WriteCentered("Display Bookings by Trip selected...");
                        break;
                    case "5":
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
        public void ShowDriverMenu()//handle input and exceptions too
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
                WriteCentered("Enter your choice: ");
                Console.ResetColor();

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        try
                        {
                            Console.Clear();
                            WriteCentered("Allocate Driver to Trip");

                            Console.Write("Enter Trip ID: ");
                            int tripId = int.Parse(Console.ReadLine());

                            Console.Write("Enter Driver ID: ");
                            int driverId = int.Parse(Console.ReadLine());

                            bool allocated = service.AllocateDriver(tripId, driverId);
                            Console.ForegroundColor = allocated ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(allocated ? "Driver allocated successfully." : "Failed to allocate driver.");
                            Console.ResetColor();
                        }
                        catch (TripNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        WriteCentered("AllocateDriver selected...");
                        //AllocateDriver()
                        break;
                    case "2":
                        try
                        {
                            Console.Clear();
                            WriteCentered("Deallocate Driver from Trip");

                            Console.Write("Enter Trip ID: ");
                            int tripId = int.Parse(Console.ReadLine());

                            bool deallocated = service.DeallocateDriver(tripId);
                            Console.ForegroundColor = deallocated ? ConsoleColor.Green : ConsoleColor.Red;
                            Console.WriteLine(deallocated ? "Driver deallocated successfully." : "Failed to deallocate driver.");
                            Console.ResetColor();
                        }
                        catch (TripNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        //DeallocateDriver()
                        WriteCentered("DeallocateDriver selected...");
                        break;
                    case "3":
                        try
                        {
                            Console.Clear();
                            WriteCentered("Available Drivers");

                            List<Driver> availableDrivers = service.GetAvailableDrivers();

                            if (availableDrivers.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("No available drivers found.");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                foreach (var driver in availableDrivers)
                                {
                                    Console.WriteLine($"Driver ID: {driver.DriverId}, Name: {driver.Name}, License: {driver.LicenseNumber}");
                                }
                            }
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        //GetAvailableDriver()
                        WriteCentered("GetAvailableDriver selected...");
                        break;
                    case "4":
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
        public static void WriteCentered(string line)
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
        #region ChoiceValidation
        public int ReadChoice(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice >= min && choice <= max)
                        return choice;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Please enter a number between {min} and {max}.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ResetColor();
                }
            }
        } 
        #endregion
    }
}
