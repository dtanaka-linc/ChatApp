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
                //User.Auth()呼び出し。
                //とりあえず今は空のメソッド
                authResult = this.DbConnect();

                // 認証成功時に画面遷移
                if (authResult)
                {
                    Form1 frm = new Form1();
                    frm.Show();

                }
                else
                {
                    MessageBox.Show("ユーザー名かパスワードが間違っています", "認証結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        // データベースに接続し認証処理を行う（User.Authが実装されたら不要になる。とりあえず処理続行のためtrueを返す）
        private bool DbConnect()
        {
            return true;
        }


    }
}
