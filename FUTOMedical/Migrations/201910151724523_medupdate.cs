namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(),
                        Report = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationTitle = c.String(),
                        EmailAddress = c.String(),
                        PhoneNo = c.String(),
                        Favicon = c.String(),
                        Logo = c.String(),
                        Currency = c.String(),
                        Initial = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GReports", "PatientId", "dbo.Patients");
            DropIndex("dbo.GReports", new[] { "PatientId" });
            DropTable("dbo.Settings");
            DropTable("dbo.GReports");
        }
    }
}
