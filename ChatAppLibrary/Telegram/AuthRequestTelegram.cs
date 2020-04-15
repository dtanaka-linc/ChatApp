using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    /// <summary>
    /// 認証要求のテレグラム
    /// </summary>
    public class AuthRequestTelegram : ITelegram
    {
        /// <summary>
        /// 各テレグラムの共通部分
        /// </summary>
        private Header header { get; set; } = new Header();

        /// <summary>
        /// パスワード
        /// </summary>
        public string PassWord { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="telegram"></param>
        public AuthRequestTelegram(byte[] telegram)
        {
            // 受け取ったbyte[]をstringに戻す
            var strTelegram = System.Text.Encoding.UTF8.GetString(telegram);

            // ,区切りして配列に格納する
            var telegramArr = strTelegram.Split(',');

            // 各プロパティに値を格納
            header.Type = Convert.ToInt32(telegramArr[0]);
            header.UserName = telegramArr[1].ToString();
            this.PassWord = telegramArr[2].ToString();
        }

        /// <summary>
        /// 共通部分の参照に使用
        /// </summary>
        /// <returns>header</returns>
        public Header GetHeader()
        {
            return this.header;
        }

        public Body GetBody()
        {
            throw new NotImplementedException();
        }


    }
}
