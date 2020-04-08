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

namespace ChatAppClient.ViewController
{
    public partial class ChatForm : Form
    {
        //サービス
        ClientService clientService = new ClientService();

        //確認用　設定は後で外部ファイル管理する！
        String host = "localhost";
        int port = 2001;

        public ChatForm()
        {

            InitializeComponent();

            //サーバー未接続だとメッセージ送信不可にする
            buttonSendMessage.Enabled = false;
            //サービス側のイベントハンドラのメソッドを登録
            clientService.messageReceived += new ClientService.ReceivedEventHandler(ChatForm_MessageReceived); 
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {

            clientService.SendMessage(textBoxSendMessage.Text);

        }

        private void richTextBoxLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        //ストリップ項目：サーバーに接続
        private void toolStripMenuItemServerConnect_Click(object sender, EventArgs e)
        {

            clientService.Connect(host, port);

            //メッセージ送信を操作可能にする
            buttonSendMessage.Enabled = true;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            //マルチスタートアッププロジェクトでの動作確認用
            Thread.Sleep(1000);

            Console.WriteLine("load時の自動接続処理完了");

            try
            {
                clientService.Connect(host, port);
                buttonSendMessage.Enabled = true;

            } catch(SocketException se) {
                
                //ちゃんとした例外処理にあとで修正
                Console.WriteLine("起動時接続に失敗したので手動で接続してください！");
            }
        }

        private void textBoxSendMessage_TextChanged(object sender, EventArgs e)
        {

        }

        //データ受信時にイベント発火！　チャット画面を更新する
        private void ChatForm_MessageReceived(object sender,String text)
        {
            if (this.IsDisposed) return;
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate { 
                        ChatForm_MessageReceived(sender,text); 
                    });
                }
                else
                {
                richTextBoxLog.Text ="(user) > " + text + "\r\n" + richTextBoxLog.Text;
            }
            
        }
    }
}
