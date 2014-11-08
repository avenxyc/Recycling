using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class RecyclingDb : DbContext
    {
        public RecyclingDb() : base("name=DefaultConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Constituent> Constituents { get; set; }
        public DbSet<ProductHasConstituent> ProductHasConstituents { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<LocatedIn> LocatedIns { get; set; }

    }
}