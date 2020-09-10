using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerRoomNode : MonoBehaviour
{
    private ServerRoom serverRoom;  //サーバールーム情報を格納する変数
    private bool isSelect;          //選択されてるかの判定フラグ

    //サーバールーム情報を格納する変数のプロパティ
    public ServerRoom ServerRoom
    {
        get { return this.serverRoom; }
        set { this.serverRoom = value; }
    }

    //選択されてるかの判定フラグのプロパティ
    public bool IsSelect
    {
        get { return this.isSelect; }
        set { this.isSelect = value; }
    }

    private void Awake()
    {
        isSelect = false;
    }

    private void Start()
    {
        //各テキストの変更処理
        GameObject serverRoomName = GameObject.Find("ServerRoomName");
        GameObject serverIP = GameObject.Find("ServerIP");
        GameObject serverLastConnect = GameObject.Find("ServerLastConnect");

        Text nameText = serverRoomName.GetComponent<Text>();
        Text IPText = serverIP.GetComponent<Text>();
        Text lastConnectText = serverLastConnect.GetComponent<Text>();

        nameText.text = serverRoom.ServerRoomName;
        IPText.text = "IP : " + serverRoom.ServerRoomIP;
        lastConnectText.text = "最終接続日 : " + serverRoom.ServerRoomLastConnect;
    }

    private void Update()
    {
        //選択されてない場合
        if(isSelect == false)
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    //サーバーリスト選択処理
    public void Push_Botton()
    {
        isSelect = true;
        this.GetComponent<Image>().color = Color.cyan;

        //サーバーリストクラスへサーバールーム情報を送る
        GameObject serverList = GameObject.Find("ServerList");
        serverList.GetComponent<ServerList>().SelectServerRoom(serverRoom);
    }
}
