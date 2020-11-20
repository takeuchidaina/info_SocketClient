using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InPut : MonoBehaviour
{
    private static string input;//入力されたinPutを読み込む

    public InputField inPut;

    //ゲッター
    public static string Get_Input
    {
        get { return input; }
    }

    void Update()
    {
        //対象のInputFieldが選択されている
        if (inPut.GetComponent<InputField>().isFocused == true)
        {
            inPut.GetComponent<Image>().color = Color.green;
        }
        else
        {
            inPut.GetComponent<Image>().color = Color.white;
        }

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
