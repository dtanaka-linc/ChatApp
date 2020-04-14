using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    /// <summary>
    /// 登録機能のテレグラム
    /// </summary>
    public partial class RegistrationTelegram : ITelegram
    {
        /// <summary>
        /// 各テレグラムの共通部分
        /// </summary>
        public Header header { get; set; } = new Header();

        /// <summary>
        /// パスワード
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="telegram"></param>
        public RegistrationTelegram(byte[] telegram)
        {
            // 受け取ったbyte配列をstringに戻す
            var strTelegram = System.Text.Encoding.UTF8.GetString(telegram);

            // ,区切りして配列に格納する
            var telegramArr = strTelegram.Split(',');

            this.header.Type = Convert.ToInt32(telegramArr[0]);
            this.header.UserName = telegramArr[1].ToString();
            this.PassWord = telegramArr[2].ToString();
        }

        public Header GetHeader()
        {
            return header;
        }

        public Body GetBody()
        {
            throw new NotImplementedException();
        }
    }
}
