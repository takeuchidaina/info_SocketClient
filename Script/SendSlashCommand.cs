using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSlashCommand : MonoBehaviour
{
    [SerializeField]
    private List<string> slashCommands;  //出力する / コマンドを格納する変数

    private bool isSend = false;    //送りフラグ
    private int sendCnt;

    private void Start()
    {
        sendCnt = 0;
    }

    private void Update()
    {
        Debug.Log("isSend : "+isSend);
        Debug.Log("sendCnt : " + sendCnt);

        if (isSend == true && sendCnt >= slashCommands.Count)
        {
            isSend = false;
        }
        else if(isSend == true && sendCnt < slashCommands.Count)
        {
            sendCnt++;
        }

        //送りフラグがオンの場合
        if (isSend == true)
        {
            SendSCommand();
        }
    }

    public void SetSlashCommands(List<string> _slashCommands)
    {
        slashCommands = _slashCommands;
        isSend = true;
    }

    private void SendSCommand()
    {
        if (GameObject.Find("ServerConnect").
            GetComponent<ServerConnect>().SendServer(slashCommands[sendCnt]) != 0)
        {
            //ログにエラーを表示する
            GameObject.Find("Text_Log").GetComponent<Log>().AddLog("送信に失敗しました");
            //1度でも接続が失敗したら中断する
            isSend = false;
        }
    }
}
