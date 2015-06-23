using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class ProductHasConstituent
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Product")]
        public string UPC { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Constituent")]
        public string ConstituentName { get; set; }
        public double PartWeight { get; set; }

        //TODO: Calculate the percentage when user needs it.

        public virtual Product Product { get; set; }
        public virtual Constituent Constituent { get; set; }
    }
}