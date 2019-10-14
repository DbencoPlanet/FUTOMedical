namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diagnosisupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestReports", "TReport", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestReports", "TReport");
        }
    }
}
