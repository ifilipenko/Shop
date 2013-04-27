namespace Shop.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SetVendorFieldsConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vendors", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Vendors", "INN", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vendors", "INN", c => c.String());
            AlterColumn("dbo.Vendors", "Name", c => c.String());
        }
    }
}
