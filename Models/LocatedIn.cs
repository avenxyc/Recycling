using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class LocatedIn
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Constituent")]
        public string ConstituentName { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Region")]
        public string RegionName { get; set; }
        public string Recyclability { get; set; }

        public virtual Constituent Constituent { get; set; }
        public virtual Region Region { get; set; }
    }
}