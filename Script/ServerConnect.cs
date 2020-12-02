using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

//https://dobon.net/vb/dotnet/internet/tcpclientserver.html#section4 参考サイト

public class ServerConnect : MonoBehaviour
{
    public IPHostEntry HostName/* = "127.0.0.1";*/;
    public string ipOrHost;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int SendServer(string _sendMsg)
    {
        //  string ipOrHost = SendButton.Get_IP;
        //string ipOrHost = "127.0.0.1";
        
        int port = 2001;
        HostName = Dns.GetHostEntry(Dns.GetHostName());
    
        Debug.Log("ip:" + ipOrHost.ToString());
        do
        {
            //Debug.Log("通信開始　内容："+_sendMsg);
            if (_sendMsg == null || _sendMsg.Length == 0)
            {
                //Debug.Log("送信失敗");
                return -1;
            }

            System.Net.Sockets.TcpClient tcp =
               new System.Net.Sockets.TcpClient(ipOrHost.ToString(), port);    //tcpクライアント作成し接続
            System.Net.Sockets.NetworkStream ns = tcp.GetStream();  //ネットワークストリーム取得
            ns.ReadTimeout = 10000;
            ns.WriteTimeout = 10000;    //タイムアウトの秒数

            //接続したあとの送信系
            System.Text.Encoding enc = System.Text.Encoding.UTF8;   //文字コード設定
            byte[] sendBytes = enc.GetBytes(_sendMsg + '\n');       //文字列をByte型配列に変換

            ns.Write(sendBytes, 0, sendBytes.Length);   //送信            
        //ログに入力内容を表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog(_sendMsg);
            //データ受信
            byte[] resBytes = new byte[1024];
            int resSize = 0;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            do
            {
                resSize = ns.Read(resBytes, 0, resBytes.Length);  //データの一部を受信
                if (resSize == 0)   //read0の場合切断されたと判断
                {
                    break;
                }
                ms.Write(resBytes, 0, resSize); //受信したデータの蓄積
                                                //読み取れるデータがあるか、データの最後が\nでないときは受信を続ける
            } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');
            string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            //ログに入力内容を表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog("受信：" + resMsg);
            ns.Close();
            tcp.Close();    //閉じる
            return 0;

        } while (true);

    }
    public int GetJson()
    {
        //  string ipOrHost = SendButton.Get_IP;
        string ipOrHost = "127.0.0.1";
        int port = 2001;

        do
        {
       
            System.Net.Sockets.TcpClient tcp =
               new System.Net.Sockets.TcpClient(ipOrHost, port);    //tcpクライアント作成し接続
            System.Net.Sockets.NetworkStream ns = tcp.GetStream();  //ネットワークストリーム取得
            ns.ReadTimeout = 10000;
            ns.WriteTimeout = 10000;    //タイムアウトの秒数

            //接続したあとの送信系
            System.Text.Encoding enc = System.Text.Encoding.UTF8;   //文字コード設定
            byte[] sendBytes = enc.GetBytes("/setting" + '\n');       //文字列をByte型配列に変換

            ns.Write(sendBytes, 0, sendBytes.Length);   //送信            
                                                        //ログに入力内容を表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog("/setting");
            //データ受信
            byte[] resBytes = new byte[1024];
            int resSize = 0;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            do
            {
                resSize = ns.Read(resBytes, 0, resBytes.Length);  //データの一部を受信
                if (resSize == 0)   //read0の場合切断されたと判断
                {
                    break;
                }
                ms.Write(resBytes, 0, resSize); //受信したデータの蓄積
                                                //読み取れるデータがあるか、データの最後が\nでないときは受信を続ける
            } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');
            string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            //ログに入力内容を表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog("受信：" + resMsg);
            JsonCreater.instance.Create(resMsg);
            ns.Close();
            tcp.Close();    //閉じる
            return 0;

        } while (true);

    }
}
