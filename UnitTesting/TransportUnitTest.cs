using Dao;
using Exceptions;
using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    public class TransportUnitTest
    {
            private TransportManagementServiceImpl service;
            [SetUp]
            public void Setup()
            {
                service = new TransportManagementServiceImpl(); // Use a mock or test DB
            }

            [Test]
            public void TestAllocateDriver_Success()
            {
                int tripId = 1;    // Assume exists
                int driverId = 1; // Assume available

                bool result = service.AllocateDriver(tripId, driverId);

                Assert.IsTrue(result, "Driver should be allocated successfully.");
            }
        [Test]
        public void TestDeallocateDriver()
        {
            int driverId = 1;
            bool result = service.DeallocateDriver(driverId);

            Assert.IsTrue(result, "Driver should be allocated successfully.");
        }

            [Test]
            public void TestAddVehicle_Success()
            {
                Vehicle vehicle = new Vehicle
                {
                    Model = "Tata Bus",
                    Capacity = 40,
                    Type = "Passenger",
                    Status = "Available"
                };

                bool vehicleAdded = service.AddVehicle(vehicle);

                Assert.IsTrue(vehicleAdded);
            }

            [Test]
            public void TestBookTrip_Success()
            {
                int tripId = 1;           // Assume valid scheduled trip
                int passengerId = 1;   // Assume valid
                string bookingDate = "2025-06-26";

                bool result = service.BookTrip(tripId, passengerId, bookingDate);

                Assert.IsTrue(result, "Booking should succeed.");
            }

            [Test]
            public void TestVehicleNotFound_ThrowsException()
            {
                Vehicle vehicle = new Vehicle
                {
                    VehicleID = 9999, // Assume this ID does not exist
                    Model="Tata Truck",
                    Type = "Truck",
                    Capacity = 10,
                    Status = "Maintenance"
                };

                // Act + Assert
                Assert.Throws<VechileNotFoundException>(() => service.UpdateVehicle(vehicle));
            }

            [Test]
            public void TestBookingNotFound_ThrowsException()
            {
                int bookingId = 99999; // Non-existent booking ID

                Assert.Throws<BookingNotFoundException>(() => service.CancelBooking(bookingId));
            }
        }
    }
