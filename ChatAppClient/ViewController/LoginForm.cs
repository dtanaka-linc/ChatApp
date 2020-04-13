using ChatAppClient;
using System;
using System.Windows.Forms;

namespace ChatAppServer
{
    public partial class LoginForm : Form
    {
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
            ChatAppClient.Service.ClientService service = new ChatAppClient.Service.ClientService();

            //初回利用時はユーザー登録を行う
            if (radioButton1.Checked)
            {
                RegisterForm registerForm = new RegisterForm();

                registerForm.userNameParam = this.NameTextBox.Text;

                registerForm.ShowDialog();
            }
            else
            {
                var authResult = true;

                var sendText = this.MakeSendText(1, this.NameTextBox.Text, this.PasswordTextBox.Text);

                service.SendMessage(sendText);

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
