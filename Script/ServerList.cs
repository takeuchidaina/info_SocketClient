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

    //サーバーの最終接続日のゲッター
    public string ServerRoomLastConnect
    {
        get { return this.serverRoomLastConnect; }
        set { this.serverRoomLastConnect = value; }
    }

    //初期生成用のコンストラクタ
    public ServerRoom(string _name,string _IP)
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
    private GameObject content;     //サーバールームを格納する場所
    private GameObject serverRoomPrefab;    //サーバールームObject
    [SerializeField]
    private List<ServerRoom> serverRoomList = new List<ServerRoom>();    //サーバーリスト
    private static ServerRoom selectServerRoom = null;
    public const int SERVER_NUM = 5;        //サーバーの数//選択されているサーバールーム
    private static int nowServerNum = 0;    //現在のサーバーの数

    private string filePath;                    //ファイルパス
    private string fileName = "ServerList.txt"; //ファイル名

    //選択されているサーバールームのゲッター
    public static ServerRoom SelectServer
    {
        get { return selectServerRoom; }
    }

    //現在のサーバーの数のゲッター
    public static int NowServerNum
    {
        get { return nowServerNum; }
    }

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

                serverRoomList.Add(new ServerRoom(bufs[0], bufs[1], bufs[2], bufs[3]));
            }
        }

        //サーバーの数を初期化
        nowServerNum = serverRoomList.Count;
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

        SceneManager.sceneLoaded += SceneLoaded;
    }

    //毎フレーム呼ばれる処理
    private void Update()
    {
        //ClientStateがConnectの場合
        if (GameManager.ClientState == eClient.Connect)
        {
            GameManager.ClientState = eClient.None;

            ConnectServerList();
        }

        //ClientStateがAdd かつ シーンが切り替わってない場合
        if(GameManager.ClientState == eClient.Add  && GameManager.IsChangeScene == false)
        {
            GameManager.ClientState = eClient.None;

            //キャンセルボタンが押されていた場合処理をおこなわない
            if (CancelButton.CancelFlg == true)
            {
                return;
            }

            AddServerList(SendButton.Get_Name, SendButton.Get_IP);
        }

        Debug.Log("IsCreate : "+AddControl.IsCreate);
    }

    //オブジェクトが破棄される際に呼ばれる処理
    private void OnDestroy()
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

        if(selectServerRoom == null)
        {
            return;
        }

        //最終接続日を入力
        serverRoomList[serverRoomList.IndexOf(selectServerRoom)].ServerRoomLastConnect =
            dt.Year.ToString() + "/" + dt.Month.ToString() + "/" + dt.Day.ToString() + "/" +
            dt.Hour.ToString() + ":" + dt.Minute.ToString();
    }

    //サーバーリストにサーバールームを新規追加する処理
    public void AddServerList(string _name, string _IP)
    {
        //生成フラグをfalseの場合生成を行わない
        if(AddControl.IsCreate == false)
        {
            return;
        }

        //サーバーリストにサーバールームを新規追加
        serverRoomList.Add(new ServerRoom(_name, _IP));

        //サーバールームをオブジェクトとして生成
        GameObject node = Instantiate(serverRoomPrefab, content.transform) as GameObject;

        //オブジェクトの名前をサーバーの名＋識別番号に変更
        node.name = serverRoomList[serverRoomList.Count - 1].ServerRoomName +
            serverRoomList[serverRoomList.Count - 1].ServerRoomIdentNum;

        //ServerRoomNodeを取得
        ServerRoomNode serverRoomNode = node.GetComponent<ServerRoomNode>();

        //追加したサーバーを取得したサーバールームに適用
        serverRoomNode.ServerRoom = serverRoomList[serverRoomList.Count - 1];

        //サーバーの数を適用
        nowServerNum = serverRoomList.Count;
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

        //サーバーの数を適用
        nowServerNum = serverRoomList.Count;
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

    void SceneLoaded(Scene nextScene,LoadSceneMode mode)
    {
        //次のシーンがAddServerSceneの場合
        if(nextScene.name == "AddServerScene")
        {
            //AddControlにServerListを渡す
            AddControl addControl = GameObject.Find("AddControl").GetComponent<AddControl>();
            addControl.GetServerList(serverRoomList);
        }

        if(nextScene.name == "ManipulateScene")
        {
            //ServerConnectにSelectServerのIP情報を渡す
            ServerConnect serverConnect = 
                GameObject.Find("ServerConnect").GetComponent<ServerConnect>();
            serverConnect.IpOrHost = selectServerRoom.ServerRoomIP;

            //接続情報を渡したのでnullリセットをする
            selectServerRoom = null;
        }

        SceneManager.sceneLoaded -= SceneLoaded;
    }
}