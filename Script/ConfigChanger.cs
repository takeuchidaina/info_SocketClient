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
        //すべてのオブジェクトの値を格納するための
        Text[] myText = GameObject.Find(objName).GetComponentsInChildren<Text>();

        for (int i = 0; i < jsonReader.MyTableCnt; i++)
        {
            changeSCommand.ChangeCommand(jsonReader.NameList[i],
                jsonReader.MyTable[jsonReader.NameList[i]]);
        }

        //text = myText[1].text;

        //sendSCommand.ChangeSlashCommand(objName, text);

        myText = null;
    }
}
