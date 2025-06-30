using Dao;
using MyExceptions;
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
                service = new TransportManagementServiceImpl();
            }

            [Test]
            public void TestAllocateDriver_Success()
            {
                int tripId = 3;  
                int driverId = 4; 

                bool result = service.AllocateDriver(tripId, driverId);

                Assert.IsTrue(result);
            }
        [Test]
        public void TestDeallocateDriver_Sucess()
        {
            int tripId = 2;
            bool result = service.DeallocateDriver(tripId);

            Assert.IsTrue(result);
        }

            [Test]
            public void TestAddVehicle_Success()
            {
                Vehicle vehicle = new Vehicle
                {
                    Model = "Eicher",
                    Capacity = 20,
                    Type = "Truck",
                    Status = "Available"
                };

                bool vehicleAdded = service.AddVehicle(vehicle);

                Assert.IsTrue(vehicleAdded);
            }

            [Test]
            public void TestBookTrip_Success()
            {
                int tripId = 2;   
                int passengerId = 4;
                string bookingDate = "2025-06-27";

                bool result = service.BookTrip(tripId, passengerId, bookingDate);

                Assert.IsTrue(result);
            }
        [Test]
        public void TestBookTrip_InvalidTrip_ThrowsException()
        {
            int invalidTripId = 999;
            int passengerId = 1;
            string bookingDate = "2025-06-26";

            Assert.Throws<TripNotFoundException>(() => service.BookTrip(invalidTripId, passengerId, bookingDate));
        }

        [Test]
            public void TestVehicleNotFound_ThrowsException()
            {
                Vehicle vehicle = new Vehicle
                {
                    VehicleID = 9999, //ID does not exist
                    Model="Tata Truck",
                    Type = "Truck",
                    Capacity = 10,
                    Status = "Maintenance"
                };
                Assert.Throws<VehicleNotFoundException>(() => service.UpdateVehicle(vehicle));
            }

            [Test]
            public void TestBookingNotFound_ThrowsException()
            {
                int bookingId = 99999; // Non-existent booking ID

                Assert.Throws<BookingNotFoundException>(() => service.CancelBooking(bookingId));
            }
        }
    }
