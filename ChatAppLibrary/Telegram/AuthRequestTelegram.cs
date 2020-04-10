using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatAppLibrary.Telegram
{

    /// <summary>
    /// 認証要求のテレグラム
    /// </summary>
    class AuthRequestTelegram : ITelegram
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

        public Header header { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="telegram"></param>
        public AuthRequestTelegram(byte[] telegram)
        {
            this.Type = telegram[0];
            this.UserName = telegram[1].ToString();
            this.PassWord = telegram[2].ToString();
        }

        /// <summary>
        /// 受信側が受け取ったbyte配列を復元するのに使用する
        /// </summary>
        /// <returns></returns>
        public Header GetHeader()
        {
            throw new NotImplementedException();
        }

        public Body GetBody()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 送信側が必要なデータをbyte配列に変換するために使用する
        /// </summary>
        /// <returns>送信用のbyte配列</returns>
        public byte[] ToTelegramText(int type, string userName, string password)
        {
            byte[] typedata = BitConverter.GetBytes(type);
            byte[] namedata = System.Text.Encoding.UTF8.GetBytes(userName);
            byte[] passdata = System.Text.Encoding.UTF8.GetBytes(password);

            return typedata;
        }
    }
}
