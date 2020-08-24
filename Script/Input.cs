using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIのInputFiledを使用するので追加しないとダメ

public class Input : MonoBehaviour
{
    private static string input;//入力されたinPutを読み込む

    public InputField inPut;

    //ゲッター
    public static string Get_Input()
    {
        return input;
    }

    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Return) && inPut.text != "")
        {
            EndInput();
        }
    }

    public void EndInput()//入力終了
    {
        if (inPut.text != "")
        {
            input = inPut.text;
            Debug.Log(input);
            inPut.text = "";
        }
    }
}
