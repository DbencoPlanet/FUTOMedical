namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reportupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "DoctorReport", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "DoctorReport");
        }
    }
}
