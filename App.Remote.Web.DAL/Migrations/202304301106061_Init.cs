namespace Jabil.Pico.Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.CT_UsersRols",
            //    c => new
            //        {
            //            PKRegisterID = c.Int(nullable: false, identity: true),
            //            EmployeeNumber = c.String(),
            //            WindowsNT = c.String(),
            //            FKRolID = c.Int(nullable: false),
            //            Available = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PKRegisterID);
        }
        
        public override void Down()
        {
            //DropTable("dbo.CT_UsersRols");
        }
    }
}
