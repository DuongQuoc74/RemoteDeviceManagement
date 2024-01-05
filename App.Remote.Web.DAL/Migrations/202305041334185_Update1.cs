namespace Jabil.Pico.Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            /*
            CreateTable(
                "dbo.CT_MIDownTimeClassification",
                c => new
                    {
                        PKClassificationID = c.Int(nullable: false, identity: true),
                        Classification = c.String(),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.PKClassificationID);
            
            CreateTable(
                "dbo.CT_Lines",
                c => new
                    {
                        PKLineID = c.Int(nullable: false, identity: true),
                        LineName = c.String(),
                        FKBuildingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PKLineID);
            
            CreateTable(
                "dbo.CT_Machines",
                c => new
                    {
                        PKMachineID = c.Int(nullable: false, identity: true),
                        MachineName = c.String(),
                        FKLineID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PKMachineID)
                .ForeignKey("dbo.CT_Lines", t => t.FKLineID, cascadeDelete: true)
                .Index(t => t.FKLineID);
            
            CreateTable(
                "dbo.CT_Tikets",
                c => new
                    {
                        PKTiketID = c.Int(nullable: false, identity: true),
                        FKMachineID = c.Int(nullable: false),
                        FKClasificationID = c.Int(nullable: false),
                        DateStop = c.DateTime(nullable: false),
                        DateRun = c.DateTime(nullable: false),
                        NewStartTime = c.DateTime(nullable: false),
                        Level = c.Int(nullable: false),
                        PartNumber = c.String(),
                        ClosedBy = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PKTiketID)
                .ForeignKey("dbo.CT_MIDownTimeClassification", t => t.FKClasificationID, cascadeDelete: true)
                .ForeignKey("dbo.CT_Machines", t => t.FKMachineID, cascadeDelete: true)
                .Index(t => t.FKMachineID)
                .Index(t => t.FKClasificationID);
            */
        }
        
        public override void Down()
        {
            /*
            DropForeignKey("dbo.CT_Tikets", "FKMachineID", "dbo.CT_Machines");
            DropForeignKey("dbo.CT_Tikets", "FKClasificationID", "dbo.CT_MIDownTimeClassification");
            DropForeignKey("dbo.CT_Machines", "FKLineID", "dbo.CT_Lines");
            DropIndex("dbo.CT_Tikets", new[] { "FKClasificationID" });
            DropIndex("dbo.CT_Tikets", new[] { "FKMachineID" });
            DropIndex("dbo.CT_Machines", new[] { "FKLineID" });
            DropTable("dbo.CT_Tikets");
            DropTable("dbo.CT_Machines");
            DropTable("dbo.CT_Lines");
            DropTable("dbo.CT_MIDownTimeClassification");
            */
        }
    }
}
