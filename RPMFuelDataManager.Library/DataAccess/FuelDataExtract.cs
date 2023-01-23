using ConsoleApp1;
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
    public class FuelDataExtract
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
                    var newDate = DateTime.ParseExact(item[0].ToString(),
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture);
                   
                    var price = item[1];
                    myData.Add(new DataModel { PriceDate = newDate, Price = price });
                }

                foreach (var data in myData)
                {
                    Console.WriteLine(data.PriceDate.ToString());
                    Console.WriteLine(data.PriceDate.ToString());
                }

                return myData;

            }
        }

        public  void SaveFuelData(List<DataModel> myData)
        {

        }

        public static JObject Parse(byte[] stream)
        {
            var jsonStr = Encoding.UTF8.GetString(stream);
            return JObject.Parse(jsonStr);
        }
    }
}
}
