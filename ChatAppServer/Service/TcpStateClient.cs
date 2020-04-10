using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ChatAppServer.Service
{


    //非同期データ受信のための状態オブジェクト

    class TcpStateClient
    {
        private Socket Socket;
        private byte[] ReceiveBuffer;
        private MemoryStream ReceivedData;
        private Encoding Encoding;

        public TcpStateClient(Socket soc)
        {
            Socket = soc;
            ReceiveBuffer = new byte[1024];
            ReceivedData = new MemoryStream();
            Encoding = Encoding.UTF8;
        }

        //データ受信スタート
        public  void StartReceive(Socket soc)
        {
            TcpStateClient so = new TcpStateClient(soc);
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
            TcpStateClient so = (TcpStateClient)ar.AsyncState;

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
                String str = Encoding.UTF8.GetString(
                    so.ReceivedData.ToArray());

                //受信した文字列を表示
                //確認用・実際はフォームに文字列を出力
                System.Console.WriteLine("TcpStateClient:クライアントから送信されたメッセージ：" + str);

                so.ReceivedData.Close();
                so.ReceivedData = new MemoryStream();

                so.Socket.Send(Encoding.GetBytes(str));
                
            }

            //再び受信開始
            so.Socket.BeginReceive(so.ReceiveBuffer,
                0,
                so.ReceiveBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(ReceiveDataCallback),
                so);
        }


    }

}