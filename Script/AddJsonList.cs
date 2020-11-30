using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//現在使用しません
public class AddJsonList : MonoBehaviour
{
    //残骸だけと何かしらに使いそうだから残しておく
    //型判別する処理
    /*int IdentifyType<T>(T x)
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
    }*/

    public InputField textName;        //テキストの名前が入る変数
    public InputField textContents;    //テキストの内容が入る変数

    public Text textNameBox;            //テキストの名前を入力してる場所が入る変数
    public Text textContentsBox;        //テキストの内容を入力してる場所が入る変数

    string readTextName;         //string型で一旦名前を格納

    string readTextContents;     //string型で一旦内容を格納

    //動的に変数を動かすためにListを使用

    //内容入れる方
    List<int> intList = new List<int>();
    List<double> doubleList = new List<double>();
    List<string> stringList = new List<string>();
    List<bool> boolList = new List<bool>();

    //名前入れる方
    List<string> nameList = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        GameObject nameInput = GameObject.Find("NameInput");         //オブジェクトの名前から取得して変数に格納する
        textName = nameInput.GetComponent<InputField>();     //オブジェクトの中にあるjsonReaderScriptを取得して変数に格納する

        GameObject contentsInput = GameObject.Find("ContentsInput");         //オブジェクトの名前から取得して変数に格納する
        textContents = contentsInput.GetComponent<InputField>();     //オブジェクトの中にあるjsonReaderScriptを取得して変数に格納する

        GameObject textNameInput = GameObject.Find("TextNameInput");         //オブジェクトの名前から取得して変数に格納する
        textNameBox = textNameInput.GetComponent<Text>();     //オブジェクトの中にあるjsonReaderScriptを取得して変数に格納する

