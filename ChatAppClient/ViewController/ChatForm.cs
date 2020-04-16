using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using ChatAppClient.Service;
using System.Threading;
using ChatAppLibrary.Telegram;
using ChatAppLibrary.TelegramService;

namespace ChatAppClient.ViewController
{
    public partial class ChatForm : Form
    {
        //サービス
        ClientService clientService = new ClientService();

        //確認用　設定は後で外部ファイル管理する！
        String host = "localhost";
        int port = 2001;

        /// <summary>
        /// 処理種別
        /// </summary>
        private const int ProcessNumber = 3;

        /// <summary>
        /// ログイン中のユーザー名
        /// </summary>
        public string loginUser { get; set; }

        /// <summary>
        /// チャットの処理番号
        /// </summary>
        private const int ProcessType = 3;

        public ChatForm()
        {

            InitializeComponent();

            //サーバー未接続だとメッセージ送信不可にする
            buttonSendMessage.Enabled = false;
            //サービス側のイベントハンドラのメソッドを登録
            clientService.messageReceived += new ClientService.ReceivedEventHandler(ChatForm_MessageReceived); 
        }

        /// <summary>
        /// 送信ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            // サーバー送信用のテキストに結合
            var sendText = this.MakeSendText(ProcessNumber, this.loginUser, textBoxSendMessage.Text);

            // サーバーに送信
            this.clientService.SendMessage(sendText);

            textBoxSendMessage.Clear();
        }

        /// <summary>
        /// メニュー：接続ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemServerConnect_Click(object sender, EventArgs e)
        {
            clientService.Connect(host, port);

            //メッセージ送信を操作可能にする
            buttonSendMessage.Enabled = true;
        }

        /// <summary>
        /// 本フォームを起動時にサーバー接続
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatForm_Load(object sender, EventArgs e)
        {
            //コメントアウトを外すとフォームを開いたときにサーバーに接続を行います

            //マルチスタートアッププロジェクトでサーバーと同時起動時の動作確認用
            Thread.Sleep(1000);

            try
            {
                clientService.Connect(host, port);
                buttonSendMessage.Enabled = true;

            }
            catch (SocketException se)
            {

                //ちゃんとした例外処理にあとで修正
                Console.WriteLine("起動時接続に失敗したので手動で接続してください！");
            }

        }

        //データ受信時にイベント発火！　チャット画面を更新する
        private void ChatForm_MessageReceived(object sender,byte[] recieveTelegram)
        {
            var telegram = new ChatTelegram(recieveTelegram);

            // テキストの更新  
            //　暫定　ユーザー名:メッセージ
            var message = telegram.GetHeader().UserName + " : " +telegram.Message + "\r\n";

            this.SetMessage(message);
        }

        /// <summary>
        /// Sendメソッドに渡すために必要なデータを結合する
        /// </summary>
        /// <param name="type">処理種別</param>
        /// <param name="userName">ユーザー名</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public string MakeSendText(int type, string userName, string message)
        {
            var strArray = new[] { type.ToString(), userName, message };

            var sendtext = string.Join(", ", strArray);

            return sendtext;
        }

        /// <summary>
        /// 非同期呼び出しのためのデリゲート
        /// </summary>
        /// <param name="message"></param>
        delegate void SetMessagetCallBack(string message);

        /// <summary>
        /// テキストボックスに受け取ったメッセージを更新していく
        /// </summary>
        /// <param name="message"></param>
        private void SetMessage(string message)
        {
            if (richTextBoxLog.IsDisposed) return;

            if (richTextBoxLog.InvokeRequired)
            {
                // コールバック作成
                SetMessagetCallBack setMessagetCallBack = new SetMessagetCallBack(SetMessage);

                // コントロールのスレッドで実行
                richTextBoxLog.Invoke(setMessagetCallBack, new object[] { message });
            }
            else
            {
                if(richTextBoxLog.Text == "")
                {
                    richTextBoxLog.Text = message;
                }
                else
                {
                    richTextBoxLog.Text += message;
                }     
            }
        }

        /// <summary>
        /// チャットフォームクローズでアプリケーション終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
