using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppServer.Models;
using ChatAppLibrary.Telegram;
using ChatAppServer.Repository;
using ChatAppServer.Service;

namespace ChatAppServer.Service
{
    /// <summary>
    /// UserRepositoryに定義しているビジネスロジックに受け渡すためのメソッド群を定義しているクラス
    /// </summary>
    public class UserService
    {
        //プロパティ
        /*UserRepositoryインスタンスはこのクラスの複数のメソッドで使うのでコンストラクタで生成されたらプロパティに格納している*/
        public UserRepository UserRepository { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
        public UserService(ChatAppDbContext dbContext)
        {
            /*UserRepositoryのインスタンスはこのクラスのすべてのメソッドで利用するのでコンストラクタ内でインスタンスを生成しプロパティに格納しておく*/
            /*DbContextはコールするごとにnewすると変更履歴が失われてしまうので○○Telegramクラスからこのクラスをコールされるときに受け取るようにする。また、このクラス内から直接DBを参照するのを防ぐためにプロパティは定義せずUserRepositoryインスタンス生成時の引数としてだけ利用する*/
            this.UserRepository = new UserRepository(dbContext);
        }


        /// <summary>
        /// RegistrationTelegramから文字列に戻されたユーザー名やハッシュ化済みのパスワードを受け取り、ユーザー名の重複がなければUserRepositoryのCreateUserメソッドへデータを受け渡す
        /// </summary>
        /// <param name="registrationData">RegistrationTelegramで文字列に戻されたユーザー名やパスワード</param>
        /// <returns>新しいUserモデルクラスのデータまたはnull</returns>
        public User Register(RegistrationTelegram registrationData)
        {
            //名前が長いので一度変数に格納する
            var userName = registrationData.GetHeader().UserName;
            var password = registrationData.PassWord;

            //パスワードはセキュリティのためPasswordServiceクラスを使ってハッシュ化する
            var hashedPassword = new PasswordService().HashPassword(password);

            //ExistUserNameで既存のユーザー名との重複を確認し新しいUserモデルクラスのデータまたはnullを返す
            if (UserRepository.ExistsUserName(userName))
            {
                //既存の名前と重複していなければユーザー名とハッシュ化済みのパスワードをCreateUserに渡す
                return UserRepository.CreateUser(userName, hashedPassword);
            }
            return null;
        }
    


        /// <summary>
        /// RegistrationTelegramで文字列に戻されたユーザー名やハッシュ化済みのパスワードをUserRepositoryクラスのAuthメソッドに渡して認証結果を得る
        /// </summary>
        /// <param name="authRequestData">AuthTelegramのインスタンス</param>
        /// <returns>Usersテーブルに該当レコードの有があればtrue、なければfalse</returns>
        public bool Auth(AuthRequestTelegram authRequestData)
        {
            //名前が長いのでAuthTeregramの各プロパティの情報を変数に格納する
            var userName = authRequestData.GetHeader().UserName;
            var password = authRequestData.PassWord;


            //新しく追加したい処理
            //userNameでUserRepository.FindByUserName()する
            //DBから取得したパスワード+ ↑のpasswordをPasswordService.verifypassword()に送信
            //return PasswordService.verifypassword()の結果(bool)を返す


            //＝＝＝＝元のAuth
            //UserRepositoryのAuthメソッドの結果を返却する(該当するユーザー名とハッシュ化済みのパスワードの組み合わせのデータがあればtrue、なければfalse)
            return UserRepository.Auth(userName, password);
        }
    }
}
