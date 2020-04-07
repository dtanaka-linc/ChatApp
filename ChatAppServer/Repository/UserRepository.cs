using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatAppServer.Models;

namespace ChatAppServer.Repository
{
    /// <summary>
    /// Userエンティティにアクセスし新規登録や認証を行うメソッド群です。
    /// </summary>
    public class UserRepository
    {
        private ChatAppDbContext DbContext { get; set; }

        //コンストラクタ
        public UserRepository(ChatAppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// 新規ユーザー登録を行います。※名前確認部分の切り離しはこれからおこないます
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void CreateUser(string name, string password)
        {
            var isUserName = ExistsUserName(name);

            if (isUserName)
            {
                var user = new User()
                {
                    Name = name,
                    Password = password
                };
                DbContext.Users.Add(user);
                DbContext.SaveChanges();
            }
        }


        /// <summary>
        /// ユーザー名で検索する
        /// </summary>
        public IQueryable<User> FindByUserName(string userName)
        {
            return this.DbContext.Users.Where(r => r.Name == userName);
        }


        /// <summary>
        /// 入力されたユーザー名と同名のレコードが存在するかどうか確認する
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistsUserName(string userName)
        {
            return this.DbContext.Users.Any(r => r.Name == userName);
        }

    }
}
