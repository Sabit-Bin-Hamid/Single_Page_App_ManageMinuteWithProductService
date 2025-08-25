﻿create  type MeetingMinuteDetailType as table
(
    MeetingMinuteId int,
    ProductServiceId int,
    Quantity decimal(18, 1),
    Unit nvarchar(50)
);

--INSERT INTO Individual_Customer_Tbl (Name) VALUES
--('Alice'),
--('Bob'),
--('Charlie'),
--('Diana'),
--('Ali'),
--('Bubi'),
--('Sakib'),
--('Apu'),
--('Rita');


--INSERT INTO Corporate_Customer_Tbl (Name) VALUES
--('BKS'),
--('NAGAD'),
--('UPAY');


--INSERT INTO Products_Service_Tbl (Name, Unit)
--VALUES 
--('Laptop', 'Piece'),
--('Mobile Phone', 'Piece'),
--('Printer', 'Piece'),
--('Consulting Service', 'Hour'),
--('Software License', 'License');
