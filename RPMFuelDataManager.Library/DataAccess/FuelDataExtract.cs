
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RPMFuelDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RPMFuelDataManager.Library.DataAccess
{
    public class FuelDataExtract : IFuelDataExtract
    {
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly IConfiguration _config;

        public FuelDataExtract(ISqlDataAccess sqlDataAccess, IConfiguration config)
        {
            _sqlDataAccess = sqlDataAccess;
            _config = config;
        }
        public List<DataModel> GetFuelDat()
        {

            string url = _config.GetValue<string>("api");
            using (var wc = new WebClient())
            {
                var stream = wc.DownloadData(url);

                dynamic jsonObject = Parse(stream);

                List<DataModel> myData = new List<DataModel>();

                foreach (var item in jsonObject["series"][0]["data"])
                {
                    var charDate = item[0].ToString().Trim();
                    var newDate = DateTime.ParseExact(item[0].ToString(),
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture);

                    var price = item[1];
                    myData.Add(new DataModel { PriceDate = newDate, Price = price, CharDate = charDate });
                }

                //foreach (var data in myData)
                //{
                //    Console.WriteLine(data.PriceDate.ToString());
                //    Console.WriteLine(data.PriceDate.ToString());
                //}

                return myData;

            }
        }

        public void SaveFuelData(List<DataModel> myData)
        {
            int nDates = _config.GetValue<int>("numDatesCutOff");
            DateTime cutOffDate = DateTime.Today.AddDays(nDates * -1);
            foreach (var item in myData)
            {
                int res = DateTime.Compare(item.PriceDate, cutOffDate);
                if (res == 0 || res == 1)
                {
                    // save to DB 
                    try
                    {
                        _sqlDataAccess.SaveData("sp_FuelDairyPrice_Insert", new { @PriceDate = item.PriceDate, @Price = item.Price, @charDate = item.CharDate }, "PRMFuelData");
                    }
                    catch (Exception)
                    {

                        throw new Exception("Error while try to save data to database.");
                    }
                }
            }
        }

        public static JObject Parse(byte[] stream)
        {
            var jsonStr = Encoding.UTF8.GetString(stream);
            return JObject.Parse(jsonStr);
        }
    }
}

