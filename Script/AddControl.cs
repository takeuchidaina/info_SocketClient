using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddControl : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasCheck; //生成不可通知パネル
    private bool isPush;            //押されフラグ
    

    private void Start()
    {
        //オブジェクトを格納
        canvasCheck = GameObject.Find("Canvas_Check");

        //オブジェクトを非表示に
        canvasCheck.SetActive(false);

        //初期化
        isPush = false;
    }

    private void Update()
    {
        //ボタンが押された場合
        if(isPush == true)
        {
            //サーバー数が最大ではない場合
            if (ServerList.NowServerNum < ServerList.SERVER_NUM)
            {
                SceneManager.LoadScene("RemoteClient");
            }
            else
            {
                //オブジェクトを表示に
                canvasCheck.SetActive(true);
            }
        }
    }

    public void CheckPush()
    {
        //オブジェクトを非表示に
        canvasCheck.SetActive(false);

        //押されフラグをfalseに
        isPush = false;
    }

    public void AddPush()
    {
        //isPushがfalseの場合true切り替える
        if(isPush == false)
        {
            isPush = true;
        }
    }
}
