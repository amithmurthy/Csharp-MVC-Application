using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MyModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        [DisplayFormat(DataFormatString= "{0:C0}")]
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public string CategoryName { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
    }
}