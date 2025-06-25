using Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace TransportManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Transport Management System";
            while (true)
            {
                ShowMainMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowVehicleMenu();
                        break;
                    case "2":
                        ShowTripMenu();
                        break;
                    case "3":
                        ShowBookingMenu();
                        break;
                    case "4":
                        ShowDriverMenu();
                        break;
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nThank you for using the system. Goodbye!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static void ShowMainMenu()
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

        static void ShowTripMenu()//handle input and exceptions too
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
                        //ScheduleTrip()
                        WriteCentered("Schedule Trip selected...");
                        break;
                    case "2":
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

        static void ShowVehicleMenu()//handle input and exceptions too
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
                        //AddVehicle()
                        WriteCentered("Add Vehicle selected...");
                        break;
                    case "2":
                        //UpdateVehicle()
                        WriteCentered("Update Vehicle selected...");
                        break;
                    case "3":
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
        static void ShowBookingMenu()
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
                        //BookTrip()
                        WriteCentered("BookTrip selected...");
                        break;
                    case "2":
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
        static void ShowDriverMenu()//handle input and exceptions too
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
                        WriteCentered("AllocateDriver selected...");
                        //AllocateDriver()
                        break;
                    case "2":
                        //DeallocateDriver()
                        WriteCentered("DeallocateDriver selected...");
                        break;
                    case "3":
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

        static void WriteCentered(string line)
        {
            int padding = (Console.WindowWidth - line.Length) / 2;
            if (padding < 0) padding = 0; // Just in case
            Console.SetCursorPosition(padding, Console.CursorTop);
            Console.WriteLine(line);
        }

        static void AddTopSpacing(int lines = 3)
        {
            for (int i = 0; i < lines; i++)
                Console.WriteLine();
        }
        //TransportManagementServiceImpl transportService = new TransportManagementServiceImpl();
        //List <Bookings>bookings = transportService.GetBookingsByPassenger();
        //List<Bookings> filteredBookings = bookings.Where(b => b.PassengerID == 1).ToList();
        //Console.WriteLine(" BookingId | TripID | PassengerID | BookingDate | Status");
        //foreach (Bookings booking in filteredBookings)
        //{
        //    Console.WriteLine($"{booking.BookingID} | {booking.TripID} | {booking.PassengerID} | {booking.BookingDate} | {booking.Status}");
        //}
    }
}
