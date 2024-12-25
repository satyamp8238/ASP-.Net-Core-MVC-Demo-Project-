Database Setup for Project
This document provides a step-by-step guide to creating the necessary database tables and stored procedures for the project. 
It also includes comments to ensure proper understanding of each SQL operation.

Database Setup
1. Create Database
The first step is to create the database for your project. Replace YourDatabaseName with the name of your database.

Sql Server
Copy code
-- Creating the Database
CREATE DATABASE YourDatabaseName;
GO
2. Create Tables
Once the database is created, create the necessary tables. In this example, we will create a Products table to store product data.

Products Table
sql
Copy code
-- Creating Products Table
CREATE TABLE Products (
    inId INT IDENTITY(1,1) PRIMARY KEY,      -- Primary key with auto increment
    stName NVARCHAR(100) NOT NULL,            -- Product name (Required field)
    inPrice DECIMAL(18, 2) NOT NULL           -- Price of the product (Required field)
);
inId: Auto-incrementing primary key.
stName: Name of the product, cannot be null.
inPrice: Price of the product, also cannot be null.
3. Create Stored Procedure

[StoreProcedures.txt](https://github.com/user-attachments/files/18245445/StoreProcedures.txt)


Conclusion
This setup allows you to manage products in your database effectively. The stored procedure handles both inserts and updates, with success codes to track the result of each operation. You can extend this setup with more complex features like transaction management, error handling, and validation as needed.

