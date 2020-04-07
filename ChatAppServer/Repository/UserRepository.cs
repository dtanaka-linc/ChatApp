using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatAppServer.Models;

namespace ChatAppServer.Repository
{
    class UserRepository
    {
        private ChatAppDbContext DbContext { get; set; }

        //コンストラクタ
        public UserRepository(ChatAppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        //新規ユーザー登録
        public void CreateUser(string name, string password)
        {
            var user = new User()
            {
                Name = name,
                Password = password
            };
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
        }

        //ユーザー名検索
        public IQueryable<User> FindByUserName(string userName)
        {
            return this.DbContext.Users.Where(r => r.Name == userName);
        }
    }
}
