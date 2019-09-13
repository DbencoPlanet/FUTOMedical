namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNo = c.String(),
                        BedNo = c.String(),
                        DateAdmitted = c.DateTime(nullable: false),
                        DateDischarged = c.DateTime(),
                        PatientId = c.Int(nullable: false),
                        OPDId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OPDs", t => t.OPDId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.OPDId);
            
            CreateTable(
                "dbo.OPDs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BP = c.String(),
                        Temperature = c.String(),
                        Weight = c.String(),
                        PatientId = c.Int(),
                        NurseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nurses", t => t.NurseId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId)
                .Index(t => t.NurseId);
            
            CreateTable(
                "dbo.Nurses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        NurseId = c.String(),
                        Surname = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        Othernames = c.String(),
                        EmailAddress = c.String(nullable: false),
                        PhoneNo = c.String(),
                        Sex = c.String(),
                        Picture = c.String(),
                        Address = c.String(nullable: false),
                        BloodGroup = c.String(),
                        Education = c.String(),
                        StateOfOrigin = c.String(),
                        LocalGov = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Surname = c.String(),
                        Firstname = c.String(),
                        Othernames = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Photo = c.String(),
                        Nationality = c.String(),
                        StateOfOrigin = c.String(),
                        LocalGov = c.String(),
                        Surname = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        Othernames = c.String(),
                        Sex = c.String(nullable: false),
                        PhoneNo = c.String(),
                        EmailAddress = c.String(nullable: false),
                        DOB = c.DateTime(),
                        JambReg = c.String(),
                        MatricNo = c.String(),
                        SchoolId = c.Int(),
                        StudentDeptId = c.Int(),
                        PermanentAddress = c.String(),
                        ParentGuardianName = c.String(),
                        ParentGuardianPhone = c.String(),
                        NextOfKin = c.String(),
                        NextOfKinAddress = c.String(),
                        NextOfKinPhone = c.String(),
                        DateOfEntry = c.DateTime(nullable: false),
                        ModeOfEntry = c.Int(nullable: false),
                        ModeOfStudy = c.Int(nullable: false),
                        Address = c.String(),
                        PlaceOfBirth = c.String(),
                        ResidentAddress = c.String(),
                        Religion = c.String(),
                        CardNumber = c.String(),
                        FolderNumber = c.String(),
                        BloodGroup = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .ForeignKey("dbo.StudentDepts", t => t.StudentDeptId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SchoolId)
                .Index(t => t.StudentDeptId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        DoctorId = c.String(),
                        Surname = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        Othernames = c.String(),
                        EmailAddress = c.String(nullable: false),
                        Designation = c.String(),
                        DeptId = c.Int(),
                        PhoneNo = c.String(),
                        MobileNo = c.String(nullable: false),
                        Biography = c.String(),
                        Sex = c.String(),
                        BloodGroup = c.String(),
                        Education = c.String(),
                        Picture = c.String(),
                        Specialist = c.String(),
                        Address = c.String(),
                        StateOfOrigin = c.String(),
                        LocalGov = c.String(),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.LocalGovs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LGAName = c.String(),
                        StatesId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StatesId)
                .Index(t => t.StatesId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pharmacists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        PharmacistId = c.String(),
                        Surname = c.String(nullable: false),
                        Firstname = c.String(nullable: false),
                        Othernames = c.String(),
                        EmailAddress = c.String(nullable: false),
                        PhoneNo = c.String(),
                        Sex = c.String(),
                        Picture = c.String(),
                        Address = c.String(nullable: false),
                        StateOfOrigin = c.String(),
                        LocalGov = c.String(),
                        BloodGroup = c.String(),
                        Education = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        DoctorId = c.Int(),
                        FolderNumber = c.String(),
                        OPDId = c.Int(),
                        NurseId = c.Int(),
                        PatientId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Nurses", t => t.NurseId)
                .ForeignKey("dbo.OPDs", t => t.OPDId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.OPDId)
                .Index(t => t.NurseId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpenDate = c.DateTime(nullable: false),
                        CloseDate = c.DateTime(),
                        OpdStatus = c.Boolean(nullable: false),
                        SectionOpen = c.Boolean(nullable: false),
                        SectionClose = c.Boolean(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reports", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Reports", "OPDId", "dbo.OPDs");
            DropForeignKey("dbo.Reports", "NurseId", "dbo.Nurses");
            DropForeignKey("dbo.Reports", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Pharmacists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LocalGovs", "StatesId", "dbo.States");
            DropForeignKey("dbo.Doctors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Doctors", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Admissions", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Admissions", "OPDId", "dbo.OPDs");
            DropForeignKey("dbo.OPDs", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Patients", "StudentDeptId", "dbo.StudentDepts");
            DropForeignKey("dbo.Patients", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.StudentDepts", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.OPDs", "NurseId", "dbo.Nurses");
            DropForeignKey("dbo.Nurses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Sections", new[] { "PatientId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reports", new[] { "PatientId" });
            DropIndex("dbo.Reports", new[] { "NurseId" });
            DropIndex("dbo.Reports", new[] { "OPDId" });
            DropIndex("dbo.Reports", new[] { "DoctorId" });
            DropIndex("dbo.Pharmacists", new[] { "UserId" });
            DropIndex("dbo.LocalGovs", new[] { "StatesId" });
            DropIndex("dbo.Doctors", new[] { "Department_Id" });
            DropIndex("dbo.Doctors", new[] { "UserId" });
            DropIndex("dbo.StudentDepts", new[] { "SchoolId" });
            DropIndex("dbo.Patients", new[] { "StudentDeptId" });
            DropIndex("dbo.Patients", new[] { "SchoolId" });
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Nurses", new[] { "UserId" });
            DropIndex("dbo.OPDs", new[] { "NurseId" });
            DropIndex("dbo.OPDs", new[] { "PatientId" });
            DropIndex("dbo.Admissions", new[] { "OPDId" });
            DropIndex("dbo.Admissions", new[] { "PatientId" });
            DropTable("dbo.Sections");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reports");
            DropTable("dbo.Pharmacists");
            DropTable("dbo.States");
            DropTable("dbo.LocalGovs");
            DropTable("dbo.Doctors");
            DropTable("dbo.Departments");
            DropTable("dbo.StudentDepts");
            DropTable("dbo.Schools");
            DropTable("dbo.Patients");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Nurses");
            DropTable("dbo.OPDs");
            DropTable("dbo.Admissions");
        }
    }
}
