using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace ChatAppServer.Service
{

/*  
 *  サーバ側のライフサイクル
    create ソケットの作成
    bind ソケットを特定のIPアドレスとポートに紐付け
    listen 接続の待受を開始
    accept 接続を受信
    close 接続を切断

    */

/*
        サーバーの処理
        Bindメソッドでバインド
        Listenメソッドでクライアントの接続を待機
        非同期メソッドのBeginAccept→EndAcceptで停止して処理→再度BeginAccept
        */


    class TcpService
    {

        //サーバーのソケット
        private Socket server;
        //接続中のクライアントのコレクション
        private ArrayList connectedClients;
        //ソケットのエンドポイント
        private IPEndPoint socketEP;

        //コンストラクタ
        TcpService()
        {
            this.server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            
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
            socketEP = new IPEndPoint(
                Dns.GetHostEntry(host).AddressList[0], port);
            this.server.Bind(this.socketEP);

            //Listenを開始する
            this.server.Listen(backlog);

            //接続要求を開始する（データを受信すると、指定したコールバックメソッドが実行される）
            this.server.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
        }

        //データを受信時のコールバックメソッド
        private void AcceptCallback(IAsyncResult ar)
        {

            //接続要求をいったん停止
            //EndAccept;

            //接続中のクライアント一覧に追加する

            //何らかの処理

            //接続要求施行を再開する
            this.server.BeginAccept(new AsyncCallback(this.AcceptCallback), null);
        }

        //接続中のクライアント全員にメッセージを送信
        public void SendMessageAllClients(String message)
        {
            //全員にメッセージを送る
        }



        }
}
