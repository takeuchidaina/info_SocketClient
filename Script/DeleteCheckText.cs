using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteCheckText : MonoBehaviour
{

    private Text targetText;      //テキスト
    GameObject obj;               //ゲームオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        this.targetText = this.GetComponent<Text>();    //テキストオブジェクト取得
        obj = GameObject.Find("ServerList");  //オブジェクトを格納;
    }

    // Update is called once per frame
    void Update()
    {
        ServerList list = obj.GetComponent<ServerList>();
        this.targetText.text = list.SelectServer.ServerRoomName + "を\n本当に削除しますか？";
    }
}
