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
    public class UserService
    {
        //UserRepostirotyのインスタンスに渡すChatAppDbContextの宣言はこれでよいでしょうか？
        private ChatAppDbContext _context = new ChatAppDbContext();

        /// <summary>
        /// RegistrationTelegramから文字列に戻されたユーザー名やパスワードを受け取り、ユーザー名の重複がなければUserRepositoryのCreateUserメソッドへデータを受け渡す
        /// </summary>
        /// <param name="registrationData">RegistrationTelegramで文字列に戻されたユーザー名やパスワード</param>
        /// <returns>新しいUserモデル型のデータまたはnull</returns>
        public User Register(RegistrationTelegram registrationData)
        {
            var userName = registrationData.header.UserName;
            var PassWord = registrationData.PassWord;
            UserRepository userRepository = new UserRepository(_context);　//コンストラクタとしてChatAppDbContextを設定していますがどのように渡せばよいかがわかりません...

            //ExistUserNameで既存のユーザー名との重複を確認し新しいUserモデル型のデータまたはnullを返す
            if (userRepository.ExistsUserName(userName)){
                return userRepository.CreateUser(userName, PassWord);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// RegistrationTelegramから文字列に戻されたユーザー名やパスワードを受け取り、データの照会結果をboolで返す
        /// </summary>
        /// <param name="authRequestData"></param>
        public void Auth(AuthRequestTelegram authRequestData)
        {
            UserRepository userRepository = new UserRepository(_context);

            var userName = authRequestData.header.UserName;
            var password = authRequestData.PassWord;
            userRepository.Auth(userName, password);
        }
    }
}
