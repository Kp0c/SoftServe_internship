--cleanup
USE Study
GO

---------------------------------------------------------------------------------------------------------------------------------------------DROPS, TRUNCATE TABLE
DROP PROCEDURE test_proc
DROP FUNCTION add_100
DROP INDEX Pilots.pilot_birth_date_index
DROP VIEW pilot_plane_bind
TRUNCATE TABLE Flights
DROP TABLE Flights
DROP TABLE #pilot_plane_salary
DROP TABLE #pilot_distance_fuel
DROP TABLE Planes
DROP TABLE Pilots

USE master
GO
DROP DATABASE Study

---------------------------------------------------------------------------------------------------------------------------------------------CREATES, CONSTRAINT KEYS
CREATE DATABASE Study
GO
USE Study
GO

CREATE PROCEDURE test_proc
AS
BEGIN
	SELECT 'i''m procedure'
END
GO  
                                                                                                                                                                                                                                                
CREATE FUNCTION add_100(@t INT)
RETURNS INT
AS
   BEGIN
       DECLARE @tt INT
       SELECT @tt=100+@t
       RETURN @tt
  END
GO

SELECT dbo.add_100(55)
EXECUTE test_proc

CREATE TABLE Pilots(
	id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	name VARCHAR(255) NOT NULL,
	birth_date DATE NOT NULL,
	is_active BIT DEFAULT 1,
	)

CREATE TABLE Planes(
	id INT IDENTITY(1,1),
	name VARCHAR(255),
	nation VARCHAR(max),
	bought_date DATE,
	capacity SMALLINT,
	CONSTRAINT U_Planes UNIQUE(name),
	CONSTRAINT NN_Planes CHECK (id IS NOT NULL AND name IS NOT NULL AND nation IS NOT NULL AND bought_date IS NOT NULL AND capacity IS NOT NULL),
	CONSTRAINT PK_Planes PRIMARY KEY(id)
	)

CREATE TABLE Flights(
	id INT IDENTITY(1,1),
	pilot INT,
	plane INT,
	fuel_spent INT,
	elapsed_distance INT,
	salary MONEY,
	PRIMARY KEY (id),
	--FOREIGN KEY (pilot) REFERENCES Pilots(id),--<--------------------------------
	FOREIGN KEY (plane) REFERENCES Planes(id),									--|
	CHECK (id IS NOT NULL),														--|
	)																			--|
																				--|
ALTER TABLE Flights																--|
	ADD CONSTRAINT FK_Flights_Pilots FOREIGN KEY (pilot) REFERENCES Pilots(id)----|

CREATE INDEX pilot_birth_date_index ON Pilots(birth_date)

---------------------------------------------------------------------------------------------------------------------------------------------DISTINCT, INNER JOIN
GO
CREATE VIEW pilot_plane_bind AS
	SELECT DISTINCT Pilots.id AS pilot_id, Planes.id AS plane_id, Pilots.name AS pilot, Planes.name AS plane 
		FROM Flights JOIN Pilots ON Flights.pilot=Pilots.id JOIN Planes ON Flights.plane=Planes.id
GO

---------------------------------------------------------------------------------------------------------------------------------------------INSERTS, CASTS
--initial fill tables							'MM-DD-YYYY'
INSERT Pilots VALUES ('James H. Doolittle',		CAST('01-01-1906' AS Date), 0)
					,('Anne Morrow Lindbergh',	CAST('11-01-1918' AS Date), 0)
					,('John Smith',				CAST('02-02-1976' AS Date), DEFAULT)
					,('Homer Simpson',			CAST('12-03-1084' AS Date), 0)
					,('Scooby-Moo',				CAST('03-05-1984' AS Date), DEFAULT)
					,('Mr. Anderson',			CAST('10-08-1654' AS Date), 0)
					,('<Write your name>',		CAST('04-13-2000' AS Date), DEFAULT)
					,('George R. R. Martin',	CAST('09-21-1967' AS Date), DEFAULT)
					,('Terry Pratchett',		CAST('05-30-1954' AS Date), 0)
					,('Jesus Christ',			CAST('01-01-0001' AS Date), NULL)

INSERT Planes VALUES ('Cessna 172',			'United States',			CAST('01-29-2008' AS DATE), 4)
					,('Piper PA-28 series', 'United States',			CAST('03-07-2003' AS DATE), 4)
					,('Antonov An-2',		'Soviet Union - Ukraine',	CAST('05-27-1943' AS DATE), 14)
					,('Beechcraft Bonanza', 'United States',			CAST('07-23-1995' AS DATE), 6)
					,('Avro 504',			'United Kingdom',			CAST('09-01-1967' AS DATE), 2)
					,('Piper Pacer',		'United States',			CAST('11-19-2013' AS DATE), 5)
					,('Aeronca Champion',	'United States',			CAST('12-14-2000' AS DATE), 2)
					,('Boeing 737',			'Germany',					CAST('10-05-2015' AS DATE), 600)
					,('Grunau Baby IIb',	'United States',			CAST('06-03-1993' AS DATE), 1)
					,('Airbus A320 family', 'France',					CAST('04-02-2017' AS DATE), 510)

