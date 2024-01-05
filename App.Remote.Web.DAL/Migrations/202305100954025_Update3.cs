namespace Jabil.Pico.Web.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            /*
            CreateTable(
                "dbo.PI_Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            */
        }
        
        public override void Down()
        {
            /*
            DropTable("dbo.PI_Settings");
            */
        }
    }
}
