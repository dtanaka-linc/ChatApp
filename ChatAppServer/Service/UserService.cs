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
        /// <summary>
        /// RegistrationTelegramから文字列に戻されたユーザー名やパスワードを受け取り、ユーザー名の重複がなければUserRepositoryのCreateUserメソッドへデータを受け渡す
        /// </summary>
        /// <param name="registrationData">RegistrationTelegramで文字列に戻されたユーザー名やパスワード</param>
        /// <returns></returns>
        public User Register(RegistrationTelegram registrationData)   //RegistrationTelegramがpublicになっていないため
        {
            private string userName = resistrationData.header.UserName;
            private string PassWord = resistrationData.PassWord;
        //ユーザー名の重複がなければCreateUserにデータを渡し新規追加したUsersのデータを返す(なんか違和感がある...)
            if(UserRepository.ExistUserName(userName)){
                return UserRepository.CreateUser(userName, password);
            }
        }

        public void Auth(AuthRequestTelegram authRequestData)
        {
            private string userName = authRequestData.header.UserName;
            private string password = authRequestData.Password;
            UserRepository.Auth(userName, password);
        }
    }
}
