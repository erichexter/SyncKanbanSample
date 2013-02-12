namespace MvcApplication22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listordering : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lists", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lists", "Order");
        }
    }
}
