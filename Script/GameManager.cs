using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//クライアント側の状態
public enum ClientState
{
    Connect,    //接続
    Edit,       //編集
    Add,        //追加
    Remove,     //削除
    Wait        //待機
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //インスタンス

    private ClientState clientState = ClientState.Wait;    //クライアントの現在の状態

    private void Awake()
    {
        Instance = this;
    }

    //クライアントの状態を取得する
    public ClientState GetClientState()
    {
        return clientState;
    }
}
