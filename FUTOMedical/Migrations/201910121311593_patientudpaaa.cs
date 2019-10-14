namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientudpaaa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sections", "PatientId", "dbo.Patients");
            DropIndex("dbo.Sections", new[] { "PatientId" });
            AlterColumn("dbo.Sections", "PatientId", c => c.Int());
            CreateIndex("dbo.Sections", "PatientId");
            AddForeignKey("dbo.Sections", "PatientId", "dbo.Patients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "PatientId", "dbo.Patients");
            DropIndex("dbo.Sections", new[] { "PatientId" });
            AlterColumn("dbo.Sections", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sections", "PatientId");
            AddForeignKey("dbo.Sections", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
