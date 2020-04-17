using ChatAppLibrary.Constants;
using ChatAppLibrary.Telegram;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.TelegramService
{
    public class TelegramLogic
    {
        public static ITelegram GetTelegram(byte[] reciveTelegram)
        {

            switch (reciveTelegram[0].ToString())
            {
                // 認証要求
                case ProcessType.AuthRequest:
                    return new AuthRequestTelegram(reciveTelegram);

                // 登録要求    
                case ProcessType.RegisterRequest:
                    return new RegistrationTelegram(reciveTelegram);

                // チャット
                case ProcessType.Chat:
                    return new ChatTelegram(reciveTelegram);

                // 認証応答
                case ProcessType.AuthResponce:
                    return new AuthResponseTelegram(reciveTelegram);

                // 登録応答
                case ProcessType.RegisterResponce:
                    return new RegistrationResponceTelegram(reciveTelegram);

                default:
                    throw new Exception();
            }

        }
    }
}

