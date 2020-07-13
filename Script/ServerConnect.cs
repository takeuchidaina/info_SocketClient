using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnect : MonoBehaviour
{
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
        string ipOrHost = "127.0.0.1";
        //string ipOrHost = "192.168.11.21";
        int port = 2001;

        do
        {
            //Debug.Log("通信開始　内容："+_sendMsg);
            if (_sendMsg == null || _sendMsg.Length == 0)
            {
                //Debug.Log("送信失敗");
                return -1;
            }

            System.Net.Sockets.TcpClient tcp =
               new System.Net.Sockets.TcpClient(ipOrHost, port);
            System.Net.Sockets.NetworkStream ns = tcp.GetStream();

            ns.ReadTimeout = 10000;
            ns.WriteTimeout = 10000;

            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            byte[] sendBytes = enc.GetBytes(_sendMsg + '\n');

            ns.Write(sendBytes, 0, sendBytes.Length);

            byte[] resBytes = new byte[256];
            int resSize = 0;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            do
            {
                resSize = ns.Read(resBytes,0,resBytes.Length);
                if (resSize == 0)
                {
                    break;
                }
                ms.Write(resBytes, 0, resSize);
                
            } while (ns.DataAvailable || resBytes[resSize-1] != '\n');
            string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            //ログに入力内容を表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog("受信："+resMsg);
            ns.Close();
            tcp.Close();
            return 0;

        } while (true);

    }
}
