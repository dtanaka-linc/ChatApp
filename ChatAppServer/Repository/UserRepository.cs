using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatAppServer.Models;

namespace ChatAppServer.Repository
{
    /// <summary>
    /// Userモデルにアクセスし新規登録や認証を行うメソッド群をもつクラス
    /// </summary>
    public class UserRepository
    {
        //プロパティ
        private ChatAppDbContext DbContext { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
        public UserRepository(ChatAppDbContext dbContext)
        {

            /*DbContextクラスは複数回newすると変更履歴が消えてしまうため、UserServiceからChatAppDbContextクラスのインスタンスを受け継いてDbContextプロパティに設定する*/
            //※このクラスはDBにアクセスするクラスなのでDbContextプロパティにインスタンスを格納して利用する
            /*クラスごとにDbContextをnewすると変更履歴が失われてしまうのでUserServiceからコールされるときに受けとって設定している*/
            this.DbContext = dbContext;
        }


        /// <summary>
        /// 名前とパスワードをUsersテーブルに追加し、Userモデルクラスのデータとして返す
        /// </summary>
        /// <param name="userName">UserServiceクラスのRegisterから渡されたユーザー名</param>
        /// <param name="hashedPassword">UserServiceクラスのRegisterから渡されたパスワード</param>
        /// <returns>Userモデル型のデータ</returns>
        public User CreateUser(string userName, string hashedPassword)
        {
            var user = new User()
            {
                //各プロパティ(カラム)に該当する情報を格納する
                Name = userName,
                Password = hashedPassword
            };
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return user;
        }

        /// <summary>
        /// 一意のユーザー名でユーザー情報を検索する
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <returns>ヒットしたUsersのレコード(Userモデルクラス型)</returns>
        public User FindByUserName(string userName)
        {
            //ユーザー名が一致したデータが1件ならそのデータ(Userモデルクラス型)、そうでない場合は例外
            try
            {
                return this.DbContext.Users.Where(r => r.Name == userName).SingleOrDefault();
            }
            //データが複数ある場合の例外 この処理の仕方でいいかな・・・？
            catch(InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        /// <summary>
        /// UserServiceのResisterメソッドから渡された新規ユーザー名が既存のNameカラムのレコードと重複しているかを確認する/// </summary>
        /// <param name="userName">UserServiceのResisterメソッドから渡された新規ユーザー名</param>
        /// <returns>重複している場合true, していなければfalse</returns>
        public bool ExistsUserName(string userName)
        {
            return this.DbContext.Users
                .Any(r => r.Name == userName);
        }
    }
}
