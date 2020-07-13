﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;

public class Receiver : MonoBehaviour
{
    //string ipString = "192.168.0.11";
    string ipString = "127.0.0.1";
    System.Net.IPAddress ipAdd;
    int port = 2001;
    System.Net.Sockets.TcpListener listener;
    // 接続要求があったら受け入れる
    System.Net.Sockets.TcpClient client;
    // NetworkStreamを取得データの流れ
    System.Net.Sockets.NetworkStream ns;
    System.Text.Encoding enc;
    bool disconnected = false;
    System.IO.MemoryStream ms;
    byte[] resBytes = new byte[256];
    int resSize = 0;
    public bool isConenct = false;

    Thread receiver;

    // Use this for initialization
    void Start()
    {
        ipAdd = System.Net.IPAddress.Parse(ipString);
        listener = new System.Net.Sockets.TcpListener(ipAdd, port);
        listener.Start();
        receiver = new Thread(new ThreadStart(ConnectWait));
        receiver.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (isConenct == false)
        {
            return;
        }

        //データの一部を受信する
        resSize = ns.Read(resBytes, 0, resBytes.Length);

        if (resSize == 0)
        {
            disconnected = true;
            resBytes[0] = 0;
        }

        if (resBytes[0] == 0 || resBytes[0] == '\n')
        {
            disconnected = true;
        }
        else if (resBytes[0] == '/')
        {
            ms.Write(resBytes, 0, resSize);
            string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            resBytes[0] = 0;
        }
        else
        {
            //受信したデータを蓄積する
            ms.Write(resBytes, 0, resSize);
            //受信したデータを文字列に変換
            string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            //ログに入力内容を表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog(resMsg);
            while (true)
            {
                resMsg = resMsg.TrimEnd('\n');
                resBytes[0] = 0;
                break;
            }
        }


        if (disconnected == true)
        {
            DisConnect();
        }

    }

    private void DisConnect()
    {
        ns.Close();
        client.Close();
        client.Dispose();

        listener.Stop();
        isConenct = false;

        receiver.Abort();
        receiver = new Thread(new ThreadStart(ConnectWait));
        receiver.Start();
    }

    void ConnectWait()
    {
        listener.Start();
        client = listener.AcceptTcpClient();
        ns = client.GetStream();
        ns.ReadTimeout = 1000;
        ns.WriteTimeout = 1000;
        enc = System.Text.Encoding.UTF8;
        ms = new System.IO.MemoryStream();
        isConenct = true;
    }
}