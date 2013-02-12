using System.Data.Entity.Migrations;

namespace SyncKanban.Migrations
{
    public partial class initalschema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Board_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.Board_Id)
                .Index(t => t.Board_Id);

            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        List_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.List_Id)
                .Index(t => t.List_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Lists", "Board_Id", "dbo.Boards");
            DropForeignKey("dbo.Tasks", "List_Id", "dbo.Lists");
            DropIndex("dbo.Lists", new[] {"Board_Id"});
            DropIndex("dbo.Tasks", new[] {"List_Id"});
            DropTable("dbo.Tasks");
            DropTable("dbo.Lists");
            DropTable("dbo.Boards");
        }
    }
}