namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceLines", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceLines", "Name");
        }
    }
}
