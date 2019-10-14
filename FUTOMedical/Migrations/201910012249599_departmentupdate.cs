namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentupdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Doctors", name: "Department_Id", newName: "DepartmentId");
            RenameIndex(table: "dbo.Doctors", name: "IX_Department_Id", newName: "IX_DepartmentId");
            DropColumn("dbo.Doctors", "DeparmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "DeparmentId", c => c.Int());
            RenameIndex(table: "dbo.Doctors", name: "IX_DepartmentId", newName: "IX_Department_Id");
            RenameColumn(table: "dbo.Doctors", name: "DepartmentId", newName: "Department_Id");
        }
    }
}