INSERT Flights VALUES (7, 9, 223, 58, 5.81)
,(3, 6, 362, 94, 9.43)
,(10, 10, 654, 170, 187.34)
,(4, 7, 834, 217, 21.72)
,(5, 6, 857, 223, 22.32)
,(2, 9, 895, 233, 23.31)
,(3, 5, 576, 150, 15)
,(4, 8, 303, 78, 102.58)
,(6, 8, 297, 77, 100.55)
,(10, 7, 285, 74, 7.42)
,(4, 2, 281, 73, 7.32)
,(9, 2, 314, 81, 8.18)
,(7, 2, 598, 155, 15.57)
,(2, 1, 599, 155, 15.6)
,(5, 1, 141, 36, 3.67)
,(2, 7, 231, 60, 6.02)
,(9, 7, 219, 57, 5.7)
,(9, 6, 809, 210, 21.07)
,(2, 3, 133, 34, 3.46)
,(10, 7, 788, 205, 20.52)
,(1, 10, 340, 88, 97.4)
,(4, 1, 442, 115, 11.51)
,(6, 6, 457, 119, 11.9)
,(6, 6, 771, 200, 20.08)
,(4, 1, 464, 120, 12.08)
,(10, 9, 712, 185, 18.54)
,(2, 5, 142, 36, 3.7)
,(1, 2, 113, 29, 2.94)
,(1, 5, 671, 174, 17.47)
,(5, 8, 866, 225, 293.18)
,(2, 8, 437, 113, 147.94)
,(9, 1, 149, 38, 3.88)
,(3, 1, 85, 22, 2.21)
,(7, 6, 196, 51, 5.1)
,(8, 3, 134, 34, 3.49)
,(5, 6, 521, 135, 13.57)
,(4, 2, 406, 105, 10.57)
,(4, 8, 333, 86, 112.73)
,(7, 10, 823, 214, 235.76)
,(3, 4, 593, 154, 15.44)
,(8, 2, 768, 200, 20)
,(6, 4, 780, 203, 20.31)
,(4, 6, 955, 248, 24.87)
,(4, 8, 812, 211, 274.9)
,(7, 5, 835, 217, 21.74)
,(10, 4, 878, 228, 22.86)
,(10, 7, 964, 251, 25.1)
,(2, 1, 638, 166, 16.61)
,(5, 8, 59, 15, 19.97)
,(2, 2, 136, 35, 3.54)
,(3, 1, 777, 202, 20.23)
,(5, 10, 466, 121, 133.49)
,(4, 2, 889, 231, 23.15)
,(4, 1, 55, 14, 1.43)
,(6, 1, 469, 122, 12.21)
,(6, 8, 295, 76, 99.87)
,(5, 8, 710, 184, 240.36)
,(10, 3, 687, 178, 17.89)
,(7, 7, 102, 26, 2.66)
,(2, 1, 604, 157, 15.73)
,(8, 3, 422, 109, 10.99)
,(9, 1, 687, 178, 17.89)
,(1, 2, 585, 152, 15.23)
,(4, 6, 156, 40, 4.06)
,(10, 7, 602, 156, 15.68)
,(10, 7, 98, 25, 2.55)
,(2, 10, 113, 29, 32.37)
,(8, 7, 415, 108, 10.81)
,(4, 4, 785, 204, 20.44)
,(1, 9, 357, 92, 9.3)
,(4, 3, 153, 39, 3.98)
,(3, 6, 836, 217, 21.77)
,(7, 7, 991, 258, 25.81)
,(8, 6, 560, 145, 14.58)
,(7, 1, 230, 59, 5.99)
,(6, 10, 622, 161, 178.18)
,(1, 7, 988, 257, 25.73)
,(1, 10, 216, 56, 61.88)
,(6, 1, 625, 162, 16.28)
,(1, 5, 709, 184, 18.46)
,(9, 10, 372, 96, 106.56)
,(3, 1, 321, 83, 8.36)
,(1, 7, 141, 36, 3.67)
,(8, 4, 934, 243, 24.32)
,(1, 5, 405, 105, 10.55)
,(6, 1, 576, 150, 15)
,(2, 9, 423, 110, 11.02)
,(2, 1, 239, 62, 6.22)
,(4, 4, 336, 87, 8.75)
,(10, 8, 577, 150, 195.34)
,(3, 7, 258, 67, 6.72)
,(3, 9, 615, 160, 16.02)
,(2, 5, 124, 32, 3.23)
,(10, 6, 303, 78, 7.89)
,(6, 4, 350, 91, 9.11)
,(1, 10, 575, 149, 164.71)
,(9, 10, 953, 248, 272.99)
,(1, 9, 667, 173, 17.37)
,(4, 8, 940, 244, 318.23)
,(2, 10, 645, 167, 184.77)
,(NULL, 5, 65, 167, 184.37)
,(NULL, 8, 64, 91, 9.77)
,(5, NULL, 45, 248, 164.99)
,(2, NULL, 645, 149, 184.77)
,(NULL, NULL, 575, 173, 184.71)
,(NULL, NULL, 350, 244, 318.77)

