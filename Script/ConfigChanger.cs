using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigChanger : MonoBehaviour
{
    string objName = "テキストボックス";
    string text;
    [SerializeField]
    ChangeSlashCommand changeSCommand;  //スラッシュコマンド生成クラスのオブジェクト
    [SerializeField]
    JsonReader jsonReader;          //Json形式の情報を保持するクラスのオブジェクト

    private void Start()
    {
        changeSCommand = this.GetComponent<ChangeSlashCommand>();
        jsonReader = GameObject.Find("LoadObject").GetComponent<JsonReader>();
    }

    public void ChangeConfig()
    {
        //設定項目UIを格納する配列変数を宣言
        GameObject[] configObject = new GameObject[jsonReader.MyTableCnt];

        //設定項目UIを格納する
        for(int i = 0; i < jsonReader.MyTableCnt; i++)
        {
            //NameListのUIを格納する
            configObject[i] = GameObject.Find(jsonReader.NameList[i]);

            //データのタイプによって処理を変える
            switch (jsonReader.MyTableType[jsonReader.NameList[i]].type)
            {
                //Init型
                case eType.Int:
                    Debug.Log("おはよう");
                    break;
                //Double型
                case eType.Double:
                    Debug.Log("だぶる");
                    break;
                //String型
                case eType.String:
                    Debug.Log("すとりんぐ");
                    break;
                //Bool型
                case eType.Bool:
                    Debug.Log("ぶーる");
                    break;
            }
        }


        for (int i = 0; i < jsonReader.MyTableCnt; i++)
        {
            changeSCommand.ChangeCommand(jsonReader.NameList[i],
                jsonReader.MyTable[jsonReader.NameList[i]]);
        }

        //text = myText[1].text;

        //sendSCommand.ChangeSlashCommand(objName, text);

        //myText = null;
    }
}
