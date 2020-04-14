using ChatAppClient.Service;
using System;
using System.Windows.Forms;

namespace ChatAppClient
{
    public partial class RegisterForm : Form
    {
        public string userNameParam { get; set; }

        String host = "localhost";
        int port = 2001;

        /// <summary>
        /// 処理種別
        /// </summary>
        int process_type = 2;

        ClientService service = new ChatAppClient.Service.ClientService();

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
            if(userNameTextBox.Text != ""　&& passTextBox.Text != "")
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
                    service.Connect(host, port);

                    // 送信可能なようにデータを結合
                    var sendTelegram = this.MakeSendText(process_type, userNameTextBox.Text, passTextBox.Text);

                    // サーバーに送信
                    service.SendMessage(sendTelegram);

                    // 認証結果がtrueと受け取ったと仮定する
                    var authResult = true;
                    // 認証成功時に画面遷移
                    if (authResult)
                    {
                        MessageBox.Show("登録完了しました","成功",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        // Chatフォームに遷移する

                    }
                    else
                    {
                        MessageBox.Show("ユーザー名かパスワードが間違っています", "認証結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
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

        /// <summary>
        /// Sendメソッドに渡すために必要なデータを結合する
        /// </summary>
        /// <param name="type">処理種別</param>
        /// <param name="userName">ユーザー名</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public string MakeSendText(int type, string userName, string password)
        {
            var strArray = new[] { type.ToString(), userName, password };

            var sendtext = string.Join(", ", strArray);

            return sendtext;
        }
    }
}
