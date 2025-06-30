create database TransportManagementDb
create table vehicles (
VehicleID int identity(1,1) primary key,
Model varchar(255) not null,
Capacity decimal(10, 2) not null,
[Type] varchar(50) not null, --Truck, Van, Bus
[Status] varchar(50) not null --Available, On Trip, Maintenance
)

create table routes (
RouteID int identity(1,1) primary key,
StartDestination varchar(255) not null,
EndDestination varchar(255) not null,
Distance decimal(10, 2) not null
)

create table trips (
TripID int identity(1,1) primary key,
VehicleID int not null,
RouteID int not null,
DepartureDate datetime not null,
ArrivalDate datetime not null,
[Status] varchar(50) not null, --scheduled, in progress, completed, cancelled
TripType varchar(50) not null default 'freight', --freight, passenger
MaxPassengers int,
foreign key (VehicleID) references vehicles(vehicleid),
foreign key (RouteID) references routes(routeid),
)

create table passengers (
PassengerID int identity(1,1) primary key,
FirstName varchar(255) not null,
Gender varchar(255) not null,
Age int not null,
Email varchar(255) not null unique,
PhoneNumber varchar(50) not null
)

create table bookings (
BookingID int identity(1,1) primary key,
TripID int not null,
PassengerID int not null,
BookingDate datetime not null,
[Status] varchar(50) not null, --confirmed, cancelled, completed
foreign key (TripID) references trips(tripid),
foreign key (PassengerID) references passengers(passengerid)
)

--drivers table to allocate drivers to trips(using EF approach for this table)
--create table drivers(DriverID int identity(1,1) primary key,[Name] varchar(100) not null,LicenseNumber varchar(50) not null,PhoneNumber varchar(15) not null,[Status] varchar(35) not null)

drop table drivers

INSERT INTO vehicles (Model, Capacity, [Type], [Status]) VALUES
('Volvo Bus', 50, 'Bus', 'Available'),
('Tata Truck', 10, 'Truck', 'On Trip'),
('Force Van', 15, 'Van', 'Available');


INSERT INTO routes (StartDestination, EndDestination, Distance) VALUES
('Chennai', 'Bangalore', 350.00),
('Delhi', 'Agra', 210.00),
('Mumbai', 'Pune', 150.00);

INSERT INTO trips (VehicleID, RouteID, DepartureDate, ArrivalDate, [Status], TripType, MaxPassengers) VALUES
(1, 1, '2025-06-24 09:00:00', '2025-06-24 15:00:00', 'Scheduled', 'Passenger', 50),
(2, 2, '2025-06-25 07:00:00', '2025-06-25 10:00:00', 'Scheduled', 'Freight', 0),
(3, 3, '2025-06-26 08:00:00', '2025-06-26 11:00:00', 'Scheduled', 'Passenger', 15);

INSERT INTO passengers (FirstName, Gender, Age, Email, PhoneNumber) VALUES
('Alice', 'Female', 28, 'alice@example.com', '1234567890'),
('Bob', 'Male', 34, 'bob@example.com', '2345678901'),
('Charlie', 'Male', 22, 'charlie@example.com', '3456789012');

INSERT INTO bookings (TripID, PassengerID, BookingDate, [Status]) VALUES
(1, 1, '2025-06-23 10:00:00', 'Confirmed'), -- Alice
(3, 1, '2025-06-23 11:00:00', 'Confirmed'), -- Alice
(3, 2, '2025-06-23 12:00:00', 'Cancelled'); -- Bob

SELECT * FROM bookings WHERE PassengerID = 1;

select * from bookings
select * from passengers
select * from trips
select * from vehicles
select * from routes
select * from drivers

--adding more data
insert into drivers values('Ajax','3161H3DSF','6373783246','Available')
INSERT INTO drivers VALUES ('Arya', 'TN01AB1234', '9876543210', 'Available');
INSERT INTO drivers VALUES ('Ravi', 'DL02CD5678', '9123456789', 'Available');
INSERT INTO drivers VALUES ('Mira', 'MH03EF9101', '9988776655', 'Available');
INSERT INTO drivers VALUES ('Kiran', 'KA04GH1122', '9443322110', 'Maintenance');
INSERT INTO drivers VALUES ('Jay', 'AP05IJ3344', '9612345678', 'Available');
INSERT INTO drivers VALUES ('Tina', 'RJ06KL5566', '9898989898', 'Available');
INSERT INTO drivers VALUES ('Omar', 'GJ07MN7788', '9556677889', 'Maintenance');
INSERT INTO drivers VALUES ('Lina', 'KL08OP9900', '9001112233', 'Available');
INSERT INTO drivers VALUES ('Arun', 'UP09QR1234', '9876123450', 'Available');
INSERT INTO drivers VALUES ('Zoya', 'WB10ST5678', '9012345678', 'Available');

INSERT INTO routes (StartDestination, EndDestination, Distance) VALUES
('Hyderabad', 'Warangal', 145.00),
('Kolkata', 'Durgapur', 170.00),
('Ahmedabad', 'Surat', 265.00),
('Jaipur', 'Udaipur', 395.00),
('Lucknow', 'Kanpur', 82.00),
('Bhopal', 'Indore', 195.00),
('Patna', 'Gaya', 100.00),
('Chandigarh', 'Shimla', 115.00),
('Coimbatore', 'Madurai', 215.00),
('Vijayawada', 'Vizag', 360.00)

INSERT INTO passengers (FirstName, Gender, Age, Email, PhoneNumber) VALUES
('Dina', 'Female', 30, 'dina@example.com', '4567890123'),
('Eli', 'Male', 26, 'eli@example.com', '5678901234'),
('Fay', 'Female', 24, 'fay@example.com', '6789012345'),
('Gus', 'Male', 29, 'gus@example.com', '7890123456'),
('Hana', 'Female', 32, 'hana@example.com', '8901234567'),
('Ian', 'Male', 27, 'ian@example.com', '9012345678'),
('Jay', 'Male', 35, 'jay@example.com', '1122334455'),
('Kim', 'Female', 31, 'kim@example.com', '2233445566');

INSERT INTO vehicles (Model, Capacity, [Type], [Status]) VALUES
('Mini Bus', 30, 'Bus', 'Available'),
('Eeco', 8, 'Van', 'Available'),
('Ace', 12, 'Truck', 'Available'),
('Tempo', 14, 'Van', 'Available'),
('City Bus', 40, 'Bus', 'Available'),
('CargoX', 20, 'Truck', 'Available');


--to ensure there is no repetition
SELECT TripID, PassengerID, COUNT(*)
FROM Bookings
GROUP BY TripID, PassengerID
HAVING COUNT(*) > 1;


ALTER TABLE Bookings
ADD CONSTRAINT UQ_Booking_Trip_Passenger UNIQUE (TripID, PassengerID);