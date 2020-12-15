using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSlashCommand : MonoBehaviour
{
    [SerializeField]
    private List<string> slashCommands;  //出力する / コマンドを格納する変数

    private bool isSend = false;    //送りフラグ
    private bool isFinish;  //終了フラグ※初回ループ回避のためtrueにしておく
    private int sendCnt;

    private void Start()
    {
        sendCnt = 0;
        isFinish = true;
    }

    private void Update()
    {

        Debug.Log("SendCnt = " + sendCnt);
        Debug.Log("isFinish = " + isFinish);
        Debug.Log("isSend = " + isSend);

        /****************************************************************
        異常が発生しているまたは送りカウンタ+1が/コマンドの数を超える場合
        送りフラグをオフにする
        ****************************************************************/
        if (isFinish == false ||
           sendCnt >= slashCommands.Count)
        {
            isSend = false;
        }

        //送りフラグをオンの場合
        if(isSend == true)
        {
            //終了フラグをオフにする
            isFinish = false;

            //コマンドをサーバーサイドに送信する
            SendSCommand();

            //終了フラグをオンにする
            isFinish = true;

            //送りカウンタを加算する
            sendCnt++;
        }
    }

    public void SetSlashCommands(List<string> _slashCommands)
    {
        slashCommands = _slashCommands;

        //変更項目がない場合は送りフラグをオンにしない
        if(slashCommands.Count != 0)
        {
            isSend = true;
        }
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
