using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//クライアント側の状態
public enum eClient
{
    Connect,
    Direct,
    Add,
    Remove,
    None
}

public class GameManager : MonoBehaviour
{
    private static eClient clientState = eClient.None;  //状態管理用変数
    private static bool isChangeScene = false;          //シーン切り替えしてるかどうか

    //状態管理用変数のゲッター、セッター
    public static eClient ClientState
    {
        get { return clientState; }
        set { clientState = value; }
    }

    public static bool IsChangeScene
    {
        get { return isChangeScene; }
    }

    private void Start()
    {
        isChangeScene = false;
    }

    //接続ボタンが押されたときの処理
    public void Push_ConnectButton()
    {
        clientState = eClient.Connect;
        isChangeScene = true;
    }

    //ダイレクト接続が押されたときの処理
    public void Push_DirectButton()
    {
        clientState = eClient.Direct;
        isChangeScene = true;
    }

    //サーバー追加が押されたときの処理
    public void Push_AddButton()
    {
        clientState = eClient.Add;
        isChangeScene = true;
    }

    //削除が押されたときの処理
    public void Push_RemoveButton()
    {
        clientState = eClient.Remove;
        isChangeScene = true;
    }

}
