using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Recycling.Models
{
    public class RecyclingDb : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Consituent> Constituents { get; set; }
    }
}