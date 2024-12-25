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
We will now create a stored procedure called saveProduct to insert and update products.

saveProduct Stored Procedure
sql
Copy code
-- Creating a Stored Procedure for Saving Products
CREATE PROCEDURE saveProduct
    @inId INT = NULL,            -- ID of the product (NULL for new products)
    @stName NVARCHAR(100),       -- Name of the product
    @inPrice DECIMAL(18, 2),     -- Price of the product
    @inSuccess INT OUT          -- Output variable to indicate success
AS
BEGIN
    -- If no ID is provided, insert a new product
    IF @inId IS NULL
    BEGIN
        INSERT INTO Products (stName, inPrice)
        VALUES (@stName, @inPrice);    -- Insert values into the Products table
        
        SET @inSuccess = 101;          -- Success code for insert
    END
    ELSE
    BEGIN
        -- If ID is provided, update the existing product
        UPDATE Products
        SET stName = @stName,
            inPrice = @inPrice
        WHERE inId = @inId;            -- Update the record with the matching ID
        
        SET @inSuccess = 102;          -- Success code for update
    END
END;
Comments in the Stored Procedure:
@inId: This parameter can be NULL. If NULL is passed, the stored procedure inserts a new product; otherwise, it updates the product with the provided inId.
@stName and @inPrice: These parameters are required for both insert and update operations.
@inSuccess: An output parameter used to return a success code. 101 for a successful insert and 102 for a successful update.
4. Executing the Stored Procedure
Here’s how to execute the stored procedure from your application or query tool:

Insert New Product
sql
Copy code
DECLARE @inSuccess INT;
EXEC saveProduct @stName = 'New Product', @inPrice = 29.99, @inSuccess = @inSuccess OUTPUT;
SELECT @inSuccess AS SuccessCode;
Update Existing Product
sql
Copy code
DECLARE @inSuccess INT;
EXEC saveProduct @inId = 1, @stName = 'Updated Product', @inPrice = 35.99, @inSuccess = @inSuccess OUTPUT;
SELECT @inSuccess AS SuccessCode;
5. Error Handling
You should also include error handling to manage possible issues like inserting invalid data. Here's an enhanced version of the stored procedure with error handling:

sql
Copy code
BEGIN TRY
    -- Insert or update product logic here
END TRY
BEGIN CATCH
    -- Handling errors
    SET @inSuccess = -1;  -- Indicate failure
    PRINT ERROR_MESSAGE(); -- Print error message
END CATCH;
Conclusion
This setup allows you to manage products in your database effectively. The stored procedure handles both inserts and updates, with success codes to track the result of each operation. You can extend this setup with more complex features like transaction management, error handling, and validation as needed.

