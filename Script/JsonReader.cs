using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;       //UnityJsonを使う場合に必要
using System.IO;    //ファイル書き込みに必要
using System.Runtime.Serialization.Json;
using System.Text;
using System.Linq;
using System.Collections.Specialized;
using System.Threading;
//2020/11/19
// 入力されるJSONに合わせてクラスを作成

public enum eType
{
    Int,
    Double,
    String,
    Bool
}

public class InputJson
{
     //配列にも対応
      public string[] nameList = new string[] { };
      public string[] contentsList = new string[] { };
}

public class Type
{
    public string typeContentsList;
    public eType type;
}


public class JsonReader : MonoBehaviour
{

    Dictionary<string, string> myTable = new Dictionary<string, string>();

    public Dictionary<string, string> MyTable
    {
        get { return myTable; }
    }

    Dictionary<string, Type> myTableType = new Dictionary<string, Type>();


    public Dictionary<string, Type> MyTableType
    {
        get { return myTableType; }
    }

    int myTableCnt;

    public int MyTableCnt
    {
        get { return myTableCnt; }
    }

    // 保存するファイル名
    const string SAVE_FILE_PATH = "input.json";

    //jsonを入れる用のクラス
    public InputJson inputJson;
    List<string> nameList = new List<string>();
    List<string> contentsList = new List<string>();

    public List<string> NameList
    {
        get { return nameList; }
    }
    public List<string> ContentsList
    {
        get { return contentsList; }
    }

    public string[] textMessage; //テキストの加工前の一行を入れる変数
    public string[,] textWords; //テキストの複数列を入れる2次元は配列 

    private int rowLength; //テキスト内の行数を取得する変数
    private int columnLength; //テキスト内の列数を取得する変数

    private System.Diagnostics.Process P = null;
    void Start()
    {
        Load();
    }

    void Update()
    {
        // Sキーで変更とセーブの実行
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("セーブ実行");

            //書き込み
            Write();
        }

        // Lキーでリロード実行
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("ロード実行");

