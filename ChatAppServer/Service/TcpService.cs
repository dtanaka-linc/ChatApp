using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using ChatAppLibrary.Telegram;
using ChatAppServer.Repository;

namespace ChatAppServer.Service
{
    /*    要望（後で修正）
        ・クライアントの接続先情報はXMLあたりで外部ファイル管理してほしい
        ・Ctrl+Enterとかで送信できるようにもしてほしい
        ・publicなメソッドやらプロパティはXMLドキュメントつけてほしい
        ⇒該当のメソッド／変数／プロパティとかの直前で「///」打てば勝手に出るので必要に応じて内容書く感じ
        ・メンバ変数は可能な限り排除（プロパティで書くのが望ましいらしいですよ！）
        ⇒public int IntVal {get; set;} みたいなやつ
        
    */

    class TcpService
    {

        //サーバーのソケット
        private Socket serverSocket;

        //ソケットのエンドポイント
        private IPEndPoint socketEP;
        private Encoding encoding;

        public static ArrayList acceptedClients;

        public UserRepository userRepository;

        //コンストラクタ
        public TcpService()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            //エンコーディングの設定
            encoding = Encoding.UTF8;
            //接続中のクライアントのコレクション
            acceptedClients = new ArrayList();

        }

        /// <summary>
        /// サーバーを立てる
        /// </summary>
        /// <param name="host">ホスト名（アドレス）</param>
        /// <param name="port">ポート番号</param>
        /// <param name="backlog">保留中の接続のキューの最大長
        /// （ソケットが受け入れしていない接続要求を何個まで保持するかの最大値）</param>
        public void listen(string host, int port, int backlog)

        {
            Console.WriteLine("listen start");

            //エンドポイントの設定
            socketEP = new IPEndPoint(
            Dns.Resolve(host).AddressList[0], port);

            //バインドする
            serverSocket.Bind(this.socketEP);

            //Listenを開始する
            serverSocket.Listen(backlog);

            //接続要求を開始する
            serverSocket.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
        }

        //データを受信時のコールバックメソッド
        private void AcceptCallback(IAsyncResult ar)
        {
            //接続したクライアントとの通信に使用するSocket
            Socket connectedClient = null;
            try
            {
                connectedClient = serverSocket.EndAccept(ar);
                Console.WriteLine("クライアント({0}:{1})と接続しました。",
                ((IPEndPoint)connectedClient.RemoteEndPoint).Address,
                ((IPEndPoint)connectedClient.RemoteEndPoint).Port);
            }
            catch
            {
                serverSocket.Close();

                return;
            }

            //(メッセージ送信テスト・後で修正)
            //connectedClient.Send(encoding.GetBytes("TcpService：（接続確認メッセージ）"));

            //接続したクライアントの状態オブジェクトTcpChatClientの作成
            TcpStateClient client = new TcpStateClient(connectedClient);

            //接続中のクライアントのコレクションに追加
            acceptedClients.Add(client);

            //メッセージ受信時にTcpServiceのメッセージ送信処理を発火させる
            client.messageReceived += new TcpStateClient.ReceivedEventHandler(SendTelegram);

            //受信状態にさせる
            client.StartReceive(connectedClient);

            //接続要求施行を再開する
            serverSocket.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
        }

        //受信したデータを送り返すメソッド
        private void SendTelegram(TcpStateClient client, byte[] telegram)
        {
            //先頭１バイトの値を取得
            string identifier = telegram[0].ToString();

            switch (identifier)
            {
                case "49":
                    Console.WriteLine("認証");
                    SendAuth(client, telegram);
                    break;
                case "50":
                    Console.WriteLine("登録");
                    SendResister(client, telegram);
                    break;
                case "51":
                    Console.WriteLine("チャット");
                    SendChat(telegram);
                    break;
                default:
                    Console.WriteLine("エラー");
                    //何かしらのエラー処理をあとでいれる
                    throw new Exception();
            }
        }

        private void SendAuth(TcpStateClient stateClient, byte[] telegram)
        {
            //Itelegramにプロパティがないのでいったんコメントアウト
            //ITelegram receiveIt = new AuthRequestTelegram(telegram);
            AuthRequestTelegram receiveIt = new AuthRequestTelegram(telegram);
            //受信送信で別のテレグラムを使うとのことなので送信用を作成
            AuthResponseTelegram sendIt = new AuthResponseTelegram(telegram);

            //UserService.Auth：DBと接続して認証

            //確認用の仮の値(UserServiceを使うときに削除)
            sendIt.authResult = true;


            //string str = MakeSendText(receiveIt.GetHeader().Type, receiveIt.GetHeader().UserName.ToString(), authResult);
            String str = sendIt.ToTelegramText();

            SendClientMessage(stateClient, str);

        }

        private void SendResister(TcpStateClient stateClient, byte[] telegram)
        {
            //Itelegramにプロパティがないのでいったんコメントアウト
            //ITelegram receiveIt = new RegistrationTelegram(telegram);
            RegistrationTelegram receiveIt = new RegistrationTelegram(telegram);
            //受信送信で別のテレグラムを使うとのことなので送信用を作成
            RegistrationResponceTelegram sendIt = new RegistrationResponceTelegram(telegram);

            //UserService.Register：DBと接続して新規登録

            //string str = MakeSendText(receiveIt.GetHeader().Type, receiveIt.GetHeader().UserName.ToString(), receiveIt.PassWord);
            String str = sendIt.ToTelegramText();

            SendClientMessage(stateClient, str);
        }

        private void SendChat(byte[] telegram)
        {
            //Itelegramにプロパティがないのでいったんコメントアウト
            //ITelegram receiveIt = new ChatTelegram(telegram);
            ChatTelegram receiveIt = new ChatTelegram(telegram);

            Console.WriteLine(receiveIt.GetHeader().Type.ToString());
            Console.WriteLine(receiveIt.GetHeader().UserName.ToString());

            //string str = MakeSendText(receiveIt.GetHeader().Type, receiveIt.GetHeader().UserName.ToString(), receiveIt.Message);
            String str = receiveIt.ToTelegramText();

            SendAllClientMessage(str);

        }

        /// <summary>
        /// クライアントにメッセージを送信（認証・新規登録）
        /// </summary>
        /// <param name="stateClient">認証・新規登録をしようとしているクライアント</param>
        /// <param name="str">送信するメッセージ（String）</param>
        public void SendClientMessage(TcpStateClient stateClient, string str)
        {
            stateClient.Socket.Send(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// クライアントにメッセージを送信（認証・新規登録）
        /// </summary>
        /// <param name="stateClient">認証・新規登録をしようとしているクライアント</param>
        /// <param name="str">送信するメッセージ（byte[]）</param>
        public void SendClientMessage(TcpStateClient stateClient, byte[] str)
        {
            stateClient.Socket.Send(str);
        }

        /// <summary>
        /// 接続中の全てのクライアントにメッセージを送信（チャット）
        /// </summary>
        /// <param name="str">送信するメッセージ（String）</param>
        public void SendAllClientMessage(string str)
        {
            foreach (TcpStateClient client in acceptedClients)
            {
                client.Socket.Send(Encoding.UTF8.GetBytes(str));
            }
        }

        /// <summary>
        /// 接続中の全てのクライアントにメッセージを送信（チャット）
        /// </summary>
        /// <param name="str">送信するメッセージ（byte[]）</param>
        public void SendAllClientMessage(byte[] str)
        {
            foreach (TcpStateClient client in acceptedClients)
            {
                client.Socket.Send(str);
            }
        }
    }



}