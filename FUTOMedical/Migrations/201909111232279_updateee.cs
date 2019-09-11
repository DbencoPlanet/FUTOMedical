namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nurses", "Othernames", c => c.String());
            AddColumn("dbo.Doctors", "Othernames", c => c.String());
            AddColumn("dbo.Pharmacists", "Othernames", c => c.String());
            DropColumn("dbo.Nurses", "OtherName");
            DropColumn("dbo.Doctors", "OtherName");
            DropColumn("dbo.Pharmacists", "OtherName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pharmacists", "OtherName", c => c.String());
            AddColumn("dbo.Doctors", "OtherName", c => c.String());
            AddColumn("dbo.Nurses", "OtherName", c => c.String());
            DropColumn("dbo.Pharmacists", "Othernames");
            DropColumn("dbo.Doctors", "Othernames");
            DropColumn("dbo.Nurses", "Othernames");
        }
    }
}
