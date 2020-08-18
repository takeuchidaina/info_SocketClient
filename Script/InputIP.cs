using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;//シーン判別に使う
using UnityEngine;
using UnityEngine.UI;//UIのInputFiledを使用するので追加しないとダメ
public class InputIP : MonoBehaviour
{
    private string ip;
    private string listName;

    public InputField IP;//入力されたIPを読み込む
    public InputField Name;//入力されたIPを読み込む

    public string Ip
    {
        get { return this.ip; }  //取得用
    }

    public string ListName
    {
        get { return this.listName; }  //取得用
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "DirectSendScene")
        {   //ダイレクト接続シーンでのみやりたい処理

            if (UnityEngine.Input.GetKeyDown(KeyCode.Return) && IP.text != "")
            {
                EndInput();
            }
        }
        else
        {
            //どちらも入力されていて
            //エンターキーが押されたら内容を送信する
            if (UnityEngine.Input.GetKeyDown(KeyCode.Return) &&
                IP.text != "" && Name.text != "")
            {
                EndInput();
            }
        }
    }

    public void EndInput()//入力終了
    {
        if (SceneManager.GetActiveScene().name == "DirectSendScene")
        {   //ダイレクト接続シーンでのみやりたい処理

            if (IP.text != "")
            {
                ip = IP.text;

                //入力したテキストを表示
                Debug.Log("ダイレクト接続");
                Debug.Log("サーバーIP:" + ip);

                //入力し終わったらテキストを消す
                IP.text = "";
            }
        }
        else
        {
            if (IP.text != "" && Name.text != "")
            {
                ip = IP.text;
                name = Name.text;

                //入力したテキストを表示
                Debug.Log("接続");
                Debug.Log("サーバー名:" + name);
                Debug.Log("サーバーIP:" + ip);

                //入力し終わったらテキストを消す
                IP.text = "";
                Name.text = "";
            }
        }
    }
}

