using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppLibrary.Telegram;
using Microsoft.AspNet.Identity;

namespace ChatAppServer.Service
{
    public class PasswordService
    {
        //Authテレグラムから受け取ったパスワードがハッシュ化されたものと一緒か確かめる
        //UserRepository.Authから呼ばれる。DBからゲットしたパスワード(ハッシュ)と入力された平文を照合する
        //入力された平文はこのクラスのHashPasswordで変換されてから比較される
        //return パスワードが合えばtrue, あわなかったりnullだったらfalseを返す
        public bool VerifyPassword() //引数は2つ　AuthTelegram→UserService.Auth()にきたパスワード　とUserRepositoryで名前検索したパスワード
        {
            //if分
            //DBからゲットしたパスワード = HashPassword
            return true;
        }

        //平文のパスワード(string)を受けとりハッシュ化して返す
        //returnもstring
        public string HashPassword(string password)
        {
            var hashedPassword = new PasswordHasher().HashPassword(password);
            return hashedPassword;
        }
    }
}
