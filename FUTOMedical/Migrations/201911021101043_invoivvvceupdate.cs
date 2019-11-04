namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoivvvceupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Invoices", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Invoices", "StartDate", c => c.DateTime());
        }
    }
}
