using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    /// <summary>
    /// チャット機能のテレグラム
    /// </summary>
    public class ChatTelegram : ITelegram
    {
        /// <summary>
        /// 各テレグラムの共通部分
        /// </summary>
        private Header header { get; set; } = new Header();

        /// <summary>
        /// 会話内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="telegram"></param>
        public ChatTelegram(byte[] telegram)
        {
            // 受け取ったbyte配列をstringに戻す
            var strTelegram = System.Text.Encoding.UTF8.GetString(telegram);

            // ,区切りして配列に格納する
            var telegramArr = strTelegram.Split(',');

            this.header.Type = Convert.ToInt32(telegramArr[0]);
            this.header.UserName = telegramArr[1].ToString();
            this.Message = telegramArr[2].ToString();
        }

        public Header GetHeader()
        {
            return header; ;
        }

        public Body GetBody()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 送信用のテレグラムを返す処理
        /// </summary>
        /// <returns>必要な項目を結合したstring</returns>
        public string ToTelegramText()
        {
            var strArray = new[] { this.GetHeader().Type.ToString(), GetHeader().UserName, this.Message };

            var sendtext = string.Join(", ", strArray);

            return sendtext;
        }
    }
}
