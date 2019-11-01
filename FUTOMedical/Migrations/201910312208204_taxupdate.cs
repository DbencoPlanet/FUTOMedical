namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taxupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceLines", "VatRate", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceLines", "VatRate");
        }
    }
}
