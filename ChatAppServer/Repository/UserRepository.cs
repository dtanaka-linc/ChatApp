using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatAppServer.Models;

namespace ChatAppServer.Repository
{
    /// <summary>
    /// Userモデルに+アクセスし新規登録や認証を行うメソッド群
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
        /// 名前とパスワードをUsersに追加し、Userクラス型のデータとして返す
        /// </summary>
        /// <param name="userName">ログインフォームで入力されたユーザー名</param>
        /// <param name="password">ログインフォームで入力されたパスワード</param>
        /// <returns>Userクラス型のデータ</returns>
        public User CreateUser(string userName, string password)
        {
            var user = new User()
            {
                Name = userName,
                Password = password
            };
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return user;
        }

        // UserServiceのAuthメソッドから検索させるために新しくメソッドを追加しました
        /// <summary>
        ///
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IQueryable<User> Auth(string userName, string password)
        {
            return this.DbContext.Users.Where(r => r.Name == userName).Where(r => r.Password == password);
        }


        /// <summary>
        /// 新規登録フォームで入力されたユーザー名と同名のレコードが存在するかどうかを確認する
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>入力されたユーザー名の同名のレコードの有無</returns>
        public bool ExistsUserName(string userName)
        {
            return this.DbContext.Users.Any(r => r.Name == userName);
        }

    }
}
