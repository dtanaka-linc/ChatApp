using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    /// <summary>
    /// 登録機能のテレグラム
    /// </summary>
    public class RegistrationResponceTelegram : ITelegram
    {
        /// <summary>
        /// 各テレグラムの共通部分
        /// </summary>
        private Header header { get; set; } = new Header();

        /// <summary>
        /// 認証結果
        /// </summary>
        public bool AuthResult { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="telegram"></param>
        public RegistrationResponceTelegram(byte[] telegram)
        {
            // 受け取ったbyte配列をstringに戻す
            var strTelegram = System.Text.Encoding.UTF8.GetString(telegram);

            // ,区切りして配列に格納する
            var telegramArr = strTelegram.Split(',');

            this.header.Type = Convert.ToInt32(telegramArr[0]);
            this.header.UserName = telegramArr[1].ToString();
            this.AuthResult = System.Convert.ToBoolean(telegramArr[2]);
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
