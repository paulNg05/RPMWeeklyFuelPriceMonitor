CREATE PROCEDURE [dbo].[sp_FuelDairyPrice_Insert]
	@PriceDate date,
	@Price	 money,
	@CharDate nvarchar(8)
AS
Begin
 SET NOCOUNT ON
	IF NOT EXISTS (SELECT * FROM DBO.FuelDairyPrice WHERE PriceDate = @PriceDate) 
	BEGIN
		   INSERT INTO DBO.FuelDairyPrice (PriceDate, Price, CharDate)
		   VALUES (@PriceDate,@Price, @CharDate)
	END
End