            for (int i = 0; i < 2; i++)
            {
                //パス指定
                var path = Application.dataPath + "/Resources/" + SAVE_FILE_PATH;

                //開く
                P = System.Diagnostics.Process.Start(path);

                //待つ
                Thread.Sleep(65);

                //閉じる
                P.Kill();

                //今の中身を削除
                MyTable.Clear();
                MyTableType.Clear();

                //こっちも削除する(空のやつ作成)
                List<string> _nameList = new List<string>();
                List<string> _contentsList = new List<string>();

                //空のやつを代入
                nameList = _nameList;
                contentsList = _contentsList;

                //リロード
                Load();

            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("testとしては配列の0番目を書き換えます");
            inputJson.nameList[0] = "aaaaa";
            inputJson.contentsList[0] = "11111";
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("中身の表示をします");

            foreach (string Key in myTableType.Keys)
            {
                Debug.Log("key:" + Key);
            }

            foreach (Type _type in myTableType.Values)
            {
                Debug.Log("dataType:"+_type.type + ":" + _type.typeContentsList);
            }

            for (int i = 0; i < MyTableCnt; i++)
            {
                Debug.Log("ContentsList:" + ContentsList[i]);
                Debug.Log("NameList:" + NameList[i]);
            }

            Debug.Log("MyTableCnt:" + MyTableCnt);
        }
    }
    //ファイルに書き込む
    void Write()
    {

        // Assetsフォルダに保存する
        var path = Application.dataPath + "/Resources/" + SAVE_FILE_PATH;

        var writer = new StreamWriter(path, false);    //false:上書き書き込み
                                                       //true :追加書き込み
        writer.WriteLine("{");

        writer.Flush();
        writer.Close();

        var wwriter = new StreamWriter(path, true);    //false:上書き書き込み
                                                       //true :追加書き込み
        //テーブルの要素ぶん回す
        for (int i = 0; i < myTable.Count; i++)
        {
            //文字を見つける処理
            var character = "!#$%&'()=~|`{+*}<>?-^@[;:],/QWERTYUIOPASDFGHJKLZXCVBNM_qwertyuiopasdfghjklzxcvbnm";
           
            bool flg = false;

            //characterの長さを取得してその分for文で回す
            for (int n = 0; n < character.Length; n++)
            {
                if (inputJson.contentsList[i].Contains(character[n].ToString()) == true)
                {
                    flg = true;
                }
            }

            if (i != myTable.Count - 1 && flg == true)
            {
                wwriter.WriteLine("    \"" + inputJson.nameList[i] + "\":\"" + inputJson.contentsList[i] + "\",");
            }
            else if (i != myTable.Count - 1 && flg == false)
            {
                wwriter.WriteLine("    \"" + inputJson.nameList[i] + "\":" + inputJson.contentsList[i] + ",");
            }
            else if (i == myTable.Count - 1 && flg == false)
            {
                wwriter.WriteLine("    \"" + inputJson.nameList[i] + "\":" + inputJson.contentsList[i] + ",");
            }
            else
            {
                wwriter.WriteLine("    \"" + inputJson.nameList[i] + "\":\"" + inputJson.contentsList[i] + "\"");
            }
        }

        wwriter.WriteLine("}");
        wwriter.Flush();
        wwriter.Close();

    }

    void Load()
    {
        //やり方ぐっちゃぐちゃ

        inputJson = new InputJson();

        // 入力ファイルはAssets/Resources/input.json
        var path = Application.dataPath + "/Resources/" + SAVE_FILE_PATH;

        //指定先のファイルが存在するなら
        if (!File.Exists(path))
        {
            Debug.Log("存在しません");

            //指定先にファイルを作成
            CreateDefaultJson();
        }

        //テキストを開く
        var reader = File.OpenText(path);

        //{ , }だけの行は数えないため-2にしておく
        var lineCount = -2;

        //テキストの中身が空ではない時
        while (reader.ReadLine() != null)
        {
            //テキストの行数をカウント
            lineCount++;
        }

        //これで配列サイズの変更ができる
        Array.Resize(ref inputJson.nameList, lineCount);
        Array.Resize(ref inputJson.contentsList, lineCount);

        //Resourcesフォルダから対象テキストを取得
        TextAsset textasset = Resources.Load("input", typeof(TextAsset)) as TextAsset;

        //テキスト全体をstring型で入れる変数を用意して入れる
        string TextLines = textasset.text;

        //Splitで一行づつを代入した1次配列を作成
        textMessage = TextLines.Split('\n');

        //行数と列数を取得
        columnLength = textMessage[0].Split('\t').Length;
        rowLength = textMessage.Length;

        //2次配列を定義
        textWords = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength - 1; i++)
        {
            //textMessageをカンマごとに分けたものを一時的にtempWordsに代入
            string[] tempWords = textMessage[i].Split('\t');

            for (int n = 0; n < columnLength; n++)
            {
                //{ , } がないことを確認
                if (tempWords[n].Contains("{") != true && tempWords[n].Contains("}") != true)
                {
                    //2次配列textWordsにカンマごとに分けたtempWordsを代入していく
                    textWords[i, n] = tempWords[n]; 

                    //入れ直す
                    string z = textWords[i, n];

                    //削除する文字の配列
                    char[] removeChars = new char[] { '"', ',', ' ' };

                    //削除する文字を1文字ずつ削除する
                    foreach (char c in removeChars)
                    {
                        z = z.Replace(c.ToString(), "");
                    }

                    //:で文字列を区切る
                    string[] d = z.Split(':');

                    //配列リサイズ
                    Array.Resize(ref inputJson.nameList, lineCount);
                    Array.Resize(ref inputJson.contentsList, lineCount);

                    //分割した文字をそれぞれのやつに入れていく
                    for (int c = 0; c < inputJson.nameList.Length; c++)
                    {
                        inputJson.nameList[c] = d[0];
                        inputJson.contentsList[c] = d[1];
                        break;
                    }

                    //改行文字が消えてくれないので一文字ずつ入れ直す方式を採用
                    string a = inputJson.contentsList[n];
                    string s = "";

                    for (int c = 0; c < a.Length - 1; c++)
                    {
                        char b = a[c];
                        s += b;
                    }

                    //修正した文字列を入れる
                    inputJson.contentsList[n] = s;

                    //中身を入れていく
                    nameList.Add(inputJson.nameList[n]);
                    contentsList.Add(inputJson.contentsList[n]);

                    //テーブルを追加していく
                    myTable.Add(inputJson.nameList[n], inputJson.contentsList[n]);

                    Type type = new Type();

                    //中身を入れていく
                    type.typeContentsList = inputJson.contentsList[n];

                    //中身を見て型処理
                    type.type = TypeJudgment(type.typeContentsList);

                    // Debug.Log(type.typeContentsList+"の中身(型)：" +TypeJudgment(type.typeContentsList));
                    
                    //テーブルを追加していく
                    myTableType.Add(inputJson.nameList[n], type);
                }
            }
        }

        //中身を入れていく
        for (int i = 0; i < nameList.Count(); i++)
        {
            inputJson.contentsList[i] = contentsList[i];
            inputJson.nameList[i] = nameList[i];
        }

        //要素数を代入
        myTableCnt = myTable.Count;
    }

    //入力した名前と値から型を判断する関数
    eType TypeJudgment(string _type)
    {
        eType ans = eType.String;

        var Int = 0;
        var Double = 0;
        var String = 0;
        var Bool = 0;

        //文字を見つける処理
        var character = "!#$%&'()=~|`{+*}<>?-^@[;:],/QWERTYUIOPASDFGHJKLZXCVBNM_qwertyuiopasdfghjklzxcvbnm";

        //characterの長さを取得してその分for文で回す
        for (int i = 0; i < character.Length; i++)
        {
            //Containsは指定したのを探索、結果をtrue,falseで返してくれる
            //ToStringは変数をstringに変換してくれる
            if (_type.Contains(character[i].ToString()) == true)
            {
                //string型になりうるかもポイントの追加
                String++;
            }
        }

        //数字を見つける処理
        var number = "0123456789";

        //numberの長さを取得してその分for文で回す
        for (int i = 0; i < number.Length; i++)
        {
            if (_type.Contains(number[i].ToString()) == true)
            {
                //int型になりうるかもポイントの追加
                Int++;
            }
        }

        //.を見つける処理
        char dot = '.';

        if (_type.Contains(dot) == true && _type[0] != dot && _type[_type.Length - 1] != dot)
        {
            //Double型になりうるかもポイントの追加
            Double++;
        }

        if (_type.Contains(dot) == true && _type[0] == dot || _type[_type.Length - 1] == dot)
        {
            //string型になりうるかもポイントの追加
            String++;
        }

        if (_type == "true" || _type == "false")
        {
            //double型になりうるかもポイントの追加
            Bool++;
        }

        //文字列の場合
        if (String != 0)
        {
            ans = eType.String;
        }

        //整数の場合
        if (String == 0 && Int != 0 && Double == 0)
        {
            ans = eType.Int;
        }

        //少数ありの場合
        if (String == 0 && Int != 0 && Double != 0)
        {
            ans = eType.Double;
        }

        //boolの場合
        if (Bool != 0)
        {
            ans = eType.Bool;
        }

        return ans;
    }

    //jsonファイルがない場合jsonファイルを作成しようとする関数
    void CreateDefaultJson()
    {
        Debug.Log("指定場所にファイルが存在しないから作成するよ");
        Debug.Log("場所：Assets/Resources/input.json");

        inputJson = new InputJson();

        List<string> defaultNameList = new List<string>();
        List<string> defaultContentsList = new List<string>();

        //デフォルトと想定した項目群を入れます

        //名前(Key)とする奴から
        defaultNameList.Add("Volum");
        defaultNameList.Add("BGM_Volum");
        defaultNameList.Add("SE_Volum");
        defaultNameList.Add("DefaltFixeduTalkEndTime");
        defaultNameList.Add("MaxTalkTime");
        defaultNameList.Add("FacingTimeout");
        defaultNameList.Add("AnnouncingTimeout");
        defaultNameList.Add("WebCamID");
        defaultNameList.Add("BufferCount");
        defaultNameList.Add("BufferLength");
        defaultNameList.Add("AnzuServerURL");
        defaultNameList.Add("MakinaServerURL");
        defaultNameList.Add("Demo001");
        defaultNameList.Add("Demo002");
        defaultNameList.Add("Demo003");
        defaultNameList.Add("Demo004");
        defaultNameList.Add("Demo005");
        defaultNameList.Add("NoisePath");
        defaultNameList.Add("TSJapaneseTalker_VoiceFile");
        defaultNameList.Add("TSJapaneseTalker_DicPath");
        defaultNameList.Add("TSJapaneseTalker_UserDicFile");
        defaultNameList.Add("IsBannerManager");
        defaultNameList.Add("IsWeatherManager");
        defaultNameList.Add("IsInfraRedDerection");
        defaultNameList.Add("IsFaceRecognizer");
        defaultNameList.Add("IsLanguageButton");

        //名前(Key)とする奴から
        defaultContentsList.Add("100");
        defaultContentsList.Add("10");
        defaultContentsList.Add("50");
        defaultContentsList.Add("10.5");
        defaultContentsList.Add("5.5");
        defaultContentsList.Add("5");
        defaultContentsList.Add("3");
        defaultContentsList.Add("test0");
        defaultContentsList.Add("test1");
        defaultContentsList.Add("test2");
        defaultContentsList.Add("test3");
        defaultContentsList.Add("test4");
        defaultContentsList.Add("test5");
        defaultContentsList.Add("test6");
        defaultContentsList.Add("test7");
        defaultContentsList.Add("test8");
        defaultContentsList.Add("test9");
        defaultContentsList.Add("test10");
        defaultContentsList.Add("test.txt");
        defaultContentsList.Add("test/test.txt");
        defaultContentsList.Add("test/test/test.txt");
        defaultContentsList.Add("true");
        defaultContentsList.Add("false");
        defaultContentsList.Add("true");
        defaultContentsList.Add("false");
        defaultContentsList.Add("true");

        //配列をリサイズ
        Array.Resize(ref inputJson.nameList, defaultNameList.Count);
        Array.Resize(ref inputJson.contentsList, defaultContentsList.Count);

        //中身を入れていく
        for (int i=0; i < defaultNameList.Count;i++)
        {
            inputJson.nameList[i] = defaultNameList[i];
            inputJson.contentsList[i] = defaultContentsList[i];

            myTable.Add(inputJson.nameList[i], inputJson.contentsList[i]);
        }

        //書き込み保存
        Write();
    }
}
//動くけどやり方がヤバい