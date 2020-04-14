using ChatAppLibrary.Telegram;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.TelegramService
{
    public class TelegramRogic
    {
        public static ITelegram GetTelegram(byte[] reciveTelegram)
        {
            switch (reciveTelegram[0].ToString())
            {
                // 認証要求
                case "1":
                    return new AuthRequestTelegram(reciveTelegram);

                // 登録要求    
                case "2":
                    return new RegistrationTelegram(reciveTelegram);

                // チャット
                case "3":
                    return new ChatTelegram(reciveTelegram);

                // 認証応答
                case "4":
                    return new AuthResponseTelegram(reciveTelegram);

                // 登録応答
                case "5":
                    return new RegistrationResponceTelegram(reciveTelegram);

                default:
                    throw new Exception();
            }

        }
    }
}

