namespace ChatAppLibrary.Telegram
{
    /// <summary>
    /// 各処理に共通する部分
    /// </summary>
    public class Header
    {
        /// <summary>
        /// 処理種別
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; }


    }
}