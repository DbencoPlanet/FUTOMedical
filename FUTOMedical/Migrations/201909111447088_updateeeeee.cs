namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeeeee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Sex", c => c.String(nullable: false));
            DropColumn("dbo.Patients", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Patients", "Sex");
        }
    }
}
