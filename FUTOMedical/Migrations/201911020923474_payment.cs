namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Invoices", "Paid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Paid", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Invoices", "Status");
        }
    }
}
