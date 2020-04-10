namespace ChatAppServer.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ChatAppDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<ChatLog> ChatLogs { get; set; }
    }
}