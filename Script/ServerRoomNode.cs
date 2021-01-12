using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*****************************************************************************************************

*****************************************************************************************************/

enum TextType
{
    Name,
    IP,
    LastConnect
}

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
        Change_UI();
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

    public void Change_UI()
    {
        //テキスト名前をサーバーリストのものに変更
        Text[] tests = this.GetComponentsInChildren<Text>();

        tests[(int)TextType.Name].text = serverRoom.ServerRoomName;
        tests[(int)TextType.IP].text = "IP : " + serverRoom.ServerRoomIP;
        tests[(int)TextType.LastConnect].text = "最終接続日 : " + serverRoom.ServerRoomLastConnect;
    }
}
