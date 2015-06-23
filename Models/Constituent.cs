using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class Constituent
    {
        public Constituent()
        {
            LocatedIns = new HashSet<LocatedIn> { };
            ProductHasConstituents = new HashSet<ProductHasConstituent> { };
        }

        public virtual string ConstituentName { get; set; }
        public virtual string Type { get; set; }

        public virtual ISet<LocatedIn> LocatedIns { get; set; }
        public virtual ISet<ProductHasConstituent> ProductHasConstituents { get; set; }
    }
}