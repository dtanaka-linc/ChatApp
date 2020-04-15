using ChatAppLibrary.Const;
using ChatAppLibrary.Telegram;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.TelegramService
{
    public class TelegramRogic
    {
        /// <summary>
        /// 認証要求
        /// </summary>
        private const string AuthRequest = "49";

        /// <summary>
        /// 登録要求
        /// </summary>
        private const string RegisterRequest = "50";

        /// <summary>
        /// チャット
        /// </summary>
        private const string Chat = "51";

        /// <summary>
        /// 認証応答
        /// </summary>
        private const string AuthResponce = "52";

        /// <summary>
        /// 登録応答
        /// </summary>
        private const string RegisterResponce = "53";

        public static ITelegram GetTelegram(byte[] reciveTelegram)
        {

            switch (reciveTelegram[0].ToString())
            {
                // 認証要求
                case AuthRequest:
                    return new AuthRequestTelegram(reciveTelegram);

                // 登録要求    
                case RegisterRequest:
                    return new RegistrationTelegram(reciveTelegram);

                // チャット
                case Chat:
                    return new ChatTelegram(reciveTelegram);

                // 認証応答
                case AuthResponce:
                    return new AuthResponseTelegram(reciveTelegram);

                // 登録応答
                case RegisterResponce:
                    return new RegistrationResponceTelegram(reciveTelegram);

                default:
                    throw new Exception();
            }

        }
    }
}

