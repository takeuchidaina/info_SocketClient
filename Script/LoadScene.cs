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
    //リストが選択されてないときは移動しない
    public void SendButton()
    {

        if (SceneManager.GetActiveScene().name == "DirectSendScene")
        {
            SceneManager.LoadScene("ManipulateScene");
        }

        if (ServerList.SelectServer == null) return;

        SceneManager.LoadScene("ManipulateScene");
    }

    //リモートクライアントシーンに移動
    public void RemoteClient()
    {
        SceneManager.LoadScene("RemoteClient");
    }

}
