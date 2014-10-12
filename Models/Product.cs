using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string UPC { get; set; }
        public string CompanyName { get; set; }
        public string ParentCompany { get; set; }
        public double Weight { get; set; }
        public double TotalWeight { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

    }
}