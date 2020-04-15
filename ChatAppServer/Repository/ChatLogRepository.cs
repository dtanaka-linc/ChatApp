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


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
        public ChatLogRepository(ChatAppDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// ChatLogServiceから渡されたユーザー名と投稿内容をChatLogsに保存する
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="msg">会話内容</param>
        public void CreateChatLog(string userName, string msg)
        {
            var chatLog = new ChatLog()
            {
                //作業メモ：ユーザー名はuserIdにしないといけない...最初からユーザーIDでもらうべきかここで検索して格納すべきか？
                Body = msg
            };
            DbContext.ChatLogs.Add(chatLog);
            DbContext.SaveChanges();
        }
    }
}