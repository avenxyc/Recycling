namespace Recycling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyImageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductImages", "ImageUrl", c => c.String(nullable: false));
            DropColumn("dbo.ProductImages", "ImageFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductImages", "ImageFile", c => c.Binary());
            DropColumn("dbo.ProductImages", "ImageUrl");
        }
    }
}
