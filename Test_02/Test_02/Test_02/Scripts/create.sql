-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-06-18 12:46:31.286

-- tables
-- Table: Car
CREATE TABLE Car (
    Id int identity NOT NULL,
    CarManufacturerId int  NOT NULL,
    ModelName varchar(200)  NOT NULL,
    Number int  NOT NULL,
    CONSTRAINT Car_pk PRIMARY KEY  (Id)
);

-- Table: CarManufacturer
CREATE TABLE CarManufacturer (
    Id int identity NOT NULL,
    Name varchar(200)  NOT NULL,
    CONSTRAINT CarManufacturer_uk_1 UNIQUE (Name),
    CONSTRAINT CarManufacturer_pk PRIMARY KEY  (Id)
);

-- Table: Competition
CREATE TABLE Competition (
    Id int identity NOT NULL,
    Name varchar(200)  NOT NULL,
    CONSTRAINT Competition_uk_1 UNIQUE (Name),
    CONSTRAINT Competition_pk PRIMARY KEY  (Id)
);

-- Table: Driver
CREATE TABLE Driver (
    Id int identity NOT NULL,
    FirstName varchar(200)  NOT NULL,
    LastName varchar(200)  NOT NULL,
    Birthday datetime2  NOT NULL,
    CarId int  NOT NULL,
    CONSTRAINT Driver_pk PRIMARY KEY  (Id)
);

-- Table: DriverCompetition
CREATE TABLE DriverCompetition (
    DriverId int identity NOT NULL,
    CompetitionId int  NOT NULL,
    Date datetime2  NOT NULL,
    CONSTRAINT DriverCompetition_pk PRIMARY KEY  (DriverId,CompetitionId)
);

-- foreign keys
-- Reference: Car_CarManufacturer (table: Car)
ALTER TABLE Car ADD CONSTRAINT Car_CarManufacturer
    FOREIGN KEY (CarManufacturerId)
    REFERENCES CarManufacturer (Id);

-- Reference: DriverCompetition_Competition (table: DriverCompetition)
ALTER TABLE DriverCompetition ADD CONSTRAINT DriverCompetition_Competition
    FOREIGN KEY (CompetitionId)
    REFERENCES Competition (Id);

-- Reference: DriverCompetition_Driver (table: DriverCompetition)
ALTER TABLE DriverCompetition ADD CONSTRAINT DriverCompetition_Driver
    FOREIGN KEY (DriverId)
    REFERENCES Driver (Id);

-- Reference: Driver_Car (table: Driver)
ALTER TABLE Driver ADD CONSTRAINT Driver_Car
    FOREIGN KEY (CarId)
    REFERENCES Car (Id);

-- End of file.

DROP TABLE Driver;
DROP TABLE DriverCompetition;
DROP TABLE Car;
DROP TABLE CarManufacturer;
DROP TABLE Competition;
