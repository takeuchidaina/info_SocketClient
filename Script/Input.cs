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

            //名前がIP_InputFieldオブジェクトだったら
            if (inPut.name == "IP_InputField")
            {
                //ここの処理は数字と.のみ入力を受け付けたいのでその処理がされてます

                //文字を見つける処理
                var character = "!#$%&'()=~|`{+*}<>?-^@[;:],/\\QWERTYUIOPASDFGHJKLZXCVBNM_qwertyuiopasdfghjklzxcvbnm";

                //characterの長さを取得してその分for文で回す
                for (int i = 0; i < character.Length; i++)
                {
                    //Containsは指定したのを探索、結果をtrue,falseで返してくれる
                    //ToStringは変数をstringに変換してくれる
                    //Replaceは第一引数を第二引数に変換する機能
                    if (inPut.text.Contains(character[i].ToString()) == true)
                    {
                        inPut.text = inPut.text.Replace(character[i].ToString(), "");
                    }
                }
            }
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
