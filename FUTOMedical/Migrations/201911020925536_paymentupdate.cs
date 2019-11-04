namespace FUTOMedical.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(),
                        InvoiceId = c.Int(),
                        PaymentDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Payments", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Payments", new[] { "InvoiceId" });
            DropIndex("dbo.Payments", new[] { "PatientId" });
            DropTable("dbo.Payments");
        }
    }
}
