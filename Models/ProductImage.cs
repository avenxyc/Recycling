using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class ProductImage
    {
        [Key]
        [ForeignKey("Product")]
        public string UPC { get; set; }
        public string Description { get; set; }
        public System.DateTime UploadDate { get; set; }
        public byte[] ImageFile { get; set; }

        public virtual Product Product { get; set; }
    }
}