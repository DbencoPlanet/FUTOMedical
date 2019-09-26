namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        DoctorId = c.Int(),
                        FolderNumber = c.String(),
                        OPDId = c.Int(),
                        NurseId = c.Int(),
                        PatientId = c.Int(),
                        LaboratoristsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Laboratorists", t => t.LaboratoristsId)
                .ForeignKey("dbo.Nurses", t => t.NurseId)
                .ForeignKey("dbo.OPDs", t => t.OPDId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.OPDId)
                .Index(t => t.NurseId)
                .Index(t => t.PatientId)
                .Index(t => t.LaboratoristsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestReports", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.TestReports", "OPDId", "dbo.OPDs");
            DropForeignKey("dbo.TestReports", "NurseId", "dbo.Nurses");
            DropForeignKey("dbo.TestReports", "LaboratoristsId", "dbo.Laboratorists");
            DropForeignKey("dbo.TestReports", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.TestReports", new[] { "LaboratoristsId" });
            DropIndex("dbo.TestReports", new[] { "PatientId" });
            DropIndex("dbo.TestReports", new[] { "NurseId" });
            DropIndex("dbo.TestReports", new[] { "OPDId" });
            DropIndex("dbo.TestReports", new[] { "DoctorId" });
            DropTable("dbo.TestReports");
        }
    }
}
