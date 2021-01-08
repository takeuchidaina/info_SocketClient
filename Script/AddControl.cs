using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddControl : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasCheck; //生成不可通知パネル
    private bool isPush;            //押されフラグ
    private bool isMatch;           //サーバー名一致フラグ
    private static bool isCreate;   //サーバー生成フラグ

    private List<ServerRoom> serverRoomList;    //サーバーリスト

    [SerializeField]
    private Text warningMessage;    //警告メッセージ
    
    //サーバー作成フラグのゲッター
    public static bool IsCreate
    {
        get { return isCreate; }
    }
    

    private void Start()
    {
        //オブジェクトを格納
        canvasCheck = GameObject.Find("Canvas_Check");

        //オブジェクトを非表示に
        canvasCheck.SetActive(false);

        //初期化
        isPush = false;
        isMatch = false;
        isCreate = false;
    }

    private void Update()
    {
        //ボタンが押され、サーバー名が不一致の場合
        if (isPush == true && isMatch == false)
        {
            //サーバー数が最大ではない場合
            if (ServerList.NowServerNum < ServerList.SERVER_NUM &&
                (SendButton.Get_IP != "" && SendButton.Get_Name != ""))
            {
                //生成フラグをtrueに
                isCreate = true;
                SceneManager.LoadScene("RemoteClient");
            }
            else if(SendButton.Get_IP == "" || SendButton.Get_Name == "")
            {
                //オブジェクトを表示に
                canvasCheck.SetActive(true);

                warningMessage.text = "サーバー名またはIPが入力されてない為\n作成できませんでした";
            }
            else
            {
                //オブジェクトを表示に
                canvasCheck.SetActive(true);

                warningMessage.text = "サーバー数が最大なため\n作成できませんでした";
            }
        }
        else if(isPush == true && isMatch == true)
        {
            //オブジェクトを表示に
            canvasCheck.SetActive(true);

            warningMessage.text = "既存のサーバー名が存在するため\n生成できませんでした";
        }
    }

    public void CheckPush()
    {
        //オブジェクトを非表示に
        canvasCheck.SetActive(false);

        //押されフラグをfalseに
        isPush = false;

        //サーバー名一致フラグがtrueの場合falseに
        if(isMatch == true)
        {
            isMatch = false;
        }
    }

    public void AddPush()
    {
        //isPushがfalseの場合true切り替える
        if(isPush == false)
        {
            isPush = true;
            CheckCreateServer();
        }
    }

    private void CheckCreateServer()
    {
        //作成予定の名前を現在のサーバーリストと比較し一致してるか判定を行う
        foreach (var serverRoom in serverRoomList)
        {
            if (serverRoom.ServerRoomName == SendButton.Get_Name)
            {
                isMatch = true;
                return;
            }
        }
    }

    public void GetServerList(List<ServerRoom> _serverList)
    {
        //ServerList classから受け取った情報を格納する
        serverRoomList = _serverList;
    }
}
