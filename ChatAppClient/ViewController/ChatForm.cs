using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatAppClient.Service;

namespace ChatAppClient.ViewController
{
    public partial class ChatForm : Form
    {
        //サービス
        ClientService clientService = new ClientService();

        public ChatForm()
        {
            InitializeComponent();

            //サーバー未接続だとメッセージ送信不可にする
            buttonSendMessage.Enabled = false;
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

            //確認用　設定は後で外部ファイル管理する！
            String host = "localhost";
            int port = 2001;

            clientService.Connect(host, port);

            //メッセージ送信を操作可能にする
            buttonSendMessage.Enabled = true;
        }
    }
}
