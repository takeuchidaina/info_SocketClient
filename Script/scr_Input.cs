using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*obj_Input_L用のスクリプトです         */
/*入力された値をinputValueに格納してます*/
public class scr_Input : MonoBehaviour
{
    InputField inputField;
    string inputValue;


    void Start()
    {

        inputField = GetComponent<InputField>();

        InitInputField();

    }

    public void InputLogger()
    {
        inputValue = inputField.text;
        //ここで代入
        Debug.Log("obj_Input_L :" + inputValue);
        //ログ表示
        InitInputField();
    }

    public string TextValue
    {
        //ゲッター
        get{ return this.inputValue; }
    }

    void InitInputField()
    {
        inputField.text = "";
        // 値をリセット(フォーカスを外す)
        inputField.text = inputValue;
        //フォーカスを外した後に値を入れることで入力内容がわかる

        //inputField_S.ActivateInputField();
        // フォーカス
    }
}
