Data Base publish:
	1) double click on RPMFuelProceDB.publish.xml
	2) Edit target database connection
	3) Fill in "PRMFuelPriceDB" as database name
	4) Click publish
	5) Open local database to verify the database is create with table "FuelDairyPrice" and store procedure sp_FuelDairyPrice_Inser"

appsettings.json
  Connection string and other paramenter are entered in this file. Can be change as needed.
  "api": "http://api.eia.gov/series/?api_key=ec92aacd6947350dcb894062a4ad2d08&series_id=PET.EMD_EPD2D_PTE_NUS_DPG.W",
  "delayExuction": 108000,
  "numDatesCutOff": 20

  Right now the program is set to run about every two minutes, can shorten the time by changing the "delayExuction" value
			