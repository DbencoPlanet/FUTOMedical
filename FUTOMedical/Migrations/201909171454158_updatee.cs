namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentDepts", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Patients", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Patients", "StudentDeptId", "dbo.StudentDepts");
            DropIndex("dbo.Patients", new[] { "SchoolId" });
            DropIndex("dbo.Patients", new[] { "StudentDeptId" });
            DropIndex("dbo.StudentDepts", new[] { "SchoolId" });
            CreateTable(
                "dbo.BloodBanks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Age = c.String(),
                        DonorName = c.String(),
                        Sex = c.String(),
                        BloodGroup = c.String(),
                        LastDonationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(),
                        Description = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        PatientId = c.Int(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Due = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrandTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId);
            
            AddColumn("dbo.Patients", "Department", c => c.String());
            AddColumn("dbo.Laboratorists", "LaboratoristId", c => c.String());
            DropColumn("dbo.Patients", "JambReg");
            DropColumn("dbo.Patients", "MatricNo");
            DropColumn("dbo.Patients", "SchoolId");
            DropColumn("dbo.Patients", "StudentDeptId");
            DropColumn("dbo.Patients", "DateOfEntry");
            DropColumn("dbo.Patients", "ModeOfEntry");
            DropColumn("dbo.Patients", "ModeOfStudy");
            DropColumn("dbo.Laboratorists", "PharmacistId");
            DropTable("dbo.Schools");
            DropTable("dbo.StudentDepts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentDepts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortCode = c.String(),
                        Image = c.String(),
                        SchoolId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Laboratorists", "PharmacistId", c => c.String());
            AddColumn("dbo.Patients", "ModeOfStudy", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "ModeOfEntry", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "DateOfEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.Patients", "StudentDeptId", c => c.Int());
            AddColumn("dbo.Patients", "SchoolId", c => c.Int());
            AddColumn("dbo.Patients", "MatricNo", c => c.String());
            AddColumn("dbo.Patients", "JambReg", c => c.String());
            DropForeignKey("dbo.Invoices", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.InvoiceLines", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Invoices", new[] { "PatientId" });
            DropIndex("dbo.InvoiceLines", new[] { "InvoiceId" });
            DropColumn("dbo.Laboratorists", "LaboratoristId");
            DropColumn("dbo.Patients", "Department");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceLines");
            DropTable("dbo.BloodBanks");
            CreateIndex("dbo.StudentDepts", "SchoolId");
            CreateIndex("dbo.Patients", "StudentDeptId");
            CreateIndex("dbo.Patients", "SchoolId");
            AddForeignKey("dbo.Patients", "StudentDeptId", "dbo.StudentDepts", "Id");
            AddForeignKey("dbo.Patients", "SchoolId", "dbo.Schools", "Id");
            AddForeignKey("dbo.StudentDepts", "SchoolId", "dbo.Schools", "Id");
        }
    }
}
