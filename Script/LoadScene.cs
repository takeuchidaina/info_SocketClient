using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //それぞれに対応するボタンが押されたら
    //それぞれのシーンに移動します

    //サーバーリスト追加シーンに移動
    public void AddServerButton()
    {
        SceneManager.LoadScene("AddServerScene");
    }

    //ダイレクト接続シーンに移動
    public void DirectSendButton()
    {
        SceneManager.LoadScene("DirectSendScene");
    }

    //接続シーンに移動
    public void SendButton()
    {
        SceneManager.LoadScene("SendScene");
    }

    //リモートクライアントシーンに移動
    public void RemoteClient()
    {
        SceneManager.LoadScene("RemoteClient");
    }

}
