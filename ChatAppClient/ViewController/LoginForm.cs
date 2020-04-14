using ChatAppClient;
using System;
using System.Windows.Forms;
using ChatAppClient.Service;
using ChatAppLibrary.Telegram;
using System.Text;

namespace ChatAppServer
{
    public partial class LoginForm : Form
    {
        //確認用　設定は後で外部ファイル管理する！
        String host = "localhost";
        int port = 2001;

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
            service.Connect(host,port);

            //初回利用時はユーザー登録を行う
            if (radioButton1.Checked)
            {
                RegisterForm registerForm = new RegisterForm();

                registerForm.userNameParam = this.NameTextBox.Text;

                registerForm.ShowDialog();
            }
            else
            {
                //----------------------------------------------------------------------------------------------------------------
                // 送信するデータの作成（string）
                // ↓
                // byte[]変換（実装時はSend()内で行う）
                // ↓
                // テレグラムにデータセット、値をとれるか確認
                // ここまで試してみる

                // 送信するデータの作成（string）
                var sendText = this.MakeSendText(1, this.NameTextBox.Text, this.PasswordTextBox.Text);

                // byte[]変換（実装時はSend()内で行う）
                //service.SendMessage(sendText);   
                var sendTelegram = this.Confirm(sendText);

                // 上記のbyte[]を受け取ったと仮定してテレグラムに引数で渡す
                // 本実装時はクライアント側なので応答のテレグラムに渡すがとりあえずテストで違うのに渡す
                var telegram = new AuthRequestTelegram(sendTelegram);

                // 登録したデータを参照して出力してみる
                Console.WriteLine(telegram.GetHeader().Type.ToString());
                Console.WriteLine(telegram.GetHeader().UserName.ToString());
                Console.WriteLine(telegram.PassWord);
                //----------------------------------------------------------------------------------------------------------------

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
        private void LoginForm_MessageReceived(object sender, string text)
        {
            throw new NotImplementedException();
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

        private byte[] Confirm(string str)
        {
            //メッセージを送信する
            //文字列をByte型配列に変換
            byte[] sendBytes = Encoding.UTF8.GetBytes(str);
            return sendBytes;
        }
    }
}
