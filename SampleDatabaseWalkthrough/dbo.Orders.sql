CREATE TABLE [dbo].[Orders] (
    [OrderID]       INT       NOT NULL,
    [CustomerID]    NCHAR (5) NOT NULL,
    [OrderDate]     DATETIME  NULL,
    [OrderQuantity] INT       NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);