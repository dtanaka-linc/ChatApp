using ChatAppClient;
using System;
using System.Windows.Forms;
using ChatAppClient.Service;
using ChatAppLibrary.Telegram;
using ChatAppLibrary.TelegramService;
using System.Text;

namespace ChatAppServer
{
    public partial class LoginForm : Form
    {
        //確認用　設定は後で外部ファイル管理する！
        String host = "localhost";
        int port = 2001;

        //　認証要求の処理番号
        int process_type = 1;

        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ログインボタンのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            ClientService service = new ChatAppClient.Service.ClientService();
            service.messageReceived += new ClientService.ReceivedEventHandler(LoginForm_MessageReceived);          

            //初回利用時はユーザー登録を行う
            if (radioButton1.Checked)
            {
                RegisterForm registerForm = new RegisterForm();

                registerForm.userNameParam = this.NameTextBox.Text;

                registerForm.ShowDialog();
            }
            else
            {
                service.Connect(host, port);
                // 送信するデータの作成（string）
                var sendText = this.MakeSendText(process_type, this.NameTextBox.Text, this.PasswordTextBox.Text);

                // サーバーにデータを送信
                service.SendMessage(sendText);

                // 認証結果がtrueと受け取ったと仮定する
                var authResult = true;
                // 認証成功時に画面遷移
                if (authResult)
                {
                    // Chatフォームに遷移する
                    
                }
                else
                {
                    MessageBox.Show("ユーザー名かパスワードが間違っています", "認証結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        /// <summary>
        /// サーバー側からのテレグラム受信時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="text"></param>
        private void LoginForm_MessageReceived(object sender, byte[] recieveTelegram)
        {
            //var telegram = GetTelegram(recieveTelegram);
            var telegram = TelegramRogic.GetTelegram(recieveTelegram);

            //switch (telegram.GetHeader().Type)
            //{

            //}
        }

        /// <summary>
        /// Sendメソッドに渡すために必要なデータを結合する
        /// </summary>
        /// <param name="type">処理種別</param>
        /// <param name="userName">ユーザー名</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public string MakeSendText(int type,string userName, string password)
        {          
            var strArray = new[] { type.ToString(), userName, password };

            var sendtext = string.Join(", ", strArray);

            return sendtext;
        }

    }
}
