using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatAppServer.Models;

namespace ChatAppServer.Repository
{
    /// <summary>
    /// Userモデルにアクセスし新規登録や認証を行うメソッド群です。
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
        /// 名前とパスワードをUsersに追加し、新しくできたUser型のデータを返す
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User CreateUser(string name, string password)
        {
            var user = new User()
            {
                Name = name,
                Password = password
            };
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return user;
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
