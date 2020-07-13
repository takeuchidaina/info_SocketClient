using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Log : MonoBehaviour
{
    public Text logs;   //ログが出力されるテキストボックス

    // Start is called before the first frame update
    void Start()
    {
        logs = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ログに文字列を追加する
    public void AddLog(string _input)
    {
        logs.text += TimeStamp() + _input + "\n";
    }

    //タイムスタンプの文字列を返す
    private string TimeStamp()
    {
        return "[" + DateTime.Now.ToLongTimeString() + "] ";
    }
}
