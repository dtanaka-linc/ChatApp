using ChatAppClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ChatAppServer
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // 初回利用時はユーザー登録を行う
            if (radioButton1.Checked)
            {
                //ユーザー登録のフォームを別で作って実装していたのですがなんか気持ち悪いのでユーザーコントロールの切り替えで対応しようと思っています（要相談）
            }
            else
            {
                bool authResult;
                //User.Auth()呼び出し。
                //とりあえず今は空のメソッド
                authResult = this.DbConnect();

                // 認証成功時に画面遷移
                if (authResult)
                {
                    Form1 frm = new Form1();
                    frm.Show();
                    this.Close();
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
