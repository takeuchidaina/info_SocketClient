using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EditControl : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasCheck; //生成不可通知パネル

    [SerializeField]
    private Text warningMessage;    //警告メッセージ

    [SerializeField]
    private InputField serverName;  //サーバーの名前

    [SerializeField]
    private InputField serverIP;    //サーバーIP

    private void Start()
    {
        //オブジェクトを格納
        canvasCheck = GameObject.Find("Canvas_Check");

        //オブジェクトを非表示に
        canvasCheck.SetActive(false);
    }

    public void EditButton()
    {
        //新規サーバーの名前が存在する場合の処理
        if (ServerList.Instance.CheckHitName(serverName.text))
        {
            ErrorProc("既存のサーバー名が存在するため\n保存できませんでした");
            return;
        }

        //サーバー名またはIPが入力されていない場合の処理
        if (serverName.text == "" || serverIP.text == "")
        {
            ErrorProc("サーバー名またはIPが入力されてない為\n保存できませんでした");
            return;
        }

        ServerList.Instance.EditServer(serverName.text, serverIP.text);

        //サーバーリスト画面へ戻る
        SceneManager.LoadScene("RemoteClient");
    }

    //エラーの際の処理
    private void ErrorProc(string _warningMessage)
    {
        //オブジェクトを表示に
        canvasCheck.SetActive(true);

        //メッセージを設定する
        warningMessage.text = _warningMessage;
    }

    public void OKButton()
    {
        //オブジェクトを非表示に
        canvasCheck.SetActive(false);
    }
}
