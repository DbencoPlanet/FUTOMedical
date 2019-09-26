namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Nationality", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Nationality");
        }
    }
}
