namespace ChatAppServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Users", "UpdatedAt");
        }
    }
}
