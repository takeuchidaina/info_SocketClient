using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;       //UnityJsonを使う場合に必要
using System.IO;    //ファイル書き込みに必要
using System.Runtime.Serialization.Json;
using System.Text;

// 入力されるJSONに合わせてクラスを作成
public class InputJson
{
    public double DefaultFixeduTalkEndTime;        //保存形式:double 変更形式:テキストボックス or シークバー  
    public double MaxTalkTime;                     //保存形式:double 変更形式:テキストボックス or シークバー
    public double FacingTimeout;                   //保存形式:double 変更形式:テキストボックス or シークバー
    public double AnnouncingTimeout;               //保存形式:double 変更形式:テキストボックス or シークバー
    public int    WebCamID;                        //保存形式:int    変更形式:テキストボックス or シークバー
    public int    BufferCount;                     //保存形式:int    変更形式:テキストボックス or シークバー
    public double BufferLength;                    //保存形式:double 変更形式:テキストボックス or シークバー

    /*"各種サーバーURL"                             String ドロップダウンリスト*/
    public string URL1;                            //保存形式:string 変更形式:テキストボックス
    public string URL2;                            //保存形式:string 変更形式:テキストボックス
    public string URL3;                            //保存形式:string 変更形式:テキストボックス

    public string NoisePath;                       //保存形式:string 変更形式:テキストボックス
    public string TSJapaneseTalker_VoiceFile;      //保存形式:string 変更形式:テキストボックス
    public string TSJapaneseTalker_DicPath;        //保存形式:string 変更形式:テキストボックス
    public string TSJapaneseTalker_UserDicFile;    //保存形式:string 変更形式:テキストボックス

    public bool   IsBannerManager;                 //保存形式:bool   変更形式:ダイアログボックス
    public bool   IsWeatherManager;                //保存形式:bool   変更形式:ダイアログボックス
    public bool   IsInfraRedDetection;             //保存形式:bool   変更形式:ダイアログボックス
    public bool   IsFaceRecognizer;                //保存形式:bool   変更形式:ダイアログボックス

}

public class JsonReader : MonoBehaviour
{
    // 保存するファイル名
    const string SAVE_FILE_PATH = "input.json";

    //jsonをstringで読み込む用
    string inputString;

    //jsonを入れる用のクラス
    public InputJson inputJson;
    public InputJson data;

    System.Diagnostics.Process p = null;

    //ファイルに書き込む
    void Write()
    {
        data = new InputJson();

        data.DefaultFixeduTalkEndTime = inputJson.DefaultFixeduTalkEndTime;
        data.MaxTalkTime = inputJson.MaxTalkTime;
        data.FacingTimeout = inputJson.FacingTimeout;
        data.AnnouncingTimeout = inputJson.AnnouncingTimeout;
        data.WebCamID = inputJson.WebCamID;
        data.BufferCount = inputJson.BufferCount;
        data.BufferLength = inputJson.BufferLength;
        data.URL1 = inputJson.URL1;
        data.URL2 = inputJson.URL2;
        data.URL3 = inputJson.URL3;
        data.NoisePath = inputJson.NoisePath;
        data.TSJapaneseTalker_VoiceFile = inputJson.TSJapaneseTalker_VoiceFile;
        data.TSJapaneseTalker_DicPath = inputJson.TSJapaneseTalker_DicPath;
        data.TSJapaneseTalker_UserDicFile = inputJson.TSJapaneseTalker_UserDicFile;
        data.IsBannerManager = inputJson.IsBannerManager;
        data.IsWeatherManager = inputJson.IsWeatherManager;
        data.IsInfraRedDetection = inputJson.IsInfraRedDetection;
        data.IsFaceRecognizer = inputJson.IsFaceRecognizer;

        // JSONにシリアライズ
        var json = JsonUtility.ToJson(data, true);

        // Assetsフォルダに保存する
        var path = Application.dataPath + "/Resources/" + SAVE_FILE_PATH;

        var writer = new StreamWriter(path, false);    //false:上書き書き込み
                                                       //true :追加書き込み
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }

    //デバッグログ表示
    void Log()
    {
        Debug.Log(inputJson.DefaultFixeduTalkEndTime);
        Debug.Log(inputJson.MaxTalkTime);
        Debug.Log(inputJson.FacingTimeout);
        Debug.Log(inputJson.AnnouncingTimeout);
        Debug.Log(inputJson.WebCamID);
        Debug.Log(inputJson.BufferCount);
        Debug.Log(inputJson.BufferLength);
        Debug.Log(inputJson.URL1);
        Debug.Log(inputJson.URL2);
        Debug.Log(inputJson.URL3);
        Debug.Log(inputJson.NoisePath);
        Debug.Log(inputJson.TSJapaneseTalker_VoiceFile);
        Debug.Log(inputJson.TSJapaneseTalker_DicPath);
        Debug.Log(inputJson.TSJapaneseTalker_UserDicFile);
        Debug.Log(inputJson.IsBannerManager);
        Debug.Log(inputJson.IsWeatherManager);
        Debug.Log(inputJson.IsInfraRedDetection);
        Debug.Log(inputJson.IsFaceRecognizer);
    }

    int IdentifyType<T>(T x)
    {
        if (x.GetType() == typeof(int))
        {
            return 1;
        }
        else
        if (x.GetType() == typeof(double))
        {
            return 2;
        }
        else
        if (x.GetType() == typeof(bool))
        {
            return 3;
        }
        else
        if (x.GetType() == typeof(string))
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }

    /*
     JsonUtility.ToJsonを知る前にやってた試行錯誤の残骸
     外部ファイルオープン＆クローズはなんかに使いそうだから残しときます

     int a;
     bool aflg;
    */

    void Start()
    {
        /*
         JsonUtility.ToJsonを知る前にやってた試行錯誤の残骸

         a = 0;
         aflg = false;
        */

        // 入力ファイルはAssets/Resources/input.json
        // input.jsonをテキストファイルとして読み取り、string型で受け取る
        inputString = Resources.Load<TextAsset>("input").ToString();

        // 上で作成したクラスへデシリアライズ
        inputJson = JsonUtility.FromJson<InputJson>(inputString);

        //ログ表示
        Log();
    }

    void Update()
    {
        /*
        JsonUtility.ToJsonを知る前にやってた試行錯誤の残骸

        if(aflg==true)
        {
            a++;
        }
        */

        // Sキーで変更とセーブの実行
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputJson.NoisePath = "うらべひろかず";
            inputJson.DefaultFixeduTalkEndTime = 1024;

            //書き込み
            Write();

            /*
            JsonUtility.ToJsonを知る前にやってた試行錯誤の残骸

            //メモ帳を起動する
            //p = System.Diagnostics.Process.Start(Application.dataPath + "/Resources/" + SAVE_FILE_PATH);

            aflg = true;
            */

            Debug.Log("変更：NoisePathとDefaultFixeduTalkEndTime");
            Debug.Log("変更：うらべひろかず、1024");
        }

        /*
        JsonUtility.ToJsonを知る前にやってた試行錯誤の残骸

        //開いたメモ帳を閉じる
        if (p != null && a > 15) 
        {
            p.Kill();
            p = null;
            a = 0;
            aflg = false;
        }
        */

        // Lキーでロード実行
        if (Input.GetKeyDown(KeyCode.L))
        {
            inputString = JsonUtility.ToJson(inputJson);

            //ログ表示
            Log();
        }

        // 変数の識別
        bool a = false;

        // 変数の中身表示
        if (IdentifyType(a)==1)
        {
            Debug.Log("intだよ");
        }
        else
        {
            Debug.Log("intじゃないよ");
        }

    }
}