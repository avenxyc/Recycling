using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recycling.Models
{
    public class ProductView
    {
        [Key]
        public string UPC { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DisplayName("Parent Company")]
        public string ParentCompany { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        [DisplayName("Total Weight")]
        public double TotalWeight { get; set; }

        [DisplayName("Number of Constituents")]
        public uint NumberOfConstituent { get; set; }

        [Required]
        public string Category { get; set; }

        public string ImageURL { get; set; }

        public byte[] ProductImage { get; set; }

        [Required]
        [DisplayName("Part Weight")]
        public double PartWeight { get; set; }

        [Required]
        [DisplayName("Constituent Name")]
        public string ConstituentName { get; set; }

        [Required]
        public string Recyclability { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Region { get; set; }

    }
}