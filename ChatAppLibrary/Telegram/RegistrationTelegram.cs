using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    /// <summary>
    /// 登録機能のテレグラム
    /// </summary>
    class RegistrationTelegram : ITelegram
    {
        /// <summary>
        /// 処理種別
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 会話内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="telegram"></param>
        public RegistrationTelegram(byte[] telegram)
        {
            this.Type = telegram[0];
            this.UserName = telegram[1].ToString();
            this.Message = telegram[2].ToString();
        }

        public Header GetHeader()
        {
            Header header = new Header();

            header.UserName = this.UserName;

            return header;
        }

        public Body GetBody()
        {
            throw new NotImplementedException();
        }
    }
}
