namespace ChatAppServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToUsers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.Users", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Name" });
            AlterColumn("dbo.Users", "Name", c => c.String());
        }
    }
}
