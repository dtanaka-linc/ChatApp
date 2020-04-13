using System;
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

        /// <summary>
        /// 登録ボタンクリックイベント
        /// </summary>
        /// <param name="sender">ボタンクリック</param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if(userNameTextBox.Text != "")
            {
                // パスワードと確認用の入力が一致しない場合は警告する
                if (passTextBox.Text != confirmTextBox.Text)
                {
                    MessageBox.Show("入力したパスワードが一致しません", "警告",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
                }
                else
                {
                    // User.UserCreateに処理委譲
                    // passTextBoxの入力内容とユーザー名のテキストを渡す
                    // とりあえず動確のためメッセージボックスに登録完了と表示する
                    MessageBox.Show("登録完了しました","成功",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
            }
            else
            {
                MessageBox.Show("ユーザー名を入力して下さい", "警告",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);
            }
        }

        /// <summary>
        /// テキストボックスの中身を親フォーム同等に更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // ユーザー名のテキストボックスのテキストの更新
            this.userNameTextBox.Text = userNameParam;
        }

    }
}
