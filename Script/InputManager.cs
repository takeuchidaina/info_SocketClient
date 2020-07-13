﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    InputField inputField;      //InputFieldを格納

    void Start()
    {
        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField_Conversation").GetComponent<InputField>();

        //入力待機状態にする
        inputField.Select();
    }

    void Update()
    {
        //エンターキーが押されたら内容を送信する
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetInput();
        }
    }

    //入力された内容を読み取ってコンソールに出力する関数
    //inspector上のOnEndEditにて選択
    //Enterが押されると呼ばれる
    public int GetInput()
    {
        //InputFieldからテキスト情報を取得する
        string inputted = inputField.text;

        //入力内容のチェック
        if (CheckTheInput(inputted) == false)
        {
            return 0;
        }

        //TODO:/コマンドの実装


        //ログに入力内容を表示する
        GameObject.Find("Text_Log").GetComponent<Log>().AddLog(inputted);
        //サーバーに送信
        GameObject.Find("ServerConnect").GetComponent<ServerConnect>().SendServer(inputted);

        /*
        //サーバーに入力内容を送信
        if (GameObject.Find("ServerConnect").GetComponent<ServerConnect>().SendServer(inputted) == 0)
        {
            //ログに入力内容を表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog(inputted);
        }
        else
        {
            //ログにエラーを表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog("送信に失敗しました");
        }
        */

        //入力フォームのテキストを空にする
        inputField.text = "";

        //入力待機状態にする
        inputField.Select();

        return 0;
    }

    private bool CheckTheInput(string _msg)
    {
        /*必ずif文が通った際にreturnでfalseが返せるようにする*/

        //文字数を確認　0文字ならfalse
        if (_msg == "") { return false; }
        //説明
        //else if(){return false;}

        return true;
    }

}