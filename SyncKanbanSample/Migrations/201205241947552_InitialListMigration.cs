namespace Sample.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialListMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Lists",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        List_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Lists", t => t.List_id)
                .Index(t => t.List_id);
            
        }
        
        public override void Down()
        {
            DropIndex("Tasks", new[] { "List_id" });
            DropForeignKey("Tasks", "List_id", "Lists");
            DropTable("Tasks");
            DropTable("Lists");
        }
    }
}
