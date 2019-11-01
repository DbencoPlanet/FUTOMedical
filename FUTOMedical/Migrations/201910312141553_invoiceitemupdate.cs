namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceitemupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceLines", "Vat", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.InvoiceLines", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceLines", "Quantity", c => c.Int());
            AlterColumn("dbo.InvoiceLines", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceLines", "SubTotal", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceLines", "SubTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceLines", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceLines", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.InvoiceLines", "Amount");
            DropColumn("dbo.InvoiceLines", "Vat");
        }
    }
}
