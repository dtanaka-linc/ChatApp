﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    /// <summary>
    /// 認証要求のテレグラム
    /// </summary>
    public class AuthResponseTelegram : ITelegram
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
        public AuthResponseTelegram(byte[] telegram)
        {
            // 受け取ったbyte配列をstringに戻す
            var strTelegram = System.Text.Encoding.UTF8.GetString(telegram);

            // ,区切りして配列に格納する
            var telegramArr = strTelegram.Split(',');

            this.header.Type = Convert.ToInt32(telegramArr[0]);
            this.header.UserName = telegramArr[1].ToString();

        }


        public Header GetHeader()
        {
            return header;
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
            var strArray = new[] { GetHeader().Type.ToString(), GetHeader().UserName, AuthResult.ToString() };

            var sendtext = string.Join(", ", strArray);

            return sendtext;
        }
    }
}
