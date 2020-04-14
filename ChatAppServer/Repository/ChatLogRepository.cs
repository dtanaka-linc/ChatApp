using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppServer.Models;

namespace ChatAppServer.Repository
{
    /// <summary>
    /// ChatLogモデルにアクセスしてチャットログの保存をするクラス
    /// </summary>
    public class ChatLogRepository
    {
        private ChatAppDbContext DbContext { get; set; }


        //コンストラクタ
        public ChatLogRepository(ChatAppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// チャットログを保存する
        /// </summary>
        /// <param name="body">投稿内容</param>
        public void CreateChatLog(string body)
        {
            var chatLog = new ChatLog()
            {
                Body = body
            };
            DbContext.ChatLogs.Add(chatLog);
            DbContext.SaveChanges();
        }
    }
}