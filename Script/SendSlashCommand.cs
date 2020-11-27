using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSlashCommand : MonoBehaviour
{
    private string slashCommand;    //出力する / コマンドを格納する変数

    private void Start()
    {
        //出力するコマンドを初期化
        slashCommand = "";
    }

    private void Update()
    {
        ChangeSlashCommand("DefaultFixeduTalkEndTime", 0.0);
        ChangeSlashCommand("MaxTalkTime", 0.0);
        ChangeSlashCommand("FacingTimeout", 0.0);
        ChangeSlashCommand("AnnouncingTimeout", 0.0);
        ChangeSlashCommand("WebCamID", 0);
        ChangeSlashCommand("BufferCount", 0);
        ChangeSlashCommand("BufferLength", 0);
        ChangeSlashCommand("AnzuServerURL", "test");
        ChangeSlashCommand("MakinaServerURL", "test");
        ChangeSlashCommand("TSJapaneseTalker_VoiceFile", "test");
        ChangeSlashCommand("TSJapaneseTalker_DicPath", "test");
        ChangeSlashCommand("TSJapaneseTalker_UserDicFile", "test");
        ChangeSlashCommand("IsBannerManager", false);
        ChangeSlashCommand("IsWeatherManager", false);
        ChangeSlashCommand("IsInfraRedDerection", false);
        ChangeSlashCommand("IsFaceRecognizer", false);
        ChangeSlashCommand("IsLanguageButton", false);
    }

    //設定項目をSlashCommand化する処理
    private void ChangeSlashCommand<T>(string _settingName, T _settingVariable)
    {
        slashCommand = "/" + _settingName + " " + _settingVariable;

        Debug.Log(slashCommand);
    }
}
