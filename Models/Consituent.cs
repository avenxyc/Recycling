using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class Consituent
    {
        public string ConstituentName { get; set; }
        public string Type { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}