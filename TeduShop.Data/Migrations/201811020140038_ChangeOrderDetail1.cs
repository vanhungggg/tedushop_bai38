namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderDetail1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "Price", c => c.Int(nullable: false));
        }
    }
}
