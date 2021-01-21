using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://dobon.net/vb/dotnet/internet/tcpclientserver.html#section4 参考サイト

public class ServerConnect : MonoBehaviour
{
    string mozi = "/Setting";

    static string sendMsg = "";

    static bool flg = true;

    public static string SendMsg
    {
        get { return sendMsg; }
    }

    public static bool Flg
    {
        get { return flg; }
    }

    [SerializeField]
    public string ipOrHost;//接続したいPCのIP

    // Start is called before the first frame update
    void Start()
    {
        ipOrHost = ServerList.SelectServer.ServerRoomIP;
        //ipOrHost = SendButton.Get_IP;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ipOrHost);
    }

    public int SendServer(string _sendMsg)
    {
        int port = 2001;
        flg = true;
        do
        {
            //Debug.Log("通信開始　内容："+_sendMsg);
            if (_sendMsg == null || _sendMsg.Length == 0)
            {
                //Debug.Log("送信失敗");
                return -1;
            }

            System.Net.Sockets.TcpClient tcp;    //tcpクライアント作成し接続
            System.Net.Sockets.NetworkStream ns;  //ネットワークストリーム取得

            try
            {
                tcp =　new System.Net.Sockets.TcpClient(ipOrHost, port);    //tcpクライアント作成し接続
                ns = tcp.GetStream();  //ネットワークストリーム取得
            }
            catch (System.Net.Sockets.SocketException)
            {
                //ログに入力内容を表示する
                GameObject.Find("Text_Log").GetComponent<Log>().AddLog("通信が成功しませんでした");
                flg = false;
                return 0;
            }

            ns.ReadTimeout = 100;
            ns.WriteTimeout = 100;    //タイムアウトの秒数

            //接続したあとの送信系
            System.Text.Encoding enc = System.Text.Encoding.UTF8;   //文字コード設定
            byte[] sendBytes = enc.GetBytes(_sendMsg + '\n');       //文字列をByte型配列に変換

            ns.Write(sendBytes, 0, sendBytes.Length);   //送信   
            if (_sendMsg != "/Setting")
            {
                //ログに入力内容を表示する
                GameObject.Find("Text_Log").GetComponent<Log>().AddLog(_sendMsg);
            }
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

            sendMsg = resMsg;

            if (_sendMsg != "/Setting")
            {
                //ログに入力内容を表示する
                GameObject.Find("Text_Log").GetComponent<Log>().AddLog("受信：" + resMsg);
            }

            ns.Close();
            tcp.Close();    //閉じる
            return 0;

        } while (true);

    }
    public void GetJson()
    {
        //送信成功
        if (GameObject.Find("ServerConnect").GetComponent<ServerConnect>().SendServer(mozi) == 0)
        {

        }
        //送信失敗
        else
        {
            //ログにエラーを表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog("送信に失敗しました");
        }
    }
}
