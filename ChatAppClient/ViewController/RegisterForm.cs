﻿using ChatAppClient.Service;
using ChatAppClient.ViewController;
using ChatAppLibrary.Constants;
using ChatAppLibrary.Telegram;
using ChatAppLibrary.TelegramService;
using System;
using System.Text;
using System.Windows.Forms;

namespace ChatAppClient
{
    public partial class RegisterForm : Form
    {
        /// <summary>
        /// ログインフォームでの入力ユーザー名受け渡し用のプロパティ
        /// </summary>
        public string userNameParam { get; set; }

        /// <summary>
        /// 登録要求の処理種別
        /// </summary>
        private const int ProcessType = 2;

        ClientService service = new ChatAppClient.Service.ClientService();

        public RegisterForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// テレグラム受信イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="telegram">受診したテレグラム</param>
        private void RegisterForm_MessageReceived(object sender, byte[] recieveTelegram)
        {
            // サーバーからの返答が応答のテレグラムになったらコメントアウト外す
            //var telegram = new RegistrationResponceTelegram(recieveTelegram);
            //var authResult = telegram.AuthResult;

            // 画面遷移の確認用　サーバーからの返答が応答のテレグラムになったら消す
            var authResult = true;

            // 認証成功時に画面遷移
            if (authResult)
            {
                Invoke(new FormHideDelegate(FormHide));

                // 登録完了のメッセージ表示
                MessageBox.Show(DisplayMessage.REGISTERD, DisplayMessage.CONFIRMATION_RESULT, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                // Chatフォームに遷移する
                ChatForm chatForm = new ChatForm();
                chatForm.loginUser = this.userNameTextBox.Text;
                chatForm.ShowDialog();
            }
            else
            {
                MessageBox.Show(DisplayMessage.MESSAGE_REGISTRATION_FAILED, DisplayMessage.CONFIRMATION_RESULT, 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                    MessageBox.Show(DisplayMessage.PASSWORD_MISSMATCH, DisplayMessage.WARNING,
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
                }
                else
                {
                    ClientService service = new ChatAppClient.Service.ClientService();
                    service.messageReceived += new ClientService.ReceivedEventHandler(RegisterForm_MessageReceived);
                    
                    // サーバーに接続する
                    service.Connect(ConnectionInfo.HOST, ConnectionInfo.PORT);

                    // 送信するデータを結合
                    var sendTelegram = this.MakeSendText(ProcessType, userNameTextBox.Text, passTextBox.Text);

                    // サーバーに送信
                    service.SendMessage(sendTelegram);
                    
                }
            }
            else
            {
                MessageBox.Show(DisplayMessage.INPUT_REQUIRED, DisplayMessage.WARNING,
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

            var sendtext = string.Join(",", strArray);

            return sendtext;
        }

        /// <summary>
        /// LoginForm_MessageReceived内でフォームの状態を変更するのに使用するデリゲート
        /// </summary>
        delegate void FormHideDelegate();

        private void FormHide()
        {
            this.Close();
        }
    }
}
