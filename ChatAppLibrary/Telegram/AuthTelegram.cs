using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    class AuthTelegram : ITelegram
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
        /// パスワード
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="telegram"></param>
        public AuthTelegram(byte[] telegram)
        {
            this.Type = telegram[0];
            this.UserName = telegram[1].ToString();
            this.PassWord = telegram[2].ToString();
        }

        public void GetHeader()
        {

        }

        public void GetBody()
        {

        }
    }
}
