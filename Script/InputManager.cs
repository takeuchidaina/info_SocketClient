using System.Collections;
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
    }

    //入力された内容を読み取ってコンソールに出力する関数
    //inspector上のOnEndEditにて選択
    //Enterが押されると呼ばれる
    public void GetInput()
    {
        //InputFieldからテキスト情報を取得する
        string inputted = inputField.text;
        Debug.Log(inputted);

        //入力フォームのテキストを空にする
        inputField.text = "";
    }
}