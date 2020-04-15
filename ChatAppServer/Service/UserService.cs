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
        //
        private ChatAppDbContext DbContext { get; set; }
        public UserRepository userRepository { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="DbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
        public UserService(ChatAppDbContext DbContext)
        {
            /*クラスごとにDbContextをnewすると変更履歴が失われてしまうので○○Telegramクラスからコールされるときに受けとって設定する*/
            this.DbContext = DbContext;
            /*UserRepositoryのインスタンスはこのクラスのすべてのメソッドで利用するのでコンストラクタ内でインスタンスを生成しプロパティに格納しておく*/
            this.userRepository = new UserRepository(this.DbContext);
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

            //ExistUserNameで既存のユーザー名との重複を確認し新しいUserモデルクラスのデータまたはnullを返す
            if (userRepository.ExistsUserName(userName))
            {
                return userRepository.CreateUser(userName, PassWord);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// RegistrationTelegramで文字列に戻されたユーザー名やパスワードをUserRepositoryクラスのAuthメソッドに渡して認証結果を得る
        /// </summary>
        /// <param name="authRequestData">AuthTelegramのインスタンス</param>
        /// <returns>Usersテーブルに該当レコードの有があればtrue、なければfalse</returns>
        public bool Auth(AuthRequestTelegram authRequestData)
        {
            //名前が長いのでAuthTeregramの各プロパティの情報を変数に格納する
            var userName = authRequestData.GetHeader().UserName;
            var password = authRequestData.PassWord;

            //UserRepositoryのAuthメソッドの結果を格納する変数　代入しなくていいかな？？
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
