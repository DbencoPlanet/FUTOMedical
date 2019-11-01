namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceLines", "Discount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceLines", "Discount");
        }
    }
}
