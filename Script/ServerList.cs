using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/*****************************************************************************************************

*****************************************************************************************************/

public class ServerRoom
{
    private string serverRoomName;      //サーバーの名前
    private string serverRoomIP;        //サーバーのIP
    private string serverRoomIdentNum;  //サーバーの識別番号

    //サーバーの名前のゲッター
    public string ServerRoomName
    {
        get { return this.serverRoomName; }
    }

    //サーバーのIPのゲッター
    public string ServerRoomIP
    {
        get { return this.serverRoomIP; }
    }

    //サーバーの識別番号のゲッター
    public string ServerRoomIdentNum
    {
        get { return this.serverRoomIdentNum; }
    }

    //初期生成用のコンストラクタ
    public ServerRoom(string _name,string _IP)
    {
        DateTime dt = DateTime.Now;

        serverRoomName = _name;
        serverRoomIP = _IP;
        serverRoomIdentNum = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + dt.Millisecond.ToString();
    }

    //ファイル読み込み用のコンストラクタ
    public ServerRoom(string _name,string _IP,string _identNum)
    {
        serverRoomName = _name;
        serverRoomIP = _IP;
        serverRoomIdentNum = _identNum;
    }
}

public class ServerList : MonoBehaviour
{
    private GameObject content;                                         //サーバールームを格納する場所
    private GameObject serverRoomPrefab;                                //サーバールームObject
    private List<ServerRoom> serverRoomList = new List<ServerRoom>();   //サーバーリスト
    private ServerRoom selectServerRoom = null;                         //選択されているサーバールーム

    private string filePath;                    //ファイルパス
    private string fileName = "ServerList.txt"; //ファイル名

    //オブジェクト生成する際に行う処理
    private void Awake()
    {
        //ファイルパスを取得
        filePath = Application.dataPath + "/" + fileName;

        //オブジェクトを取得
        content = GameObject.Find("Content");
        serverRoomPrefab = (GameObject)Resources.Load("ServerRoomNode");

        //ファイル読み込み(現在テキストファイルでcsvみたいな方法で管理している)
        using(StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
        {
            while (!streamReader.EndOfStream)
            {
                string[] bufs = streamReader.ReadLine().Split(new char[] { ',', '\n' });

                serverRoomList.Add(new ServerRoom(bufs[0], bufs[1], bufs[2]));
            }
        }
    }

    //オブジェクト生成後行う処理
    private void Start()
    {
        //サーバールームをオブジェクトとして生成
        foreach(var serverRoom in serverRoomList)
        {
            GameObject node = Instantiate(serverRoomPrefab, content.transform) as GameObject;
            node.name = serverRoom.ServerRoomName + serverRoom.ServerRoomIdentNum;
            ServerRoomNode serverRoomNode = node.GetComponent<ServerRoomNode>();
            serverRoomNode.ServerRoom = serverRoom;
        }
    }

    //オブジェクトが破棄される際に呼ばれる処理
    private void OnDestroy()
    {
        //ファイル書き込み(現在テキストファイルでcsvみたいな方法で管理している)
        using (StreamWriter fileWrirer = new StreamWriter(filePath, false))
        {
            foreach (var serverRoom in serverRoomList)
            {
                fileWrirer.WriteLine(serverRoom.ServerRoomName + "," + serverRoom.ServerRoomIP + "," + serverRoom.ServerRoomIdentNum);
            }
        }
    }

    //サーバーリストにサーバールームを新規追加する処理
    public void AddServerList()
    {
        //サーバーリストにサーバールームを新規追加
        serverRoomList.Add(new ServerRoom("新規", "00.00.00.00"));

        //サーバールームをオブジェクトとして生成
        GameObject node = Instantiate(serverRoomPrefab, content.transform) as GameObject;
        node.name = serverRoomList[serverRoomList.Count - 1].ServerRoomName + 
            serverRoomList[serverRoomList.Count - 1].ServerRoomIdentNum;
        ServerRoomNode serverRoomNode = node.GetComponent<ServerRoomNode>();
        serverRoomNode.ServerRoom = serverRoomList[serverRoomList.Count - 1];
    }

    //選択されたサーバールームを削除する処理
    public void RemoveServerList()
    {
        //選択されたサーバールームがなかった場合処理をしない
        if(selectServerRoom == null)
        {
            return;
        }

        //サーバーリストから選択されたサーバールームを削除する
        serverRoomList.RemoveAt(serverRoomList.IndexOf(selectServerRoom));

        //選択されたサーバールームのオブジェクトを削除する
        Destroy(GameObject.Find(selectServerRoom.ServerRoomName + selectServerRoom.ServerRoomIdentNum));

        //選択状態を解除する
        selectServerRoom = null;
    }

    //サーバールームの選択処理
    public void SelectServerRoom(ServerRoom _serverRoom)
    {
        selectServerRoom = _serverRoom;
    }
}