using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Telegram
{
    public interface ITelegram
    {
        Header GetHeader();

        Body GetBody();

    }

}
