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
    string str;
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
                var character = "!#$%&'()=~|`{+*}<>?-^@[;:],/\\QWERTYUIOPASDFGHJKLZXCVBNM_qwertyuiopasdfghjklzxcvbnm";

                //characterの長さを取得してその分for文で回す
                for (int i = 0; i < character.Length; i++)
                {
                    //Containsは指定したのを探索、結果をtrue,falseで返してくれる
                    //ToStringは変数をstringに変換してくれる
                    //Replaceは第一引数を第二引数に変換する機能
                    if (inputField.text.Contains(character[i].ToString()) == true)
                    {
                        inputField.text = inputField.text.Replace(character[i].ToString(), "");
                    }
                }

                for (int i = 0; i < inputField.text.Length; i++)
                {
                    //Debug.Log(inputField.text[i]+ "：" + Encoding.GetEncoding("Shift_JIS").GetByteCount(inputField.text[i].ToString()));
                    if (Encoding.GetEncoding("Shift_JIS").GetByteCount(inputField.text[i].ToString()) != 2)
                    {
                        str += inputField.text[i];
                    }
                }

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
