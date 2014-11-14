namespace Recycling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseModified : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductViews", "ProductName", c => c.String(nullable: false));
            AlterColumn("dbo.ProductViews", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.ProductViews", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.ProductViews", "ConstituentName", c => c.String(nullable: false));
            AlterColumn("dbo.ProductViews", "Recyclability", c => c.String(nullable: false));
            AlterColumn("dbo.ProductViews", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.ProductViews", "Region", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductViews", "Region", c => c.String());
            AlterColumn("dbo.ProductViews", "Type", c => c.String());
            AlterColumn("dbo.ProductViews", "Recyclability", c => c.String());
            AlterColumn("dbo.ProductViews", "ConstituentName", c => c.String());
            AlterColumn("dbo.ProductViews", "Category", c => c.String());
            AlterColumn("dbo.ProductViews", "CompanyName", c => c.String());
            AlterColumn("dbo.ProductViews", "ProductName", c => c.String());
        }
    }
}
