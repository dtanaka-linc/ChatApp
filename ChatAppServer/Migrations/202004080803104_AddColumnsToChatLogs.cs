namespace ChatAppServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsToChatLogs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatLogs", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ChatLogs", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatLogs", "CreatedAt");
            DropColumn("dbo.ChatLogs", "UpdatedAt");
        }
    }
}
