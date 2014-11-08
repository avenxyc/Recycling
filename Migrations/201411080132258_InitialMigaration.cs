namespace Recycling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigaration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Constituents",
                c => new
                    {
                        ConstituentName = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ConstituentName);
            
            CreateTable(
                "dbo.LocatedIns",
                c => new
                    {
                        ConstituentName = c.String(nullable: false, maxLength: 128),
                        RegionName = c.String(nullable: false, maxLength: 128),
                        Classification = c.String(),
                        Recyclability = c.String(),
                    })
                .PrimaryKey(t => new { t.ConstituentName, t.RegionName })
                .ForeignKey("dbo.Constituents", t => t.ConstituentName, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionName, cascadeDelete: true)
                .Index(t => t.ConstituentName)
                .Index(t => t.RegionName);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RegionName);
            
            CreateTable(
                "dbo.ProductHasConstituents",
                c => new
                    {
                        UPC = c.String(nullable: false, maxLength: 13),
                        ConstituentName = c.String(nullable: false, maxLength: 128),
                        Percentage = c.Double(nullable: false),
                        PartWeight = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.UPC, t.ConstituentName })
                .ForeignKey("dbo.Constituents", t => t.ConstituentName, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.UPC, cascadeDelete: true)
                .Index(t => t.UPC)
                .Index(t => t.ConstituentName);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        UPC = c.String(nullable: false, maxLength: 13),
                        Name = c.String(maxLength: 30),
                        CompanyName = c.String(maxLength: 30),
                        ParentCompany = c.String(maxLength: 30),
                        Weight = c.Double(nullable: false),
                        TotalWeight = c.Double(nullable: false),
                        Category = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.UPC);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        UPC = c.String(nullable: false, maxLength: 13),
                        Description = c.String(maxLength: 300),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UPC)
                .ForeignKey("dbo.Products", t => t.UPC)
                .Index(t => t.UPC);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductHasConstituents", "UPC", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "UPC", "dbo.Products");
            DropForeignKey("dbo.ProductHasConstituents", "ConstituentName", "dbo.Constituents");
            DropForeignKey("dbo.LocatedIns", "RegionName", "dbo.Regions");
            DropForeignKey("dbo.LocatedIns", "ConstituentName", "dbo.Constituents");
            DropIndex("dbo.ProductImages", new[] { "UPC" });
            DropIndex("dbo.ProductHasConstituents", new[] { "ConstituentName" });
            DropIndex("dbo.ProductHasConstituents", new[] { "UPC" });
            DropIndex("dbo.LocatedIns", new[] { "RegionName" });
            DropIndex("dbo.LocatedIns", new[] { "ConstituentName" });
            DropTable("dbo.ProductImages");
            DropTable("dbo.Products");
            DropTable("dbo.ProductHasConstituents");
            DropTable("dbo.Regions");
            DropTable("dbo.LocatedIns");
            DropTable("dbo.Constituents");
        }
    }
}
