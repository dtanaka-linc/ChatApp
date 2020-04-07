using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatAppClient
{
    public partial class RegisterForm : Form
    {
        private string _userNameParam;

        public string userNameParam
        {
            get
            {
                return _userNameParam;
            }
            set
            {
                _userNameParam = value;
            }
        }

        public RegisterForm()
        {
            InitializeComponent();

            
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            // パスワードと確認用の入力が一致する場合は警告する
            if(passTextBox.Text == confirmTextBox.Text)
            {

            }
            else
            {
                // User.UserCreateに処理委譲
                // passTextBoxの入力内容とユーザー名のテキストを渡す
            }

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // ユーザー名のテキストボックスのテキストの更新
            this.userNameTextBox.Text = userNameParam;
        }
    }
}