SELECT * FROM pilot_plane_bind;

---------------------------------------------------------------------------------------------------------------------------------------------dateTime
SELECT SYSDATETIMEOFFSET() AS 'server dateTime'
---------------------------------------------------------------------------------------------------------------------------------------------NULL
SELECT * FROM Pilots WHERE is_active IS NOT NULL
---------------------------------------------------------------------------------------------------------------------------------------------AVG, COUNT
SELECT CAST(AVG(salary) AS DECIMAL(9,5)) AS 'average salary', CAST(AVG(elapsed_distance) AS DECIMAL(9,2)) AS 'average distance', COUNT(*) AS 'flights_count' FROM Flights

---------------------------------------------------------------------------------------------------------------------------------------------ALL, ANY, MAX, MIN
SELECT (SELECT name FROM Pilots WHERE birth_date=(SELECT MAX(birth_date) FROM Pilots)) AS 'most young pilot',
	(SELECT name FROM Pilots WHERE birth_date >= ALL(SELECT birth_date FROM Pilots)) AS 'most young pilot ALL',
	(SELECT name FROM Pilots WHERE birth_date=(SELECT MIN(birth_date) FROM Pilots)) AS 'most old pilot',
	(SELECT name FROM Pilots WHERE NOT birth_date > ANY(SELECT birth_date FROM Pilots)) AS 'most old pilot ANY'

---------------------------------------------------------------------------------------------------------------------------------------------SUM, LEN
SELECT SUM(elapsed_distance) AS 'summary distance' FROM Flights;
SELECT name, LEN(name) AS 'length' FROM Pilots ORDER BY LEN(name) DESC;

SELECT * FROM Pilots ORDER BY birth_date
SELECT * FROM Planes
SELECT * FROM Flights ORDER BY pilot

---------------------------------------------------------------------------------------------------------------------------------------------ALTER TABLE, IN, UPDATE, DELETE
ALTER TABLE Flights ADD test INT
GO
UPDATE Flights SET test=elapsed_distance
SELECT * FROM Flights WHERE test IN (57,60,51)
DELETE FROM Flights WHERE test < 55
SELECT * FROM Flights WHERE test IN (57,60,51)

ALTER TABLE Flights ALTER COLUMN test float
UPDATE Flights SET test=48.2
SELECT * FROM Flights 

ALTER TABLE Flights DROP COLUMN test
SELECT * FROM Flights

---------------------------------------------------------------------------------------------------------------------------------------------SELECT INTO
SELECT pilot_plane_bind.pilot, pilot_plane_bind.plane, Flights.salary INTO #pilot_plane_salary
	FROM Flights, pilot_plane_bind WHERE Flights.pilot=pilot_plane_bind.pilot_id AND Flights.plane=pilot_plane_bind.plane_id

---------------------------------------------------------------------------------------------------------------------------------------------TOP
SELECT TOP 50 PERCENT * FROM #pilot_plane_salary ORDER BY salary DESC
---------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE #pilot_distance_fuel(
	pilot VARCHAR(255),
	plane VARCHAR(255),
	distance INT,
	fuel INT,
	salary MONEY,
	);

-----------------------------------------------------------------------------------------------------------------------------------INSERT INTO SELECT, ORDER BY, GROUP BY
INSERT INTO #pilot_distance_fuel(pilot, plane, distance, fuel, salary) 
	SELECT SYSDATETIME.name, SUM.name, SUM([CREATE].elapsed_distance), SUM([CREATE].fuel_spent), SUM([CREATE].salary)
		FROM Pilots AS SYSDATETIME, Flights AS [CREATE], Planes AS SUM
			WHERE SYSDATETIME.id=[CREATE].pilot AND SUM.id=[CREATE].plane
				GROUP BY SYSDATETIME.name, SUM.name

SELECT * FROM #pilot_plane_salary ORDER BY salary DESC

-----------------------------------------------------------------------------------------------------------------------------------UNION(EXCEPT), HAVING, AND, OR, BETWEEN
SELECT pilot, SUM(distance) AS distance, SUM(fuel) AS fuel, SUM(salary) FROM #pilot_distance_fuel
	GROUP BY pilot
EXCEPT
SELECT pilot, SUM(distance) AS distance, SUM(fuel) AS fuel, SUM(salary) AS salary FROM #pilot_distance_fuel
	GROUP BY pilot
	HAVING SUM(distance) > 1000 OR SUM(salary) BETWEEN 500 AND 1000
	ORDER BY SUM(salary) DESC

---------------------------------------------------------------------------------------------------------------------------------------------WHERE, LIKE
DELETE FROM #pilot_distance_fuel WHERE pilot LIKE 'j%'

SELECT * FROM #pilot_distance_fuel
---------------------------------------------------------------------------------------------------------------------------------------------JOINS EXCEPT INNER
SELECT * FROM Flights LEFT JOIN Pilots ON Flights.pilot=Pilots.id
SELECT * FROM Flights RIGHT JOIN Planes ON Flights.plane=Planes.id
SELECT * FROM Pilots CROSS JOIN Planes