using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppLibrary.Telegram;
using Microsoft.AspNet.Identity;

namespace ChatAppServer.Service
{
    /// <summary>
    /// パスワードのハッシュ化と照合に関するメソッド群をまとめたクラス
    /// </summary>
    public class PasswordService
    {

        /// <summary>
        /// ハッシュ化済みの登録パスワードと平文のユーザーが入力したパスワードが一致するかどうかを確かめる
        /// </summary>
        /// <param name="hashedPassword">ハッシュ化済みのパスワード(DBから取得)</param>
        /// <param name="normalPassword">ユーザーが入力した平文のパスワード</param>
        /// <returns>一致すればtrue、一致しなければfalse</returns>
        public bool VerifyPassword(string hashedPassword, string normalPassword)
        {
            return hashedPassword == ToHashPassword(normalPassword);
        }


        /// <summary>
        /// 平文のパスワードをハッシュ化する
        /// </summary>
        /// <param name="normalPassword">平文(string)のパスワード</param>
        /// <returns>ハッシュ化したパスワード</returns>
        public string ToHashPassword(string normalPassword)
        {
            var hashedPassword = new PasswordHasher().HashPassword(normalPassword);
            return hashedPassword;
        }
    }
}
