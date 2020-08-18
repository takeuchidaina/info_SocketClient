using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerRoomNode : MonoBehaviour
{
    private ServerRoom serverRoom;  //サーバールーム情報を格納する変数

    //サーバールーム情報を格納する変数のプロパティ
    public ServerRoom ServerRoom
    {
        get { return this.serverRoom; }
        set { this.serverRoom = value; }
    }

    private void Start()
    {
        //表示する名前を変更
        Text nameText = this.GetComponentInChildren<Text>();
        nameText.text = serverRoom.ServerRoomName;
    }

    //サーバーリスト選択処理
    public void Push_Botton()
    {
        //サーバーリストクラスへサーバールーム情報を送る
        GameObject serverList = GameObject.Find("ServerList");
        serverList.GetComponent<ServerList>().SelectServerRoom(serverRoom);
    }
}
