namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatee1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Admissions", "OPDId", "dbo.OPDs");
            DropForeignKey("dbo.Admissions", "PatientId", "dbo.Patients");
            DropIndex("dbo.Admissions", new[] { "PatientId" });
            DropIndex("dbo.Admissions", new[] { "OPDId" });
            AlterColumn("dbo.Admissions", "PatientId", c => c.Int());
            AlterColumn("dbo.Admissions", "OPDId", c => c.Int());
            CreateIndex("dbo.Admissions", "PatientId");
            CreateIndex("dbo.Admissions", "OPDId");
            AddForeignKey("dbo.Admissions", "OPDId", "dbo.OPDs", "Id");
            AddForeignKey("dbo.Admissions", "PatientId", "dbo.Patients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admissions", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Admissions", "OPDId", "dbo.OPDs");
            DropIndex("dbo.Admissions", new[] { "OPDId" });
            DropIndex("dbo.Admissions", new[] { "PatientId" });
            AlterColumn("dbo.Admissions", "OPDId", c => c.Int(nullable: false));
            AlterColumn("dbo.Admissions", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Admissions", "OPDId");
            CreateIndex("dbo.Admissions", "PatientId");
            AddForeignKey("dbo.Admissions", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Admissions", "OPDId", "dbo.OPDs", "Id", cascadeDelete: true);
        }
    }
}
