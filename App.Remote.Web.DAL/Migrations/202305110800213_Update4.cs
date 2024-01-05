namespace Jabil.Pico.Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            /*
            CreateTable(
                "dbo.PI_ApiLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Api = c.String(),
                        Request = c.String(),
                        Response = c.String(),
                        MachineId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            */
        }
        
        public override void Down()
        {
            /*
            DropTable("dbo.PI_ApiLogs");
            */
        }
    }
}
