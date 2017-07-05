CREATE DATABASE VB6
CREATE TABLE Users(
	username VARCHAR(255) NOT NULL PRIMARY KEY,
	pass VARCHAR(255) NOT NULL,
	program VARCHAR(255) NOT NULL,
	is_secure BIT
	)
	--crypted 'admin' = 'aicopindpd'
INSERT INTO Users VALUES('admin', 'aicopindpd', 'admin-UI', 1)