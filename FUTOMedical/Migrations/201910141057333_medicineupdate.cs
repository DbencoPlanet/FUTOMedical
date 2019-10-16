namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicineupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicineCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MedicineCategoryId = c.Int(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManufacturedBy = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicineCategories", t => t.MedicineCategoryId)
                .Index(t => t.MedicineCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicines", "MedicineCategoryId", "dbo.MedicineCategories");
            DropIndex("dbo.Medicines", new[] { "MedicineCategoryId" });
            DropTable("dbo.Medicines");
            DropTable("dbo.MedicineCategories");
        }
    }
}
