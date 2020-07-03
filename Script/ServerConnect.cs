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
        //string ipOrHost = "192.168.11.17";
        int port = 2001;

        do
        {
            if (_sendMsg == null || _sendMsg.Length == 0)
            {
                return 0;
            }

            System.Net.Sockets.TcpClient tcp =
               new System.Net.Sockets.TcpClient(ipOrHost, port);
            System.Net.Sockets.NetworkStream ns = tcp.GetStream();

            ns.ReadTimeout = 10000;
            ns.WriteTimeout = 10000;

            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            byte[] sendBytes = enc.GetBytes(_sendMsg + '\n');

            ns.Write(sendBytes, 0, sendBytes.Length);
            ns.Close();
            tcp.Close();

        } while (true);
    }
}
