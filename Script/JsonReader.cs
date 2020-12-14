using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Threading;

//2020/12/11
//ファイルの中身を読み取って連想配列を作成します

//どの型にするか判別するのに使うenum形
public enum eType
{
    Int,
    Double,
    String,
    Bool
}

//テキストから読み取ったものを入れ込む用のクラス
public class InputJson
{
     //配列にも対応
      public string[] nameList = new string[] { };
      public string[] contentsList = new string[] { };
}

//Values + どの型なのかを判別したやつを入れるクラス
public class Type
{
    public string typeContentsList;
    public eType type;
}

public class JsonReader : MonoBehaviour
{
    Dictionary<string, string> myTable = new Dictionary<string, string>();

    //読み込んだデータをKeyとValuesに入れ込んで登録
    public Dictionary<string, string> MyTable
    {
        get { return myTable; }
    }

    Dictionary<string, Type> myTableType = new Dictionary<string, Type>();

    //読み込んだデータをKeyとValues + どの型なのかを判別したやつ で登録
    public Dictionary<string, Type> MyTableType
    {
        get { return myTableType; }
    }

    int myTableCnt;

    //myTableTypeの要素数を返す
    public int MyTableCnt
    {
        get { return myTableCnt; }
    }

    //jsonを入れる用のクラス
    public InputJson inputJson;
    List<string> nameList = new List<string>();
    List<string> contentsList = new List<string>();

    //テキストから読み取ったやつ(名前)を配列にまとめたやつを取得出来ます
    public List<string> NameList
    {
        get { return nameList; }
    }

    //テキストから読み取ったやつ(中身)を配列にまとめたやつを取得出来ます
    public List<string> ContentsList
    {
        get { return contentsList; }
    }

    //保存するファイル名
    private const string SAVE_FILE_PATH = "input.json";

    //入力ファイルはAssets/Resources/input.json
    private string path;

    //テキストの加工前の一行を入れる変数
    private string[] textMessage;

    //テキストの複数列を入れる2次元は配列 
    private string[,] textWords;

    //テキスト内の行数を取得する変数
    private int rowLength;

    //テキスト内の列数を取得する変数
    private int columnLength;

    //テキストファイル開いたりするのに使う変数
    private System.Diagnostics.Process P = null;

    private void Awake()
    {
        //入力ファイルはAssets/Resources/input.json
        path = Application.dataPath + "/Resources/" + SAVE_FILE_PATH;

        //指定先のファイルが存在しない場合
        if (!File.Exists(path))
        {
            //指定先にファイルを作成
            CreateDefaultJson();

            //開く
            P = System.Diagnostics.Process.Start(path);

            //待つ
            Thread.Sleep(100);

            //閉じる
            P.Kill();
        }
        else
        {
            //存在するなら読み込む
            Load();
        }
    }

    void Update()
    {
        //UIの値にする(contentsListを)
        ChangeSetting();

        // Sキーで変更とセーブの実行
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("セーブ実行");

            //書き込み
            Write();
        }

        // Lキーでロード実行
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("ロード実行");

            for (int i = 0; i < 2; i++)
            {
                //開く
                P = System.Diagnostics.Process.Start(path);

                //待つ
                Thread.Sleep(100);

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

                //ロード
                Load();
            }
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
    public void Write()
    {
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

        //指定先のファイルが存在するなら
        if (!File.Exists(path))
        {
            //指定先にファイルを作成
            CreateDefaultJson();
        }

        inputJson = new InputJson();

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
    public void CreateDefaultJson()
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

        //中身(Values)とする奴から
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

        Debug.Log("生成完了：Lキーを押してロードしてください...");
    }

    //UIの値に書き換える処理
    void ChangeSetting()
    {
        //UIで表示してるオブジェクトを探す(6種類あるからそれぞれを探す)
        for (int i = 0; i < myTableCnt; i++)
        {
            //scr_Slider_intがあるオブジェクトを探す
            scr_Slider_int sliderInt = GameObject.Find(inputJson.nameList[i]).GetComponent<scr_Slider_int>();

            //見つけたらその番号のcontentsList[i]の値を書き換える
            if (sliderInt !=null)
            {
                //Debug.Log(i+":"+ sliderInt.TextValue);
                inputJson.contentsList[i] = sliderInt.TextValue;
            }

            //scr_Sliderがあるオブジェクトを探す
            scr_Slider slider = GameObject.Find(inputJson.nameList[i]).GetComponent<scr_Slider>();

            //見つけたらその番号のcontentsList[i]の値を書き換える
            if (slider != null)
            {
                //Debug.Log(i + ":" + slider.TextValue);
                inputJson.contentsList[i] = slider.TextValue;
            }

            //scr_DropDownがあるオブジェクトを探す
            scr_DropDown dropDown = GameObject.Find(inputJson.nameList[i]).GetComponent<scr_DropDown>();

            //見つけたらその番号のcontentsList[i]の値を書き換える
            if (dropDown != null)
            {
                //Debug.Log(i + ":" + dropDown.TextValue);
                inputJson.contentsList[i] = dropDown.TextValue;
            }

            //scr_Input_intがあるオブジェクトを探す
            scr_Input_int inputInt = GameObject.Find(inputJson.nameList[i]).GetComponent<scr_Input_int>();

            //見つけたらその番号のcontentsList[i]の値を書き換える
            if (inputInt != null)
            {
                //Debug.Log(i + ":" + inputInt.TextValue);
                inputJson.contentsList[i] = inputInt.TextValue;
            }

            //scr_Input_Sがあるオブジェクトを探す
            scr_Input_S inputS = GameObject.Find(inputJson.nameList[i]).GetComponent<scr_Input_S>();

            //見つけたらその番号のcontentsList[i]の値を書き換える
            if (inputS != null)
            {
                //Debug.Log(i + ":" + inputS.TextValue);

                //なんかfalseが空白で返されるのでif文で空白はfalseとして扱うようにしてます
                if (inputS.TextValue == "true")
                {
                    inputJson.contentsList[i] = inputS.TextValue;
                }
                else
                {
                    inputJson.contentsList[i] = "false";
                }
            }

            //scr_Inputがあるオブジェクトを探す
            scr_Input input = GameObject.Find(inputJson.nameList[i]).GetComponent<scr_Input>();

            //見つけたらその番号のcontentsList[i]の値を書き換える
            if (input != null)
            {
                //Debug.Log(i + ":" + input.TextValue);
                inputJson.contentsList[i] = input.TextValue;
            }
        }        
    }
}
//動くけどやり方がヤバい