CREATE TABLE [dbo].[FuelDairyPrice]
(
	[int] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [PriceDate] DATE NOT NULL, 
    [Price] MONEY NOT NULL, 
    [CharDate] NVARCHAR(8) NOT NULL
)
