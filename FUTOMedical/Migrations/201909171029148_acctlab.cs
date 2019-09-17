namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acctlab : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accountants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        AccountantId = c.String(),
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
                "dbo.Laboratorists",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Laboratorists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Accountants", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Laboratorists", new[] { "UserId" });
            DropIndex("dbo.Accountants", new[] { "UserId" });
            DropTable("dbo.Laboratorists");
            DropTable("dbo.Accountants");
        }
    }
}
