-- Insert sample data into CarManufacturer
INSERT INTO CarManufacturer (Name) VALUES
('Ferrari'),
('Mercedes'),
('Red Bull Racing');

-- Insert sample data into Car
INSERT INTO Car (CarManufacturerId, ModelName, Number) VALUES
(1, 'SF21', 5),
(1, 'SF21', 16),
(2, 'W12', 44),
(2, 'W12', 77),
(3, 'RB16B', 33),
(3, 'RB16B', 11);

-- Insert sample data into Competition
INSERT INTO Competition (Name) VALUES
('Monaco Grand Prix'),
('British Grand Prix'),
('Italian Grand Prix');

-- Insert sample data into Driver
INSERT INTO Driver (FirstName, LastName, Birthday, CarId) VALUES
('Charles', 'Leclerc', '1997-10-16', 2),
('Carlos', 'Sainz', '1994-09-01', 1),
('Lewis', 'Hamilton', '1985-01-07', 3),
('Valtteri', 'Bottas', '1989-08-28', 4),
('Max', 'Verstappen', '1997-09-30', 5),
('Sergio', 'Perez', '1990-01-26', 6);

-- Insert sample data into DriverCompetition
SET IDENTITY_INSERT DriverCompetition ON;
INSERT INTO DriverCompetition (DriverId, CompetitionId, Date)VALUES
(1, 1, '2023-05-23'),
(2, 1, '2023-05-23'),
(3, 1, '2023-05-23'),
(4, 1, '2023-05-23'),
(5, 1, '2023-05-23'),
(6, 1, '2023-05-23'),

(1, 2, '2023-07-18'),
(2, 2, '2023-07-18'),
(3, 2, '2023-07-18'),
(4, 2, '2023-07-18'),
(5, 2, '2023-07-18'),
(6, 2, '2023-07-18');
SET IDENTITY_INSERT DriverCompetition OFF;