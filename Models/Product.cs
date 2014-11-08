using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class Product
    {
        [Key]
        [Required]
        [StringLength(13, ErrorMessage = "The UPC code should be 13 digits.")]
        public string UPC { get; set; }
        [StringLength(30, ErrorMessage = "The Product Name is limited under 30 charaters.")]
        public string Name { get; set; }
        [StringLength(30, ErrorMessage = "The Company Name is limited under 30 charaters.")]
        public string CompanyName { get; set; }
        [StringLength(30, ErrorMessage = "The Parent Company Name is limited under 30 charaters.")]
        public string ParentCompany { get; set; }
        public double Weight { get; set; }
        public double TotalWeight { get; set; }

        public string Category { get; set; }
    
        public virtual ICollection<ProductHasConstituent> ProductHasConstituents { get; set; }
        public virtual ProductImage ProductImage { get; set; }
    }
}

