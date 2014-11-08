using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class Region
    {
        [Key]
        public string RegionName { get; set; }

        public virtual ICollection<LocatedIn> LocatedIns { get; set; }
    }
}