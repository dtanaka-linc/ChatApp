using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppServer.Models;
using ChatAppServer.Repository;

namespace ChatAppServer.Repository
{
    /// <summary>
    /// ChatLogモデルにアクセスしてチャットログの保存をするクラス
    /// </summary>
    public class ChatLogRepository
    {
        //プロパティ
        private ChatAppDbContext DbContext { get; set; }
        public UserRepository UserRepository { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
        public ChatLogRepository(ChatAppDbContext dbContext)
        {
            /*DbContextクラスは複数回newすると変更履歴が消えてしまうため、ChatLogServiceからChatAppDbContextクラスのインスタンスを受け継いてDbContextプロパティに設定する*/
            //※このクラスはDBにアクセスするクラスなのでDbContextプロパティにインスタンスを格納して利用する
            this.DbContext = dbContext;
            //Usersテーブルで検索して情報を得る処理が複数追加されることを想定してUserRepositoryのインスタンスをコンストラクタ内で生成しておく。
            this.UserRepository = new UserRepository(this.DbContext);
        }


        /// <summary>
        /// ChatLogServiceから渡されたユーザー名と投稿内容をChatLogsに保存する
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="msg">会話内容</param>
        public void CreateChatLog(string userName, string msg)
        {
            //UserIdカラムにユーザーIDの情報を格納するために、ユーザー名から検索してUsersテーブルのIDを取得する
            //ユーザー名が合致したUsersのレコードを取得してuserに格納する
            var user = UserRepository.FindByUserName(userName);

            var chatLog = new ChatLog()
            {
                //ChatLogクラスの各プロパティに該当する情報を格納する
                UserId = user.Id,
                Body = msg
            };
            DbContext.ChatLogs.Add(chatLog);
            DbContext.SaveChanges();
        }
    }
}