        GameObject textContentsInput = GameObject.Find("TextContentsInput");         //オブジェクトの名前から取得して変数に格納する
        textContentsBox = textContentsInput.GetComponent<Text>();     //オブジェクトの中にあるjsonReaderScriptを取得して変数に格納する

    }

    // Update is called once per frame
    void Update()
    {
        //Enterを押されたら
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //入力されたものを仮の箱に代入
            readTextName = textName.text;
            readTextContents = textContents.text;


            Debug.Log("名前"+readTextName);
            Debug.Log("内容"+readTextContents);


            //入力されたものの中身を調べどの型に当てはめるかを判断する
            Debug.Log("作成される型：" + TypeJudgment(readTextName, readTextContents));

            string stringAdd = readTextName;

            switch (TypeJudgment(readTextName, readTextContents))
            {
                //(型).Parse(変数)で型変換できる
                case 0:    //int型
                    int intAdd = int.Parse(readTextContents);
                    //中身を登録
                    intList.Add(intAdd);
                    nameList.Add(stringAdd);
                    break;

                case 1:    //double型
                    double doubleAdd = double.Parse(readTextContents);
                    //中身を登録
                    doubleList.Add(doubleAdd);
                    nameList.Add(stringAdd);
                    break;

                case 2:    //string型
                    //中身を登録
                    stringList.Add(readTextContents);
                    nameList.Add(stringAdd);
                    break;

                case 3:    //bool型
                    bool boolAdd = bool.Parse(readTextContents);
                    //中身を登録
                    boolList.Add(boolAdd);
                    nameList.Add(stringAdd);
                    break;

                default:
                    break;
            }
        }

        //Qを押されたら
        if (Input.GetKeyDown(KeyCode.Q))
        {
            /*Debug.Log("int   :" + intList.Count);
            Debug.Log("double:" + doubleList.Count);
            Debug.Log("string:" + stringList.Count);
            Debug.Log("bool  :" + boolList.Count);*/

            for (int i=0;i<nameList.Count;i++)
            {
                Debug.Log("name  :" + nameList[i]);
            }
        }
    }


    //入力した名前と値から型を判断する関数
    int TypeJudgment(string name,string contents) 
    {
        var ans     = -1;
        var NGCnt   =  0;
        var nameNumCnt  = 0;
        var nameCharCnt = 0;
        var contNumCnt  = 0;
        var contCharCnt = 0;
        var contDotCnt  = 0;

        //登録ができない名前
        var NG = "!#$%&'()=~|`{+*}<>?-^@[;:],/";

        //NGの長さを取得してその分for文で回す
        for (int i = 0; i < NG.Length; i++)
        {
            //Containsは指定したのを探索、結果をtrue,falseで返してくれる
            //ToStringは変数をstringに変換してくれる
            if (name.Contains(NG[i].ToString()) == true)
            {
                //Debug.Log(NG[i] + ":見つけた");
                NGCnt++;
            }
        }

        //文字を見つける処理
        var character = "QWERTYUIOPASDFGHJKLZXCVBNM_qwertyuiopasdfghjklzxcvbnm";

        //characterの長さを取得してその分for文で回す
        for (int i=0;i<character.Length;i++)
        {
            //Containsは指定したのを探索、結果をtrue,falseで返してくれる
            //ToStringは変数をstringに変換してくれる
            if (name.Contains(character[i].ToString()) == true)
            {
                //Debug.Log(name+"で"+character[i] + "を見つけた");
                nameCharCnt++;
            }

            if (contents.Contains(character[i].ToString()) == true)
            {
                //Debug.Log(contents+"で"+character[i] + "を見つけた");
                contCharCnt++;
            }
        }

        //数字を見つける処理
        var number = "0123456789";

        //numberの長さを取得してその分for文で回す
        for (int i=0;i< number.Length; i++)
        {
            if (name[0] == number[i])
            {
                //Debug.Log(name + "で" + number[i] + "を最初に見つけたので作成拒否");
                NGCnt++;
            }

            //Containsは指定したのを探索、結果をtrue,falseで返してくれる
            //ToStringは変数をstringに変換してくれる
            if (name.Contains(number[i].ToString()) == true)
            {
                //Debug.Log(name + "で" + number[i] + "を見つけた");
                nameNumCnt++;
            }

            if (contents.Contains(number[i].ToString()) == true)
            {
               //Debug.Log(contents + "で" + number[i] + "を見つけた");
                contNumCnt++;
            }
        }

        //.を見つける処理
        var dot = ".";

        //nameの長さを取得してその分for文で回す
        for (int i = 1; i < name.Length-1; i++)
        {
            //Containsは指定したのを探索、結果をtrue,falseで返してくれる
            //ToStringは変数をstringに変換してくれる
            if (name.Contains(dot) == true)
            {
                //Debug.Log(name + "で" + name[i] + "を見つけた");
                NGCnt++;
            }
        }

        //contentsの長さを取得してその分for文で回す
        for (int i = 0; i < dot.Length; i++) 
        {
            //Containsは指定したのを探索、結果をtrue,falseで返してくれる
            //ToStringは変数をstringに変換してくれる
            if(contents.Contains(dot[i].ToString()) == true)
            {
                //Debug.Log(contents + "で" + contents[i] + "を見つけた");
                contDotCnt++;
            }
        }

        //家に帰るんだなおまえにも家族がいるだろう
        if (NGCnt!=0)
        {
            return ans;
        }

        //名前が文字列のみ場合
        if (nameCharCnt != 0 && nameNumCnt == 0) 
        {
            //数字のみなら
            if (contNumCnt != 0 && contCharCnt == 0 && contDotCnt == 0)  
            {
                //int型で登録
                ans = 0;
            }

            //数字と最初と最後以外に一つ.があるなら
            if (contNumCnt != 0 && contCharCnt == 0 && contDotCnt == 1 &&
                contents[0] != dot[0] && contents[contents.Length - 1] != dot[0]) 
            {
                //double型で登録
                ans = 1;
            }

            //文字があるなら
            if (contCharCnt != 0 || contDotCnt > 1)
            {
                //string型で登録
                ans = 2;
            }

            //内容がtrue、false以外ならstring型に
            if (contents == "true" || contents == "false")
            {
                //bool型で登録
                ans = 3;
            }
        }

        //名前が数字のみ場合
        if (nameCharCnt == 0 && nameNumCnt != 0) 
        {
            //作成不可能
        }

        //.以外のを見つけた場合
        //名前の最初に数字がないなら作成可能
        if (nameCharCnt != 0 && nameNumCnt != 0)
        {
            //numberの長さを取得してその分for文で回す
            for (int i = 0; i < number.Length; i++)
            {
                //配列の最初を取得して比べる
                if (name.First()==number[i])
                {
                    //作成不可能
                }
                else
                {
                    //数字のみなら
                    if (contNumCnt != 0 && contCharCnt == 0 && contDotCnt == 0)
                    {
                        //int型で登録
                        ans = 0;
                    }

                    //数字と最初と最後以外に一つ.があるなら
                    if (contNumCnt != 0 && contCharCnt == 0 && contDotCnt == 1 &&
                        contents[0] != dot[0] && contents[contents.Length - 1] != dot[0])
                    {
                        //double型で登録
                        ans = 1;
                    }

                    //文字があるなら
                    if (contCharCnt != 0 || contDotCnt > 1)
                    {
                        //string型で登録
                        ans = 2;
                    }

                    //内容がtrue、false以外ならstring型に
                    if (contents == "true" || contents == "false")
                    {
                        //bool型で登録
                        ans = 3;
                    }
                }
            }
        }

        //どれも見つからない場合
        if (nameCharCnt == 0 && nameNumCnt == 0 )
        {
            //作成不可能
        }

        /*
        Debug.Log("nameNumCnt:"+nameNumCnt);
        Debug.Log("nameCharCnt:" + nameCharCnt);
        Debug.Log("contNumCnt:" + contNumCnt);
        Debug.Log("contCharCnt:" + contCharCnt);
        Debug.Log("contDotCnt:" + contDotCnt);
        */

        return ans;

        /*
         ansの返り値 

        -1:登録不可
         0:   int型として登録してね
         1:double型として登録してね
         2:string型として登録してね
         3:  bool型として登録してね
         */
    }
}
