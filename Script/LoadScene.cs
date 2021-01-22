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
        if (SceneManager.GetActiveScene().name == "SettingsChangeScene")
        {
            //SceneManager.LoadScene("EditServerScene");
            SceneManager.LoadScene("ManipulateScene");
            return;
        }

        if (SceneManager.GetActiveScene().name == "DirectSendScene")
        {
            SceneManager.LoadScene("ManipulateScene");
            return;
        }

        if (ServerList.SelectServer == null) return;

        SceneManager.LoadScene("ManipulateScene");

    }

    //リモートクライアントシーンに移動
    public void RemoteClient()
    {
        SceneManager.LoadScene("RemoteClient");
    }

    //サーバー編集シーンに移動
    public void EditServer()
    {
        if (ServerList.SelectServer == null) return;

        SceneManager.LoadScene("EditServerScene");
    }

    //設定変更シーンに移動
    public void SettingsChange()
    {
        if(ServerConnect.Flg == true && SceneManager.GetActiveScene().name == "ManipulateScene")
        {
            SceneManager.LoadScene("SettingsChangeScene");
        }
    }

}
