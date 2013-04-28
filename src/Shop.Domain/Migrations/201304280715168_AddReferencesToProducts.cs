namespace Shop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReferencesToProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Category_Id", c => c.Int());
            AddColumn("dbo.Products", "Vendor_Id", c => c.Int());
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Products", "Vendor_Id", "dbo.Vendors", "Id");
            CreateIndex("dbo.Products", "Category_Id");
            CreateIndex("dbo.Products", "Vendor_Id");
            DropColumn("dbo.Products", "Category");
            DropColumn("dbo.Products", "Vendor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Vendor", c => c.String());
            AddColumn("dbo.Products", "Category", c => c.String());
            DropIndex("dbo.Products", new[] { "Vendor_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropForeignKey("dbo.Products", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropColumn("dbo.Products", "Vendor_Id");
            DropColumn("dbo.Products", "Category_Id");
        }
    }
}
