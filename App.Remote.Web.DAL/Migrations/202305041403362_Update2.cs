namespace Jabil.Pico.Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            /*
            AlterColumn("dbo.CT_Tikets", "DateRun", c => c.DateTime());
            AlterColumn("dbo.CT_Tikets", "NewStartTime", c => c.DateTime());
            AlterColumn("dbo.CT_Tikets", "Level", c => c.Int());
            */
        }
        
        public override void Down()
        {
            /*
            AlterColumn("dbo.CT_Tikets", "Level", c => c.Int(nullable: false));
            AlterColumn("dbo.CT_Tikets", "NewStartTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CT_Tikets", "DateRun", c => c.DateTime(nullable: false));
            */
        }
    }
}
