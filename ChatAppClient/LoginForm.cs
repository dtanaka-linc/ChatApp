using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // 押下時に入力内容をサーバーに送信してデータベースと照合を行う
            // 認証成功時　→　画面遷移
            // 認証失敗時　→　メッセージウィンドウ表示
        }
    }
}
