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
        ///UserServiceのResisterメソッドから渡されたユーザー名・パスワードの組み合わせが存在するかどうかを確認する
        /// </summary>
        ///<param name="userName">ユーザー名</param>
        ///<param name="password">パスワード</param>
        /// <returns></returns>
        public bool Auth(string userName, string password)
        {
            //二つのカラムのが条件にあっているかを確認するための記述がわからない...
            return this.DbContext.Users.All(r => r.Name == userName).Where(r => r.Password == password);
        }


        /// <summary>
        /// UserServiceのResisterメソッドから渡されたユーザー名と同名のレコードが存在するかどうかを確認する
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>入力されたユーザー名の同名のレコードの有無</returns>
        public bool ExistsUserName(string userName)
        {
            return this.DbContext.Users.Any(r => r.Name == userName);
        }

    }
}
