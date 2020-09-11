using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteCheckText : MonoBehaviour
{

    private Text targetText;      //テキスト

    // Start is called before the first frame update
    void Start()
    {
        this.targetText = this.GetComponent<Text>();    //テキストオブジェクト取得
    }

    // Update is called once per frame
    void Update()
    {
        this.targetText.text = ServerList.SelectServer.ServerRoomName + "を\n本当に削除しますか？";
    }
}
