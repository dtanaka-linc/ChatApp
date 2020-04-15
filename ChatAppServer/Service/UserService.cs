using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppServer.Models;
using ChatAppLibrary.Telegram;
using ChatAppServer.Repository;

namespace ChatAppServer.Service
{
    /// <summary>
    /// UserRepositoryに定義しているビジネスロジックに受け渡すためのメソッド群を定義しているクラス
    /// </summary>
    public class UserService
    {
        //UserRepostirotyのインスタンスに渡すChatAppDbContextの宣言はこれでよいでしょうか？
        private ChatAppDbContext _context = new ChatAppDbContext();

        private ChatAppDbContext DbContext { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserService(ChatAppDbContext DbContext)
        {
            this.DbContext = DbContext;
        }


        /// <summary>
        /// RegistrationTelegramから文字列に戻されたユーザー名やパスワードを受け取り、ユーザー名の重複がなければUserRepositoryのCreateUserメソッドへデータを受け渡す
        /// </summary>
        /// <param name="registrationData">RegistrationTelegramで文字列に戻されたユーザー名やパスワード</param>
        /// <returns>新しいUserモデルクラスのデータまたはnull</returns>
        public User Register(RegistrationTelegram registrationData)
        {
            var userName = registrationData.GetHeader().UserName;
            var PassWord = registrationData.PassWord;
            UserRepository userRepository = new UserRepository(_context);　//コンストラクタとしてChatAppDbContextを設定していますがどのように渡せばよいかがわかりません...

            //ExistUserNameで既存のユーザー名との重複を確認し新しいUserモデルクラスのデータまたはnullを返す
            if (userRepository.ExistsUserName(userName)){
                return userRepository.CreateUser(userName, PassWord);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// RegistrationTelegramで文字列に戻されたユーザー名やパスワードを受けとってUserRepositoryクラスのAuthメソッドに渡して認証結果を得る
        /// </summary>
        /// <param name="authRequestData">AuthTelegramのインスタンス</param>
        /// <returns>Usersテーブルに該当レコードの有があればtrue、なければfalse</returns>
        public bool Auth(AuthRequestTelegram authRequestData)
        {
            UserRepository userRepository = new UserRepository(_context);

            //名前が長いのでAuthTeregramの各プロパティの情報を変数に格納する
            var userName = authRequestData.GetHeader().UserName;
            var password = authRequestData.PassWord;

            //UserRepositoryのAuthメソッドの結果を格納する変数
            var authResult = userRepository.Auth(userName, password);

            //Authメソッドで該当するレコードがあればtrue、なければfalseを返す
            if (authResult != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
