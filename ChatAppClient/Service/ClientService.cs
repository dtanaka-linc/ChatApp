using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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


		//コンストラクタ
		public ClientService()
		{
			clientSocket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			//エンコーディングの設定
			encoding = Encoding.UTF8;

		}

		//サーバーと接続する
		public void Connect(string host, int port)
		{
			Console.WriteLine("Client connect");

			socketEP = new IPEndPoint(
			Dns.Resolve(host).AddressList[0], port);

			clientSocket.Connect(socketEP);

			//非同期データ受信を開始する
			AsyncStateClient.StartReceive(clientSocket);
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
