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

		private Encoding encoding;

        public byte[] ReceiveBuffer;
        public MemoryStream ReceivedData;

        //データを送信した後のデリゲートとイベント
        public delegate void ReceivedEventHandler(object sender, String text);
        public event ReceivedEventHandler messageReceived;


        //コンストラクタ
        public ClientService()
		{
			clientSocket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			//エンコーディングの設定
			encoding = Encoding.UTF8;
            ReceiveBuffer = new byte[1024];
            ReceivedData = new MemoryStream();

        }

		//サーバーと接続する
		public void Connect(string host, int port)
		{
			Console.WriteLine("Client connect");

			socketEP = new IPEndPoint(
			Dns.Resolve(host).AddressList[0], port);

			clientSocket.Connect(socketEP);

			//非同期データ受信を開始する
			StartReceive(clientSocket);
		}

		public void StartReceive(Socket soc)
		{
			AsyncStateClient so = new AsyncStateClient(soc);
			//非同期受信を開始
			soc.BeginReceive(so.ReceiveBuffer,
				0,
				so.ReceiveBuffer.Length,
				SocketFlags.None,
				new AsyncCallback(ReceiveDataCallback),
				so);
		}

        //BeginReceiveのコールバック
        private  void ReceiveDataCallback(IAsyncResult ar)
        {
            //状態オブジェクトの取得
            AsyncStateClient so = (AsyncStateClient)ar.AsyncState;

            //読み込んだ長さを取得
            int len = 0;
            try
            {
                len = so.Socket.EndReceive(ar);
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
                so.Socket.Close();
                return;
            }

            //受信したデータを蓄積する
            so.ReceivedData.Write(so.ReceiveBuffer, 0, len);
            if (so.Socket.Available == 0)
            {
                //最後まで受信した時
                //受信したデータを文字列に変換
                string str = Encoding.UTF8.GetString(
                    so.ReceivedData.ToArray());

                //受信した文字列を表示
                //確認用・実際はフォームに文字列を出力
                System.Console.WriteLine("サーバーからsendされました：" + str);

                messageReceived(this,str);


                so.ReceivedData.Close();
                so.ReceivedData = new MemoryStream();
            }

            //再び受信開始
            so.Socket.BeginReceive(so.ReceiveBuffer,
                0,
                so.ReceiveBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(ReceiveDataCallback),
                so);
        }

        /*送信側の処理*/

        //サーバーにメッセージを送信する
        public void SendMessage(String msg)
		{
			//メッセージを送信する
			//文字列をByte型配列に変換
			byte[] sendBytes = encoding.GetBytes(msg + "\r\n");

			lock (this)
			{
				//データを送信する
				clientSocket.Send(sendBytes);
			}
		}


	}
}