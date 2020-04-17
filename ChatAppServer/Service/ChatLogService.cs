using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAppServer.Repository;
using ChatAppServer.Models;
using ChatAppLibrary.Telegram;


namespace ChatAppServer.Service
{
    public class ChatLogService
    {
        
            //プロパティ
            /*ChatLogReopsitoryのインスタンスをコンストラクタによって格納する。現状はメソッドが一つだけだが、今後追加される可能性も考えてプロパティにしておいた*/
            public ChatLogRepository ChatLogRepository { get; set; }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="dbContext">データベースの接続やエンティティの管理を担当するChatAppDbContextクラスのインスタンス</param>
            public ChatLogService(ChatAppDbContext dbContext)
            {
                /*UserRepositoryのインスタンスはこのクラスのすべてのメソッドで利用するのでコンストラクタ内でインスタンスを生成しプロパティに格納しておく*/
                /*DbContextはコールするごとにnewすると変更履歴が失われてしまうので○○Telegramクラスからこのクラスをコールされるときに受け取るようにする。また、このクラス内から直接DBを参照するのを防ぐためにプロパティは定義せずChatLogRepositoryインスタンス生成時の引数としてだけ利用する*/
                this.ChatLogRepository = new ChatLogRepository(dbContext);
            }


            /// <summary>
            /// ChatTelegramから文字列に戻されたユーザー名や投稿内容をを受け取りChatLogRepositoryのCreateChatLogへ渡す
            /// </summary>
            /// <param name="registrationData">ChatTelegramで文字列に戻されたユーザー名や投稿内容</param>
            /// <returns></returns>
            public void Register(ChatTelegram registrationData)
            {
                //名前が長いのでChatTeregramの各プロパティの情報を変数に格納する
                var userName = registrationData.GetHeader().UserName;
                var msg = registrationData.Message;
                
                //ビジネスロジック分離のためCreateChatLogにデータを渡す
                ChatLogRepository.CreateChatLog(userName, msg);
            }
        
    }
}
