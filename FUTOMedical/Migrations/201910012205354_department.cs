namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class department : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BloodBanks", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Departments", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
            DropColumn("dbo.BloodBanks", "Status");
        }
    }
}
