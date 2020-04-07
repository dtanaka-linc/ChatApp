using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

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

        public Socket ServerSocket { get => serverSocket; set => serverSocket = value; }

        // マニュアルリセットイベントで処理待ちができる？
        private ManualResetEvent mre = new ManualResetEvent(false);

        //コンストラクタ
        public TcpService()
        {
            this.serverSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            //エンコーディングの設定
            encoding = Encoding.UTF8;

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

            //Listen中にlistenしないような処理を追加する

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
            //EndAcceptメソッドで、接続したクライアントとの通信に使用するSocketオブジェクト
            Socket connectedClient = null;
            try
            {
                lock (this)
                {
                    connectedClient = serverSocket.EndAccept(ar);
                    Console.WriteLine("クライアント({0}:{1})と接続しました。",
                    ((IPEndPoint)connectedClient.RemoteEndPoint).Address,
                    ((IPEndPoint)connectedClient.RemoteEndPoint).Port);
                }
            }
            catch
            {
                serverSocket.Close();
                return;
            }

            //(メッセージ送信テスト・後で削除)
            connectedClient.Send(encoding.GetBytes("接続確認"));
            //connectedClient.Shutdown(SocketShutdown.Both);
            //connectedClient.Close();

            //接続要求施行を再開する
            serverSocket.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
        }

        //接続中のクライアント全員にメッセージを送信する
        public void SendMessageAllClients(byte[] sendBytes)
        {
            //全員にメッセージを送る
            lock (this)
            {
                //データを送信する
                serverSocket.Send(sendBytes);
            }
        }

        //接続を切るときの処理
        public void socketClose() 
        {
            ServerSocket.Close();
            Console.WriteLine("Listenerを閉じました。");

        }

        }
}
