using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class ConstituentView
    {
        [Key]
        public string ConstituentName { get; set; }
        public string Type { get; set; }

        public virtual ICollection<LocatedIn> LocatedIns { get; set; }
        public virtual ICollection<ProductHasConstituent> ProductHasConstituents { get; set; }
    }
}