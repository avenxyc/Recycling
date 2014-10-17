namespace Recycling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consituents",
                c => new
                    {
                        ConstituentName = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                        ProductUPC = c.Int(nullable: false),
                        Product_UPC = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ConstituentName)
                .ForeignKey("dbo.Products", t => t.Product_UPC)
                .Index(t => t.Product_UPC);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        UPC = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CompanyName = c.String(),
                        ParentCompany = c.String(),
                        Weight = c.Double(nullable: false),
                        TotalWeight = c.Double(nullable: false),
                        Category = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.UPC);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Consituents", "Product_UPC", "dbo.Products");
            DropIndex("dbo.Consituents", new[] { "Product_UPC" });
            DropTable("dbo.Products");
            DropTable("dbo.Consituents");
        }
    }
}
