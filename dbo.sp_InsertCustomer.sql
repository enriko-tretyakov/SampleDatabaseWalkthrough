CREATE PROCEDURE [dbo].[sp_InsertCustomer]
    @Id int,
    @customerID nchar(5),
	@companyName  nvarchar(50),
	@contactName  nvarchar(50),
    @Phone nvarchar(24)
AS
    INSERT INTO Customers(Id, CustomerID, CompanyName, ContactName, Phone)
    VALUES (@Id, @customerID, @companyName, @contactName, @Phone)
  
    SELECT SCOPE_IDENTITY()
GO
