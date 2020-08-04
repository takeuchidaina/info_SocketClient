using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIのInputFiledを使用するので追加しないとダメ
public class InPut : MonoBehaviour
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
        //どちらも入力されていて
        //エンターキーが押されたら内容を送信する
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return) &&
            IP.text != "" && Name.text != "")
        {
            EndInput();
        }
    }

    public void EndInput()//入力終了
    {
        if (IP.text != "" && Name.text != "")
        {
            ip = IP.text;
            name = Name.text;
        }

        //入力したテキストを表示
        Debug.Log("IP:" + ip);
        //入力したテキストを表示
        Debug.Log("Name:" + name);
        //入力されてのを送信する処理

        //入力し終わったらテキストを消す
        IP.text = "";
        Name.text = "";
    }

}

