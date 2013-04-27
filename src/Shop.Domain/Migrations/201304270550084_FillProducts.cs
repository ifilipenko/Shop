namespace Shop.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FillProducts : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[Products] (Name, Category, Vendor, Description) 
                  VALUES ('Колбаса', 'Мясо', 'ЯМЗ', '')");
        }
        
        public override void Down()
        {
        }
    }
}
