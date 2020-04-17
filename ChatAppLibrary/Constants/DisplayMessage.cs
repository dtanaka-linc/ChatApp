using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppLibrary.Constants
{
    /// <summary>
    /// メッセージ表示の定数クラス
    /// </summary>
    public static class DisplayMessage
    {
        public static readonly string MESSAGE_AUTH_FAILED = "ユーザー名かパスワードが間違っています";

        public static readonly string MESSAGE_REGISTRATION_FAILED = "すでに登録済みです";

        public static readonly string CONFIRMATION_RESULT = "確認結果";

        public static readonly string REGISTERD = "登録完了しました";

        public static readonly string WARNING = "警告";

        public static readonly string PASSWORD_MISSMATCH = "入力したパスワードが一致しません";

        public static readonly string INPUT_REQUIRED = "ユーザー名を入力してください";
    }
}
