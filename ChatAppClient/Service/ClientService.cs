using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChatAppClient.Service
{
	/*    要望より
        ・クライアントの接続先情報はXMLあたりで外部ファイル管理してほしい
        ・Ctrl+Enterとかで送信できるようにもしてほしい*/

	class ClientService
	{
		//クライアントのソケット
		private Socket clientSocket;
		//ソケットのエンドポイント
		private IPEndPoint socketEP;

		//コンストラクタ
		ClientService()
		{

		}

		//サーバーと接続する
		public void Connect(string host, int port)
		{


			//接続する
			IPEndPoint ipEnd = new IPEndPoint(
				Dns.Resolve(host).AddressList[0], port);
			this.socket.Connect(ipEnd);

			this._localEndPoint = (IPEndPoint)this._socket.LocalEndPoint;
			this._remoteEndPoint = (IPEndPoint)this._socket.RemoteEndPoint;

			//イベントを発生
			this.OnConnected(new EventArgs());

			//非同期データ受信を開始する
			this.StartReceive();
		}


		/*受信側の処理*/

		//データの非同期受信を開始する
		public void StartReceive()
		{
			//beginReceiveメソッドで通信開始
		}

		//beginReceiveメソッドのコールバック
		private void ReceiveCallback(IAsyncResult ar)
		{
		//データを文字列に戻して表示したりする

		//beginReceiveで再び受信状態にする
		}

		/*送信側の処理*/

		//サーバーにメッセージを送信する
		public void SendMessage(String msg)
		{
			//メッセージを送信する
		}


	}
}
