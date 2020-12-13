using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigChanger : MonoBehaviour
{
    [SerializeField]
    ChangeSlashCommand changeSCommand;  //スラッシュコマンド生成クラスのオブジェクト
    [SerializeField]
    JsonReader jsonReader;              //Json形式の情報を保持するクラスのオブジェクト
    [SerializeField]
    GameObject[] configObject;          //自動生成された設定項目のUIオブジェクトを格納する配列 

    private void Start()
    {
        //スクリプトをオブジェクトから取得し、格納する
        changeSCommand = this.GetComponent<ChangeSlashCommand>();
        jsonReader = GameObject.Find("LoadObject").GetComponent<JsonReader>();

        //実体宣言
        configObject = new GameObject[jsonReader.MyTableCnt];
    }

    public void ChangeConfig()
    {

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
                    scr_Slider_int intData = configObject[i].GetComponent<scr_Slider_int>();
                    ChangeCommand(configObject[i].name, intData.TextValue);
                    break;
                //Double型
                case eType.Double:
                    scr_Slider doubleData = configObject[i].GetComponent<scr_Slider>();
                    ChangeCommand(configObject[i].name, doubleData.TextValue);
                    break;
                //String型
                case eType.String:
                    scr_Input stringData = configObject[i].GetComponent<scr_Input>();
                    ChangeCommand(configObject[i].name, stringData.TextValue);
                    break;
                //Bool型
                case eType.Bool:
                    scr_DropDown boolData = configObject[i].GetComponent<scr_DropDown>();
                    ChangeCommand(configObject[i].name, boolData.TextValue);
                    break;
            }

        }
    }

    void ChangeCommand(string _objName,string _value)
    {
        //_valueがjsonReaderの初期値と同じ場合処理を行わない
        if(jsonReader.MyTable[_objName] == _value)
        {
            return;
        }

        //スラッシュコマンド化させる
        changeSCommand.ChangeCommand(_objName, _value);
    }
}
