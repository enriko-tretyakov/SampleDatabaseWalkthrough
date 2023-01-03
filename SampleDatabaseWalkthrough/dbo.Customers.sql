CREATE TABLE [dbo].[Customers] (
    [Id]          INT           NOT NULL,
    [CustomerID]  NCHAR (5)     NOT NULL,
    [CompanyName] NVARCHAR (50) NOT NULL,
    [ContactName] NVARCHAR (50) NULL,
    [Phone]       INT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

