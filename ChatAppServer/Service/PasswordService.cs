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
        public void VerifyPassword()
        {

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
