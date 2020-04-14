using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace ChatAppClient.Service
{

	/*    要望（後で修正）
        ・クライアントの接続先情報はXMLあたりで外部ファイル管理してほしい
        ・Ctrl+Enterとかで送信できるようにもしてほしい
        ・publicなメソッドやらプロパティはXMLドキュメントつけてほしい
        ⇒該当のメソッド／変数／プロパティとかの直前で「///」打てば勝手に出るので必要に応じて内容書く感じ
        ・メンバ変数は可能な限り排除（プロパティで書くのが望ましいらしいですよ！）
        ⇒public int IntVal {get; set;} みたいなやつ
        
    */

	class ClientService
	{

		//クライアントのソケット
		private Socket clientSocket;
		//ソケットのエンドポイント
		private IPEndPoint socketEP;
        //文字コード

		private Encoding encoding;

        private byte[] ReceiveBuffer;
        private MemoryStream ReceivedData;
        //データを受信した後、チャット画面更新用のデリゲートとイベント
        public delegate void ReceivedEventHandler(object sender, String text);
        public event ReceivedEventHandler messageReceived;


        //コンストラクタ
        public ClientService()
		{
			clientSocket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			encoding = Encoding.UTF8;
            ReceiveBuffer = new byte[1024];
            ReceivedData = new MemoryStream();

        }

		/// <summary>
        /// 受信状態(Listenメソッド実行後の)サーバーと接続する
        /// </summary>
        /// <param name="host">ホスト名</param>
        /// <param name="port">ポート番号</param>
		public void Connect(string host, int port)
		{
			socketEP = new IPEndPoint(
			Dns.Resolve(host).AddressList[0], port);

			clientSocket.Connect(socketEP);

			//非同期データ受信を開始する
			StartReceive(clientSocket);
		}

        //受信を開始
		private void StartReceive(Socket soc)
		{
			//AsyncStateClient so = new AsyncStateClient(soc);
			//非同期受信を開始
			soc.BeginReceive(ReceiveBuffer,
				0,
				ReceiveBuffer.Length,
				SocketFlags.None,
				new AsyncCallback(ReceiveDataCallback),
				this);
		}

        //BeginReceiveのコールバック
        private void ReceiveDataCallback(IAsyncResult ar)
        {
            //読み込んだ長さを取得
            int len = 0;
            try
            {
                len = clientSocket.EndReceive(ar);
            }
            catch (ObjectDisposedException)
            {
                //閉じた時
                Console.WriteLine("閉じました。");
                return;
            }
            catch (SocketException)
            {
                Console.WriteLine("ホストに強制的に切断されました。");
                return;
            }

            //切断されたか調べる
            if (len <= 0)
            {
                Console.WriteLine("切断されました。");
                clientSocket.Close();
                return;
            }

            //受信したデータを蓄積する
            ReceivedData.Write(ReceiveBuffer, 0, len);
            if (clientSocket.Available == 0)
            {
                //最後まで受信した時
                //受信したデータを文字列に変換
                string str = Encoding.UTF8.GetString(
                    ReceivedData.ToArray());

                //受信した文字列をコンソールに表示
                System.Console.WriteLine("ClientService:サーバーから送信されたメッセージ：" + str);

                //メッセージ受信時の処理
                messageReceived(this,str);


                ReceivedData.Close();
                ReceivedData = new MemoryStream();
            }

            //再び受信開始
            clientSocket.BeginReceive(ReceiveBuffer,
                0,
                ReceiveBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(ReceiveDataCallback),
                this);
        }

        /// <summary>
        /// サーバーにメッセージを送信する
        /// </summary>
        /// <param name="msg">送信する文字列</param>
        public void SendMessage(String msg)
		{
            //メッセージを送信する
            //文字列をByte型配列に変換
            byte[] sendBytes = encoding.GetBytes(msg);

				//データを送信する
				clientSocket.Send(sendBytes);

		}

    }
}