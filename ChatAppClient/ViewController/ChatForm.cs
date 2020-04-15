﻿using System;
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

        /// <summary>
        /// チャットの処理番号
        /// </summary>
        private const int process_type = 3;

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
            this.
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
            //コメントアウトを外すとフォームを開いたときにサーバーに接続を行います
/*
            //マルチスタートアッププロジェクトでサーバーと同時起動時の動作確認用
            Thread.Sleep(1000);

            Console.WriteLine("load時の自動接続処理完了");

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
*/
        }

        private void textBoxSendMessage_TextChanged(object sender, EventArgs e)
        {

        }

        //データ受信時にイベント発火！　チャット画面を更新する
        private void ChatForm_MessageReceived(object sender,byte[] telegram)
        {
            //if (this.IsDisposed)
            //{
            //    return;
            //}
            //if (this.InvokeRequired)
            //{
            //    this.Invoke((MethodInvoker)delegate
            //    {
            //        ChatForm_MessageReceived(sender, text);
            //    });
            //}
            //else
            //{
            //    richTextBoxLog.Text = "user（未実装）>" + text + "\r\n" + richTextBoxLog.Text;
            //}
        }

        /// <summary>
        /// Sendメソッドに渡すために必要なデータを結合する
        /// </summary>
        /// <param name="type">処理種別</param>
        /// <param name="userName">ユーザー名</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public string MakeSendText(int type, string userName, string password)
        {
            var strArray = new[] { type.ToString(), userName, password };

            var sendtext = string.Join(", ", strArray);

            return sendtext;
        }
    }
}
