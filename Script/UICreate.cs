using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreate : MonoBehaviour
{
    #region 変数宣言
    /*プレハブ格納用*/
    private GameObject srider;
    private GameObject srider_int;
    private GameObject dropDown_L;
    private GameObject dropDown_S;
    private GameObject input_L;
    private GameObject input_S;
    private GameObject input_int;
    private GameObject text;
    private GameObject scrollView;

    /*生成したインスタンスを格納する配列*/
    private GameObject[] instances;     //インスタンス格納
    private Slider[] sliders;           //スライダー
    private GameObject[] sliderinput;   //スライダーの場合inputもあるので...
    private GameObject[] itemName;      //項目名のオブジェクト
    private Text[] names;               //項目名のテキスト
    private Dropdown[] dropDown;        //ドロップダウン格納
    private InputField[] inputs;        //インプットフィールド格納

    private GameObject scroll;          //スクロールオブジェクト
    JsonReader jsonReader;              //JsonReader

    /*リストにある項目の配列*/
    private string[] NameArray;                 //項目の名前
    private Type[] TypeArray;                   //項目のタイプ
    private string[] ContentsArray;             //項目の値

    /*座標*/
    Vector2 view = new Vector2(0.0f, 300.0f);                   //スクロールビューの座標
    Vector2 textPos = new Vector2(0.0f,-70.0f);              //項目名
    Vector2 basisPos = new Vector2(0.0f, -140.0f);              //ベース
    Vector2 sliderPos =  new Vector2(-110.0f, -140.0f);         //スライダー
    Vector2 slidertextPos =  new Vector2(380.0f,-140.0f);       //スライダーのinput
    Vector2 dropS_Pos =  new Vector2(260.0f, -140.0f);          //ドロップダウン(short)
    Vector2 space = new Vector2(0.0f, 230.0f);                  //間隔

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        /*プレハブ読み込み*/
        srider = (GameObject)Resources.Load("obj_Slider");          //スクロール
        srider_int = (GameObject)Resources.Load("obj_Slider_int");          //スクロール
        dropDown_L = (GameObject)Resources.Load("obj_Dropdown_L");  //ドロップダウン(長い)
        dropDown_S = (GameObject)Resources.Load("obj_Dropdown_S");  //ドロップダウン(短い)
        input_L = (GameObject)Resources.Load("obj_input_L");        //テキストボックス(長い)
        input_S = (GameObject)Resources.Load("obj_input_S");        //テキストボックス(スライダー)
        input_int = (GameObject)Resources.Load("obj_input_int");    //intの入力
        text = (GameObject)Resources.Load("obj_Text");              //テキスト
        scrollView = (GameObject)Resources.Load("obj_ScrollView");  //スクロールビュー

        CreateInit();                                               //生成

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateInit()
    {
        #region 初期化処理
        //初期化
        JsonReader jsonReader = GameObject.Find("LoadObject").GetComponent<JsonReader>();

        instances = new GameObject[jsonReader.MyTableCnt];                   //配列初期化
        sliders = new Slider[jsonReader.MyTableCnt];
        sliderinput = new GameObject[jsonReader.MyTableCnt];
        itemName = new GameObject[jsonReader.MyTableCnt];
        names = new Text[jsonReader.MyTableCnt];
        dropDown = new Dropdown[jsonReader.MyTableCnt];
        inputs = new InputField[jsonReader.MyTableCnt];
        NameArray = new string[jsonReader.MyTableCnt];
        TypeArray = new Type[jsonReader.MyTableCnt];
        ContentsArray = new string[jsonReader.MyTableCnt];

        scroll = null;

        jsonReader.MyTableType.Keys.CopyTo(NameArray, 0);    //名前
        jsonReader.MyTableType.Values.CopyTo(TypeArray, 0);  //タイプ
        ContentsArray = jsonReader.ContentsList.ToArray();   //項目内容

        scroll = (GameObject)Instantiate(scrollView, view, Quaternion.identity);    //スクロールビュー
        scroll.transform.SetParent(gameObject.transform, false);                        //親設定
        scroll.name = "ScrollView";                                                     //名前設定


        var viewPort = scroll.transform.Find("Viewport");
        Transform Content = viewPort.transform.Find("Content");
        var delta = Content.GetComponent<RectTransform>();
        delta.sizeDelta = Vector2.up + new Vector2(0.0f,260.0f) * jsonReader.MyTableCnt;//項目数に応じた大きさ
        var half = (new Vector2(0.0f, 260.0f) * jsonReader.MyTableCnt) /2;              //UIのy = 0の位置がこの値
        textPos += half;
        basisPos += half;
        sliderPos += half;
        slidertextPos += half;
        dropS_Pos += half;

        #endregion

        for (int i = 0; i < jsonReader.MyTableCnt; i++)//生成する数だけ繰り返す
        {
            #region 生成処理

            switch (TypeArray[i].type)
            {
                case eType.Int:
                    //生成
                    itemName[i] = (GameObject)Instantiate(text, textPos - (space * i), Quaternion.identity);
                    instances[i] = (GameObject)Instantiate(srider_int, sliderPos - (space * i), Quaternion.identity);
                    sliderinput[i] = (GameObject)Instantiate(input_int, slidertextPos - (space * i), Quaternion.identity);
                    //親オブジェクト設定
                    itemName[i].transform.SetParent(Content, false);
                    instances[i].transform.SetParent(Content, false);
                    sliderinput[i].transform.SetParent(Content, false);
                    //名前設定
                    instances[i].name = NameArray[i];
                    sliderinput[i].name = instances[i].name + "InputObj";
                    itemName[i].name = instances[i].name + "NameObj";
                    //項目名
                    names[i] = itemName[i].GetComponent<Text>();
                    names[i].text = instances[i].name;
                    //値の設定
                    sliders[i] = instances[i].GetComponent<Slider>();
                    inputs[i] = sliderinput[i].GetComponent<InputField>();
                    inputs[i].text = ContentsArray[i];
                    break;

                case eType.Double:
                    //生成
                    itemName[i] = (GameObject)Instantiate(text, textPos - (space * i), Quaternion.identity);
                    instances[i] = (GameObject)Instantiate(srider, sliderPos - (space * i), Quaternion.identity);
                    sliderinput[i] = (GameObject)Instantiate(input_S, slidertextPos - (space * i), Quaternion.identity);
                    //親オブジェクト設定
                    itemName[i].transform.SetParent(Content, false);
                    instances[i].transform.SetParent(Content, false);
                    sliderinput[i].transform.SetParent(Content, false);
                    //名前設定
                    instances[i].name = NameArray[i];
                    sliderinput[i].name = instances[i].name + "InputObj";
                    itemName[i].name = instances[i].name + "NameObj";
                    //項目名
                    names[i] = itemName[i].GetComponent<Text>();
                    names[i].text = instances[i].name;
                    //値の設定
                    sliders[i] = instances[i].GetComponent<Slider>();
                    inputs[i] = sliderinput[i].GetComponent<InputField>();
                    inputs[i].text = ContentsArray[i];
                    break;

                case eType.String:
                    //設定
                    itemName[i] = (GameObject)Instantiate(text, textPos - (space * i), Quaternion.identity);
                    instances[i] = (GameObject)Instantiate(input_L, basisPos - (space * i), Quaternion.identity);
                    //親オブジェクト設定
                    itemName[i].transform.SetParent(Content, false);
                    instances[i].transform.SetParent(Content, false);
                    //名前設定
                    instances[i].name = NameArray[i];
                    itemName[i].name = instances[i].name + "NameObj";
                    //項目名
                    names[i] = itemName[i].GetComponent<Text>();
                    names[i].text = instances[i].name;
                    //値の設定
                    inputs[i] = instances[i].GetComponent<InputField>();
                    inputs[i].text = ContentsArray[i];
                    break;

                case eType.Bool:
                    //生成
                    itemName[i] = (GameObject)Instantiate(text, textPos - (space * i), Quaternion.identity);
                    instances[i] = (GameObject)Instantiate(dropDown_S, dropS_Pos - (space * i), Quaternion.identity);
                    //親オブジェクト設定
                    itemName[i].transform.SetParent(Content, false);
                    instances[i].transform.SetParent(Content, false);
                    //名前設定
                    instances[i].name = NameArray[i];
                    itemName[i].name = instances[i].name + "NameObj";
                    //項目名
                    names[i] = itemName[i].GetComponent<Text>();
                    names[i].text = instances[i].name;
                    //値の設定
                    dropDown[i] = instances[i].GetComponent<Dropdown>();
                    //項目のクリア
                    dropDown[i].ClearOptions();
                    //項目追加
                    dropDown[i].options.Add(new Dropdown.OptionData { text = "false" });
                    dropDown[i].options.Add(new Dropdown.OptionData { text = "true" });
                    //初期の値の設定
                    if (ContentsArray[i] == "true") dropDown[i].value = 1;
                    else if (ContentsArray[i] == "false") dropDown[i].value = 0;
                    else dropDown[i].value = 1;
                    break;
                default:
                    break;
            }
            #endregion
        }
    }

    #region テスト用
    // プレハブを元にオブジェクトを生成する(テスト用)
    //GameObject instance = (GameObject)Instantiate(text, basisPos,Quaternion.identity);

    //アタッチされているキャンバスの子オブジェクトにする
    //instance.transform.SetParent(gameObject.transform, false);

    //生成したインスタンスの名前を変更
    //instance.name = "テキストボックス";

    /*テスト

    //スライダー
    instances[0] = (GameObject)Instantiate(srider, sliderPos - (space * 0), Quaternion.identity);
    sliderinput[0] = (GameObject)Instantiate(input_S, slidertextPos - (space * 0), Quaternion.identity);
    itemName[0] = (GameObject)Instantiate(text, textPos - (space * 0), Quaternion.identity);
    instances[0].transform.SetParent(gameObject.transform, false);
    sliderinput[0].transform.SetParent(gameObject.transform, false);
    itemName[0].transform.SetParent(gameObject.transform, false);
    instances[0].name = "スライダー";
    sliderinput[0].name = instances[0].name + "InputObj";
    itemName[0].name = instances[0].name + "NameObj";
    names[0] = itemName[0].GetComponent<Text>();
    names[0].text = instances[0].name ;
    sliders[0] = instances[0].GetComponent<Slider>();
    inputs[0] = sliderinput[0].GetComponent<InputField>();
    inputs[0].text = "1211";

    //ドロップダウン
    instances[1] = (GameObject)Instantiate(dropDown_L, basisPos - (space * 1), Quaternion.identity);
    itemName[1] = (GameObject)Instantiate(text, textPos - (space * 1), Quaternion.identity);
    instances[1].transform.SetParent(gameObject.transform, false);
    itemName[1].transform.SetParent(gameObject.transform, false);
    instances[1].name = "ドロップダウン_L";
    itemName[1].name = instances[1].name + "NameObj";
    names[1] = itemName[1].GetComponent<Text>();
    names[1].text = instances[1].name;
    dropDown[1] = instances[1].GetComponent<Dropdown>();
    dropDown[1].ClearOptions();                                         //項目クリア
    dropDown[1].options.Add(new Dropdown.OptionData { text = "logi1" });//項目追加
    dropDown[1].options.Add(new Dropdown.OptionData {text = "logi2" });
    dropDown[1].options.Add(new Dropdown.OptionData { text = "logi3" });
    dropDown[1].value = 2;                                              //生成時に選択されている項目

    //ドロップダウン(短)
    instances[2] = (GameObject)Instantiate(dropDown_S, dropS_Pos - (space * 2), Quaternion.identity);
    itemName[2] = (GameObject)Instantiate(text, textPos - (space * 2), Quaternion.identity);
    instances[2].transform.SetParent(gameObject.transform, false);
    itemName[2].transform.SetParent(gameObject.transform, false);
    instances[2].name = "ドロップダウン_S";
    itemName[2].name = instances[2].name + "NameObj";
    names[2] = itemName[2].GetComponent<Text>();
    names[2].text = instances[2].name;
    dropDown[2] = instances[2].GetComponent<Dropdown>();
    dropDown[2].ClearOptions();                                        //項目クリア
    dropDown[2].options.Add(new Dropdown.OptionData { text = "true" });//項目追加
    dropDown[2].options.Add(new Dropdown.OptionData { text = "false" });
    dropDown[2].value = 1;                                             //生成時に選択されている項目


    //テキストボックス
    instances[3] = (GameObject)Instantiate(input_L, basisPos - (space * 3), Quaternion.identity);
    itemName[3] = (GameObject)Instantiate(text, textPos - (space * 3), Quaternion.identity);
    instances[3].transform.SetParent(gameObject.transform, false);
    itemName[3].transform.SetParent(gameObject.transform, false);
    instances[3].name = "テキストボックス";
    itemName[3].name = instances[3].name + "NameObj";
    names[3] = itemName[3].GetComponent<Text>();
    names[3].text = instances[3].name;
    inputs[3] = instances[3].GetComponent<InputField>();
    inputs[3].text = "test input";
    */
    #endregion
}
