using ChatAppClient;
using System;
using System.Windows.Forms;
using ChatAppClient.Service;
using ChatAppClient.ViewController;
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

        /// <summary>
        /// 認証要求の処理番号
        /// </summary>
        private const int ProcessType = 1;

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
                this.Hide();
            }
            else
            {
                // サーバーに接続
                service.Connect(host, port);

                // 送信するデータの作成（string）
                var sendText = this.MakeSendText(ProcessType, this.NameTextBox.Text, this.PasswordTextBox.Text);

                // サーバーにデータを送信
                service.SendMessage(sendText);

            }

        }

        /// <summary>
        /// サーバー側からのテレグラム受信時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="text"></param>
        private void LoginForm_MessageReceived(object sender, byte[] recieveTelegram)
        {

            // サーバー側からの返答が応答のテレグラムになったら外す
            //var telegram = new AuthResponseTelegram(recieveTelegram);
            //var authResult = telegram.AuthResult;

            // 画面遷移の動作確認用　
            var authResult = true;

            // 認証成功時に画面遷移
            if (authResult)
            {
                // Loginフォームをhideする
                Invoke(new FormHideDelegate(FormHide));

                // Chatフォームに遷移する
                ChatForm chatForm = new ChatForm();
                chatForm.loginUser = this.NameTextBox.Text;
                chatForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("ユーザー名かパスワードが間違っています", "認証結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

            var sendtext = string.Join(",", strArray);

            return sendtext;
        }

        /// <summary>
        /// LoginForm_MessageReceived内でフォームの状態を変更するのに使用するデリゲート
        /// </summary>
        delegate void FormHideDelegate();

        private void FormHide()
        {
            this.Hide();
        }
    }
}
