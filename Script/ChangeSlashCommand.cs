using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSlashCommand : MonoBehaviour
{
    [SerializeField]
    private List<string> slashCommands;  //出力する / コマンドを格納する変数

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    //設定項目をSlashCommand化する処理
    public void ChangeCommand<T>(string _settingName, T _settingVariable)
    {
        //JsonReaderの要素を参照したいため読み込む
        JsonReader jsonReader = GameObject.Find("LoadObject").GetComponent<JsonReader>();

        //現在保存したコマンドの数が要素数を超える場合処理を中断する
        if (slashCommands.Count >= jsonReader.MyTableCnt)
        {
            return;
        }

        //スラッシュコマンド化したテキストを保存
        slashCommands.Add("/" + _settingName + " " + _settingVariable);
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        if (nextScene.name == "ManipulateScene")
        {
            SendSlashCommand sendSCommand =
                GameObject.Find("SendSlashCommand").GetComponent<SendSlashCommand>();

            //接続シーンにて使うCommandを送信する
            sendSCommand.SetSlashCommands(slashCommands);
        }

        SceneManager.sceneLoaded -= SceneLoaded;
            
    }
}
