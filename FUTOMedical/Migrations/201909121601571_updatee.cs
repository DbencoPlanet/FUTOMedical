namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DeparmentId", c => c.Int());
            DropColumn("dbo.Doctors", "DeptId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "DeptId", c => c.Int());
            DropColumn("dbo.Doctors", "DeparmentId");
        }
    }
}
