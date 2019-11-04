namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "PaymentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "PaymentDate", c => c.DateTime());
        }
    }
}
