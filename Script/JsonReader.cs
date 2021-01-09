using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;
//2020/1/8
//サーバーから情報をもらって連想配列を作成します

//どの型にするか判別するのに使うenum形
public enum eType
{
    Int,
    Double,
    String,
    Bool
}

//サーバーから読み取ったものを入れ込む用のクラス
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

    //テキストの加工前の一行を入れる変数
    private string[] textMessage;

    //情報をもらうためのリクエスト文章
    private string request = "/Setting";

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "SettingsChangeScene")
        {
            if (GameObject.Find("LoadObject").GetComponent<ServerConnect>().SendServer(request) == 0)
            {
                Debug.Log("送信成功");

                //Load関数を書き直してサーバーからもらった情報を格納する形にする
                Load();

                //Debug.Log("受信："+ServerConnect.SendMsg);
            }
            else
            {
                Debug.Log("送信失敗");
            }
        }
    }

    void Update()
    {
        //UIの値にする(contentsListを)
        ChangeSetting();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("中身の表示をします");

            foreach (string Key in myTableType.Keys)
            {
                Debug.Log("キー名：" + Key);
            }

            foreach (Type _type in myTableType.Values)
            {
                Debug.Log("データタイプ:"+_type.type + "　中身:" + _type.typeContentsList);
            }

            Debug.Log("MyTableCnt:" + MyTableCnt);
        }
    }

    void Load()
    {
        inputJson = new InputJson();

        bool charFlg = false;
        int cnt = 0;
        List<string> strList = new List<string>();

        string test ="";

        //テキスト全体をstring型で入れる変数を用意して入れる
        string TextLines = ServerConnect.SendMsg;

        //Debug.Log(ServerConnect.SendMsg);

        //Debug.Log(TextLines);

        for (int i = 0; i < TextLines.Length; i++)
        {
            char a = '\"';
            char b = ',';
            char c = ':';
            char d = '{';
            char e = '}';

            if (TextLines[i] == a)
            {
                if (charFlg == false) 
                {
                    test = "";
                    charFlg = true;
                }
                else
                if (charFlg == true)
                {
                    if (i < TextLines.Length - 2)
                    {
                        if (TextLines[i] == '\"' && TextLines[i + 1] == ':' && TextLines[i + 2] == ' ' ||
                            TextLines[i] == '\"' && TextLines[i + 1] == ',' ||
                            TextLines[i] == '\"' && TextLines[i + 1] == '\n') 
                        {
                            strList.Add(test);
                            //Debug.Log("trueからfalseに入った文:" + test);
                            test = "";
                            charFlg = false;
                        }
                    }
                }
               // Debug.Log(i + "で見つけた"+ charFlg+"になりました");
            }

            if (charFlg == false && TextLines[i] == a && cnt==0)
            {
                test += "";
                //Debug.Log("a"+ TextLines[i-2]+ TextLines[i-1]+ TextLines[i]);
            }
            if (charFlg == false && TextLines[i] == b)
            {
                if(test!="\"")
                {
                    strList.Add(test);
                    //Debug.Log(i + "で,を見つけた。入った文:" + test);
                    cnt = 0;
                }
                test = "";
            }
            else
            if (charFlg == false && TextLines[i] == c)
            {
               // Debug.Log(i + "で見つけた");
                test += "";
            }
            else
            if (charFlg == false && TextLines[i] == d)
            {
               // Debug.Log(i + "で見つけた");
                test += "";
            }
            else
            if (charFlg == false && TextLines[i] == e)
            {
                //Debug.Log(i + "で見つけた");
                test += "";
            }
            else
            {
                test += TextLines[i];
               // Debug.Log(i + "入ったやつ：" + test);
            }

            if (charFlg == true && TextLines[i] == b)
            {
               // Debug.Log(i + "で見つけたけどだめなやつ"+TextLines[i-1]+ TextLines[i]+ TextLines[i+1]);
            }
            else
            if (charFlg == true && TextLines[i] == c)
            {
                //Debug.Log(i + "で見つけたけどだめなやつ" + TextLines[i - 1] + TextLines[i] + TextLines[i + 1]);
            }
            else
            if (charFlg == true && TextLines[i] == d)
            {
              //  Debug.Log(i + "で見つけたけどだめなやつ" + TextLines[i - 1] + TextLines[i] + TextLines[i + 1]);
            }
            else
            if (charFlg == true && TextLines[i] == e)
            {
              //  Debug.Log(i + "で見つけたけどだめなやつ" + TextLines[i - 1] + TextLines[i] + TextLines[i + 1]);
            }
        }



        for (int i=0; i< strList.Count();i++)
        {
            //削除する文字の配列
            char[] removeChars = new char[] { ' ', '\n' };

            //削除する文字を1文字ずつ削除する
            foreach (char c in removeChars)
            {
                strList[i] = strList[i].Replace(c.ToString(), "");
            }
            string str1 = "";
            string str2 = "";
            str1 = strList[i];

            for (int j = 1; j < str1.Length; j ++)
            {
                str2 += str1[j];

                strList[i] = str2;
            }

            //Debug.Log("strList"+i+":" + strList[i]);
        }

        //配列リサイズ
        Array.Resize(ref inputJson.nameList, strList.Count/ 2);
        Array.Resize(ref inputJson.contentsList, strList.Count / 2);

        for (int i = 0; i < strList.Count-1; i += 2)
        {
            int j = i / 2;

            inputJson.nameList[j] = strList[i];
            inputJson.contentsList[j] = strList[i + 1];

            //中身を入れていく
            nameList.Add(inputJson.nameList[j]);
            contentsList.Add(inputJson.contentsList[j]);

            //テーブルを追加していく
            myTable.Add(inputJson.nameList[j], inputJson.contentsList[j]);

            Type type = new Type();

            //中身を入れていく
            type.typeContentsList = inputJson.contentsList[j];

            //中身を見て型処理
            type.type = TypeJudgment(type.typeContentsList);

           // Debug.Log(type.typeContentsList + "の中身(型)：" + TypeJudgment(type.typeContentsList));

            //テーブルを追加していく
            myTableType.Add(inputJson.nameList[j], type);
        }

        //要素数を代入
        myTableCnt = myTable.Count;

        //使わなくなった処理です
        {
            /*
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
        */
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

    //UIの値に書き換える処理
    void ChangeSetting()
    {
        for (int i = 0; i < myTableCnt; i++)
        {
            GameObject configObject = GameObject.Find(inputJson.nameList[i]);
            switch (myTableType[inputJson.nameList[i]].type)
            {
                case eType.Int:
                    scr_Slider_int sliderInt = configObject.GetComponent<scr_Slider_int>();
                    if(sliderInt.TextValue != null)
                    {
                        inputJson.contentsList[i] = sliderInt.TextValue;
                    }
                    //Debug.Log(inputJson.contentsList[i]);
                    break;
                case eType.Double:
                    scr_Slider slider = configObject.GetComponent<scr_Slider>();
                    if (slider.TextValue != null)
                    {
                        inputJson.contentsList[i] = slider.TextValue;
                    }
                    //Debug.Log(inputJson.contentsList[i]);
                    break;
                case eType.String:
                    scr_Input input = configObject.GetComponent<scr_Input>();
                    if (input.TextValue != null)
                    {
                        inputJson.contentsList[i] = input.TextValue;
                    }
                    //Debug.Log(inputJson.contentsList[i]);
                    break;
                case eType.Bool:
                    scr_DropDown dropDown = configObject.GetComponent<scr_DropDown>();
                    if (dropDown.TextValue != null)
                    {
                        inputJson.contentsList[i] = dropDown.TextValue;
                    }
                    //Debug.Log(inputJson.contentsList[i]);
                    break;
                default:
                    break;
            }
        }        
    }
}