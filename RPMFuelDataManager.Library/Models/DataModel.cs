using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPMFuelDataManager.Library.Models
{
    public class DataModel
    {
        public DateOnly PriceDate { get; set; }
        public decimal Price { get; set; }
    }
}
