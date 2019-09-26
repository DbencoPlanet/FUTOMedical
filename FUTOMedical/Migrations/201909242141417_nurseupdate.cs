namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nurseupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accountants", "Nationality", c => c.String());
            AddColumn("dbo.Nurses", "Nationality", c => c.String());
            AddColumn("dbo.Departments", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Laboratorists", "Nationality", c => c.String());
            AddColumn("dbo.Pharmacists", "Nationality", c => c.String());
            DropColumn("dbo.Departments", "DeptName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "DeptName", c => c.String(nullable: false));
            DropColumn("dbo.Pharmacists", "Nationality");
            DropColumn("dbo.Laboratorists", "Nationality");
            DropColumn("dbo.Departments", "Name");
            DropColumn("dbo.Nurses", "Nationality");
            DropColumn("dbo.Accountants", "Nationality");
        }
    }
}
