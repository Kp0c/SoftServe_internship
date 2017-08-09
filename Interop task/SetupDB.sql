CREATE TABLE Users (
  username VARCHAR(255) PRIMARY KEY NOT NULL,
  password VARCHAR(255) NOT NULL,
  #money INT NOT NULL DEFAULT 1000,
);

CREATE TABLE Transactions (
  id int PRIMARY KEY NOT NULL IDENTITY (1, 1),
  debitUser VARCHAR(255) FOREIGN KEY REFERENCES Users (username) NOT NULL,
  creditUser VARCHAR(255) FOREIGN KEY REFERENCES Users (username) NOT NULL,
  #sum INT NOT NULL
);

CREATE PROCEDURE make_transaction @from VARCHAR(255), @to VARCHAR(255), @count INT
AS
  IF @count > 0
  BEGIN
    INSERT INTO Transactions
      VALUES (@from, @to, @count)
    UPDATE Users
    SET [#money] = [#money] - @count
    WHERE username = @from
    UPDATE Users
    SET [#money] = [#money] + @count
    WHERE username = @to
  END
GO

CREATE PROCEDURE remove_user @username VARCHAR(255)
AS
  DELETE FROM Transactions
  WHERE debitUser = @username
    OR creditUser = @username
  DELETE FROM Users
  WHERE username = @username
GO

INSERT INTO Users
  VALUES ('admin', 'admin', DEFAULT)
INSERT INTO Users
  VALUES ('1', '1', DEFAULT)
INSERT INTO Users
  VALUES ('0', '0', DEFAULT)

EXECUTE make_transaction '0', '1', 200
EXECUTE remove_user '0'