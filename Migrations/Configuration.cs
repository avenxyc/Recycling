namespace Recycling.Migrations
{
    using Recycling.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Recycling.Models.RecyclingDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Recycling.Models.RecyclingDb";
        }

        protected override void Seed(Recycling.Models.RecyclingDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Products.AddOrUpdate(
            //  p => p.UPC,
            //  new Product { UPC = "111111111" },
            //  new Product { UPC = "222222222" },
            //  new Product
            //  {
            //      UPC = "333333333",
            //      Name = "Oreo",
            //      CompanyName = "Christie",
            //      Weight = 200,
            //      TotalWeight = 250,
            //      ParentCompany = "Kraft",
            //      Constituents =
            //            new List<Constituent> {           
            //                    new Constituent {
            //                    ConstituentName = "Plastic bag",
            //                    Type = "Plastic",
            //                }
            //            }

                  
            //  }
            //);

            for (int i = 0; i < 1000; i++)
            {
                context.Products.AddOrUpdate(
                p => p.UPC,
                new Product
                {
                    UPC = i.ToString(),
                    ProductName = "Oreo" + i.ToString(),
                    CompanyName = "Christie" + i.ToString(),
                    Weight = 200,
                    TotalWeight = 250,
                    ParentCompany = "Kraft" + i.ToString(),
                });
            }
        }
    }
}
