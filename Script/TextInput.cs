using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

//全角かどうか調べるやつ
//参考サイト:https://qiita.com/azumagoro/items/42be0f33fd7a6cb345b9


public class TextInput : MonoBehaviour
{
    private static string input;//入力されたinPutを読み込む

    public InputField inputField;

    //ゲッター
    public static string Get_Input
    {
        get { return input; }
    }


    void Update()
    {
        //対象のInputFieldが選択されている
        if (inputField.GetComponent<InputField>().isFocused == true)
        {
            inputField.GetComponent<Image>().color = Color.green;

            //名前がIP_InputFieldオブジェクトだったら
            if (inputField.name == "IP_InputField")
            {
                string str = "";
                //ここの処理は数字と.のみ入力を受け付けたいのでその処理がされてます
                //文字を見つける処理
                var character = "1234567890.";

                //inputFieldの長さを取得してその分for文で回す
                for (int i = 0; i < inputField.text.Length; i++)
                {
                    //characterの長さを取得してその分for文で回す
                    for (int j = 0; j < character.Length; j++)
                    {
                        if (inputField.text[i] == character[j])
                        {
                            str += inputField.text[i];
                        }
                    }
                }
                //精査したやつを入れなおす
                inputField.text = str;
            }
        }
        else
        {
            inputField.GetComponent<Image>().color = Color.white;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Return) && inputField.text != "")
        {
            EndInput();
        }
    }

    public void EndInput()//入力終了
    {
        if (inputField.text != "")
        {
            input = inputField.text;
            Debug.Log(input);
            inputField.text = "";
        }
    }
}
