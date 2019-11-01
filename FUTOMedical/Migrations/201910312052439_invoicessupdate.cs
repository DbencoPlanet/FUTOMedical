namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicessupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Invoices", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Invoices", "Vat", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Discount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Paid", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Due", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Total", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "GrandTotal", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "GrandTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Due", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Paid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "Vat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Invoices", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
