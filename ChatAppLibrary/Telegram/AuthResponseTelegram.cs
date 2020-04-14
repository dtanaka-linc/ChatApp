using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{

    /// <summary>
    /// 認証要求のテレグラム
    /// </summary>
    class AuthResponseTelegram : ITelegram
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
        /// 認証結果
        /// </summary>
        public bool AuthResult { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="telegram"></param>
        public AuthResponseTelegram(byte[] telegram)
        {
            this.Type = telegram[0];
            this.UserName = telegram[1].ToString();
            this.AuthResult = System.Convert.ToBoolean(telegram[2].ToString());
        }


        public Header GetHeader()
        {
            throw new NotImplementedException();
        }

        public Body GetBody()
        {
            throw new NotImplementedException();
        }
    }
}
