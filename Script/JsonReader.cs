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


    // 保存するファイル名
    const string SAVE_FILE_PATH = "input.json";

    //jsonをstringで読み込む用
    string inputString;

    //jsonを入れる用のクラス
    public InputJson inputJson;
    List<string> nameList = new List<string>();
    List<string> contentsList = new List<string>();
    public string[] textMessage; //テキストの加工前の一行を入れる変数
    public string[,] textWords; //テキストの複数列を入れる2次元は配列 

    private int rowLength; //テキスト内の行数を取得する変数
    private int columnLength; //テキスト内の列数を取得する変数

    private System.Diagnostics.Process P = null;
    int cnt =0;
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
            Write(inputJson);
        }

        // Lキーでリロード実行
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("ロード実行");
            for (int i=0; i<2;i++)
            {
                var path = Application.dataPath + "/Resources/" + SAVE_FILE_PATH;

                P = System.Diagnostics.Process.Start(path);

                Thread.Sleep(65);

                P.Kill();

                //今の中身を削除
                MyTable.Clear();
                MyTableType.Clear();
                List<string> _nameList = new List<string>();
                List<string> _contentsList = new List<string>();
                nameList = _nameList;
                contentsList = _contentsList;

                //リロード
                Load();

            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("aaa");
            inputJson.nameList[0] = "aaaaa";
            inputJson.contentsList[0] = "11111";
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {  
            foreach (string Key in myTableType.Keys)
            {
                Debug.Log(Key);

            }

            foreach (Type _type in myTableType.Values)
            {
               Debug.Log(_type.type + ":" + _type.typeContentsList);
            }
        }
    }
    //ファイルに書き込む
    void Write(InputJson data)
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
            else if(i != myTable.Count - 1 && flg == false)
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

        var lineCount = -2;
        var path = Application.dataPath + "/Resources/" + SAVE_FILE_PATH;
        var reader = File.OpenText(path);

        while (reader.ReadLine() != null)
        {
            lineCount++;
        }

        //これで配列サイズの変更ができる
        Array.Resize(ref inputJson.nameList, lineCount);
        Array.Resize(ref inputJson.contentsList, lineCount);

        TextAsset textasset = Resources.Load("input", typeof(TextAsset)) as TextAsset; //Resourcesフォルダから対象テキストを取得
        string TextLines = textasset.text; //テキスト全体をstring型で入れる変数を用意して入れる

        //Splitで一行づつを代入した1次配列を作成
        textMessage = TextLines.Split('\n'); //

        //行数と列数を取得
        columnLength = textMessage[0].Split('\t').Length;
        rowLength = textMessage.Length;

        //2次配列を定義
        textWords = new string[rowLength, columnLength];

        for (int i = 0; i < rowLength - 1; i++)
        {
            string[] tempWords = textMessage[i].Split('\t'); //textMessageをカンマごとに分けたものを一時的にtempWordsに代入

            for (int n = 0; n < columnLength; n++)
            {
                if (tempWords[n].Contains("{") != true && tempWords[n].Contains("}") != true)
                {
                    textWords[i, n] = tempWords[n]; //2次配列textWordsにカンマごとに分けたtempWordsを代入していく

                    string z = textWords[i, n];

                    //削除する文字の配列
                    char[] removeChars = new char[] { '"', ',', ' ' };

                    //削除する文字を1文字ずつ削除する
                    foreach (char c in removeChars)
                    {
                        z = z.Replace(c.ToString(), "");
                    }
                    string[] d = z.Split(':');
                    Array.Resize(ref inputJson.nameList, lineCount);
                    Array.Resize(ref inputJson.contentsList, lineCount);

                    for (int c = 0; c < inputJson.nameList.Length; c++)
                    {
                        inputJson.nameList[c] = d[0];
                        inputJson.contentsList[c] = d[1];
                        break;
                    }

                    string a = inputJson.contentsList[n];
                    string s = "";

                    for (int c = 0; c < a.Length - 1; c++)
                    {
                        char b = a[c];
                        s += b;
                    }

                    inputJson.contentsList[n] = s;

                    nameList.Add(inputJson.nameList[n]);
                    contentsList.Add(inputJson.contentsList[n]);
                    myTable.Add(inputJson.nameList[n], inputJson.contentsList[n]);

                    Type type = new Type();

                    type.typeContentsList = inputJson.contentsList[n];

                    //中身を見て型処理
                    type.type = TypeJudgment(type.typeContentsList);
                   // Debug.Log(type.typeContentsList+"の中身(型)：" +TypeJudgment(type.typeContentsList));
                    myTableType.Add(inputJson.nameList[n], type);
                }
            }
        }

        for (int i = 0; i < nameList.Count(); i++)
        {
            inputJson.contentsList[i] = contentsList[i];
            inputJson.nameList[i] = nameList[i];
        }

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
                Int++;
            }
        }

        //.を見つける処理
        char dot = '.';

        if (_type.Contains(dot) == true && _type[0] != dot && _type[_type.Length - 1] != dot)
        {
            Double++;
        }

        if (_type.Contains(dot) == true && _type[0] == dot || _type[_type.Length - 1] == dot)
        {
            String++;
        }

        if (_type == "true" || _type == "false")
        {
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
}
//動くけどやり方がヤバい