using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Constants
{
    /// <summary>
    /// 処理種別の定数クラス
    /// </summary>
    class ProcessType
    {
        /// <summary>
        /// 認証要求
        /// </summary>
        public const string AuthRequest = "49";

        /// <summary>
        /// 登録要求
        /// </summary>
        public const string RegisterRequest = "50";

        /// <summary>
        /// チャット
        /// </summary>
        public const string Chat = "51";

        /// <summary>
        /// 認証応答
        /// </summary>
        public const string AuthResponce = "52";

        /// <summary>
        /// 登録応答
        /// </summary>
        public const string RegisterResponce = "53";
    }
}
