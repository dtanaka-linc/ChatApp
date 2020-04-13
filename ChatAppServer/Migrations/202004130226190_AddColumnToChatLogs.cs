namespace ChatAppServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnToChatLogs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatLogs", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ChatLogs", "UserId");
            AddForeignKey("dbo.ChatLogs", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatLogs", "UserId", "dbo.Users");
            DropIndex("dbo.ChatLogs", new[] { "UserId" });
            DropColumn("dbo.ChatLogs", "UserId");
        }
    }
}
