using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Recycling.Models
{
    public class Consituent
    {
        [Key]
        public string ConstituentName { get; set; }
        public string Type { get; set; }
        public string ProductUPC { get; set; }

    }
}