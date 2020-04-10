using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatAppServer.Service;

namespace ChatAppServer
{
    //サーバーのサービスの実行確認用。後で修正（別フォームか、フォーム自体使用しないか）
    //接続処理は別ファイルに後で修正する

    public partial class Form1 : Form
    {
        TcpService tcpService = new TcpService();

        //後で削除
        String host;
        int port;
        int backlog;

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Delay(1000);
            //接続確認用の仮設定、後で別ファイルで管理する
            host = "localhost";
            port = 2001;
            backlog = 10;
            tcpService.listen(host, port, backlog);
            //サーバーを立ち上げる
            textBox1.Text = "受信中";

            //手動接続ボタンを操作不可にする
            button1.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //接続確認用の仮設定、後で別ファイルで管理する
            host = "localhost";
            port = 2001;
            backlog = 10;
            tcpService.listen(host, port, backlog);
            //サーバーを立ち上げる
            textBox1.Text = "受信中";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
