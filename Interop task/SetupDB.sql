--CREATE TABLE Users(
--	username varchar(255) PRIMARY KEY NOT NULL,
--	password varchar(255) NOT NULL,
--    #money int NOT NULL DEFAULT 1000,
--	);

--CREATE TABLE Transactions(
--  id int PRIMARY KEY NOT NULL IDENTITY(1,1),
--  debitUser varchar(255) FOREIGN KEY REFERENCES Users(username) NOT NULL,
--  creditUser varchar(255) FOREIGN KEY REFERENCES Users(username) NOT NULL,
--  #sum int NOT NULL
--);

--CREATE PROCEDURE make_transaction @from varchar(255), @to varchar(255), @count int
--AS
--	INSERT INTO Transactions VALUES(@from, @to, @count)
--	UPDATE Users SET [#money]=[#money]-@count WHERE username = @from
--	UPDATE Users SET [#money]=[#money]+@count WHERE username = @to
--GO

--INSERT INTO Users VALUES('admin','admin', default)
--INSERT INTO Users VALUES('1','1', default)
--INSERT INTO Users VALUES('0','0', default)

--EXECUTE make_transaction '0', '1', 200
