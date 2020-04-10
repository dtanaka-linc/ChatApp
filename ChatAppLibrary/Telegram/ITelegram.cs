using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    interface ITelegram
    {
        Header GetHeader();
        Body GetBody();

    }

}
