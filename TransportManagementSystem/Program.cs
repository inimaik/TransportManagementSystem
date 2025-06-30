using Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using TransportManagementSystem.Menu;

namespace TransportManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Transport Management System";
            MenuManager menu = new MenuManager();
            while (true)
            {
                menu.ShowMainMenu();
                Console.ForegroundColor = ConsoleColor.Green;
                int choice = menu.GetIntInput("Enter a choice: ");
                Console.ResetColor();
                switch (choice)
                {
                    case 1:
                        menu.ShowVehicleMenu();
                        break;
                    case 2:
                        menu.ShowTripMenu();
                        break;
                    case 3:
                        menu.ShowBookingMenu();
                        break;
                    case 4:
                        menu.ShowDriverMenu();
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nThank you for using the system. Goodbye!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        menu.WriteCentered("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
        