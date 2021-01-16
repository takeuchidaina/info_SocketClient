using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

/*****************************************************************************************************

*****************************************************************************************************/

[Serializable]
public class ServerRoom
{
    [SerializeField]
    private string serverRoomName;          //サーバーの名前
    [SerializeField]
    private string serverRoomIP;            //サーバーのIP
    [SerializeField]
    private string serverRoomIdentNum;      //サーバーの識別番号
    [SerializeField]
    private string serverRoomLastConnect;   //サーバーの最終接続日

    //サーバーの名前のゲッター・セッター
    public string ServerRoomName
    {
        get { return this.serverRoomName; }
        set { this.serverRoomName = value; }
    }

    //サーバーのIPのゲッター・セッター
    public string ServerRoomIP
    {
        get { return this.serverRoomIP; }
        set { this.serverRoomIP = value; }
    }

    //サーバーの識別番号のゲッター
    public string ServerRoomIdentNum
    {
        get { return this.serverRoomIdentNum; }
    }

    //サーバーの最終接続日のゲッター・セッター
    public string ServerRoomLastConnect
    {
        get { return this.serverRoomLastConnect; }
        set { this.serverRoomLastConnect = value; }
    }

    //初期生成用のコンストラクタ
    public ServerRoom(string _name, string _IP)
    {
        DateTime dt = DateTime.Now;

        serverRoomName = _name;
        serverRoomIP = _IP;
        serverRoomIdentNum = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() +
            dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() +
            dt.Millisecond.ToString();
        serverRoomLastConnect = "NoData";
    }

    //ファイル読み込み用のコンストラクタ
    public ServerRoom(string _name, string _IP, string _identNum, string _lastConnect)
    {
        serverRoomName = _name;
        serverRoomIP = _IP;
        serverRoomIdentNum = _identNum;
        serverRoomLastConnect = _lastConnect;
    }
}

public class ServerList : MonoBehaviour
{

    public static ServerList Instance;  //インスタンス

    private GameObject content;     //サーバールームを格納する場所
    private GameObject serverRoomPrefab;    //サーバールームObject
    [SerializeField]
    private List<ServerRoom> serverRoomList = new List<ServerRoom>();    //サーバーリスト
    private static ServerRoom selectServerRoom = null;  //選択されているサーバールーム
    public const int SERVER_NUM = 5;        //サーバーの数

    private string filePath;                    //ファイルパス
    private string fileName = "ServerList.txt"; //ファイル名

    //選択されているサーバールームのゲッター
    public static ServerRoom SelectServer
    {
        get { return selectServerRoom; }
    }

    //オブジェクト生成する際に行う処理
    private void Awake()
    {
        Instance = this;

        //ファイルパスを取得
        filePath = Application.persistentDataPath + "/" + fileName;

        //オブジェクトを取得
        content = GameObject.Find("Content");
        serverRoomPrefab = (GameObject)Resources.Load("ServerRoomNode");

        //ファイル読み込み(現在テキストファイルでcsvみたいな方法で管理している)
        using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
        {
            while (!streamReader.EndOfStream)
            {
                string[] bufs = streamReader.ReadLine().Split(new char[] { ',', '\n' });

                serverRoomList.Add(new ServerRoom(bufs[0], bufs[1], bufs[2], bufs[3]));
            }
        }

        //選択情報をリセット
        selectServerRoom = null;

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
        ListWrirer();
    }

    private void ListWrirer()
    {
        //ファイル書き込み(現在テキストファイルでcsvみたいな方法で管理している)
        using (StreamWriter fileWrirer = new StreamWriter(filePath, false))
        {
            foreach (var serverRoom in serverRoomList)
            {
                fileWrirer.WriteLine(serverRoom.ServerRoomName + "," + serverRoom.ServerRoomIP +
                    "," + serverRoom.ServerRoomIdentNum +
                    "," + serverRoom.ServerRoomLastConnect);
            }
        }
    }

    //サーバールームに接続する際の処理
    public void ConnectServerList()
    {
        DateTime dt = DateTime.Now;

        if (selectServerRoom == null)
        {
            return;
        }

        //最終接続日を入力
        serverRoomList[serverRoomList.IndexOf(selectServerRoom)].ServerRoomLastConnect =
            dt.Year.ToString() + "/" + dt.Month.ToString() + "/" + dt.Day.ToString() + "/" +
            dt.Hour.ToString() + ":" + dt.Minute.ToString();
    }

    //サーバーを追加する処理
    public void AddServer(string _name, string _IP)
    {
        //サーバーリストにサーバールームを新規追加
        serverRoomList.Add(new ServerRoom(_name, _IP));

        //追加したサーバーをファイルへ書き込む
        ListWrirer();
    }

    //名前が一致するサーバーが存在するかの判定処理
    public bool CheckHitName(string _name)
    {
        foreach(var serverRoom in serverRoomList)
        {
            //一致するサーバーがあった場合
            if(serverRoom.ServerRoomName == _name)
            {
                return true;
            }
        }

        //一致するサーバーがなかった場合
        return false;
    }

    //サーバー数が最大かの判定処理
    public bool CheckMaxServerNum()
    {
        //サーバーの数が最大の場合
        if(serverRoomList.Count >= SERVER_NUM)
        {
            return true;
        }

        //サーバーの数が最大ではない場合
        return false;
    }

    public void EditServer(string _name, string _IP)
    {
        //選択されているサーバールームの格納場所を取得する
        int serverRoomNum = serverRoomList.IndexOf(selectServerRoom);

        //書き換える
        serverRoomList[serverRoomNum].ServerRoomName = _name;
        serverRoomList[serverRoomNum].ServerRoomIP = _IP;

        //追加したサーバーをファイルへ書き込む
        ListWrirer();
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
        Destroy(GameObject.Find(selectServerRoom.ServerRoomName +
            selectServerRoom.ServerRoomIdentNum));

        //選択状態を解除する
        selectServerRoom = null;
    }

    //サーバールームの選択処理
    public void SelectServerRoom(ServerRoom _serverRoom)
    {
        //サーバールームが選択されてないとき
        if (selectServerRoom != null && selectServerRoom != _serverRoom)
        {
            GameObject node = GameObject.Find(selectServerRoom.ServerRoomName +
                selectServerRoom.ServerRoomIdentNum);
            ServerRoomNode serverRoomNode = node.GetComponent<ServerRoomNode>();

            serverRoomNode.IsSelect = false;
        }

        selectServerRoom = _serverRoom;
    }
}