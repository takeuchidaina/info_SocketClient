using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreate : MonoBehaviour
{

    /*プレハブ格納用*/
    private GameObject srider;
    private GameObject dropDown_L;
    private GameObject dropDown_S;
    private GameObject input_L;
    private GameObject input_S;
    private GameObject text;

    /*生成したインスタンスを格納する配列*/
    private GameObject[] instances;     //インスタンス格納
    private Slider[] sliders;           //スライダー
    private GameObject[] sliderinput;   //スライダーの場合inputもあるので...
    private GameObject[] itemName;      //項目名のオブジェクト
    private Text[] names;               //項目名のテキスト
    private Dropdown[] dropDown;        //ドロップダウン格納
    private InputField[] inputs;        //インプットフィールド格納


    /*生成数*/
    const int MaxCreateNum = 50;    //最大生成数
    int createNum = 3;              //生成する項目数

    /*座標*/
    Vector3 basisPos = new Vector3(0.0f, 700.0f, 0.0f);             //一番上
    Vector3 sliderPos = new Vector3(-110.0f, 700.0f, 0.0f);         //スライダーの座標
    Vector3 slidertextPos = new Vector3(380.0f, 700.0f, 0.0f);      //スライダーのテキスト入力の座標
    Vector3 dropS_Pos = new Vector3(260.0f, 700.0f, 0.0f);            //ドロップダウン(短)の座標
    Vector3 textPos = new Vector3(0.0f, 700.0f, 0.0f) +
                      new Vector3(-170.0f, 70.0f, 0.0f);            //テキストの座標
    Vector3 space = new Vector3(0.0f, 230.0f, 0.0f);                //間隔

    // Start is called before the first frame update
    void Start()
    {
        /*初期化*/
        instances = new GameObject[MaxCreateNum];                   //配列初期化
        sliders = new Slider[MaxCreateNum];
        sliderinput = new GameObject[MaxCreateNum];
        itemName = new GameObject[MaxCreateNum];
        names = new Text[MaxCreateNum];
        dropDown = new Dropdown[MaxCreateNum];
        inputs = new InputField[MaxCreateNum];

        /*プレハブ読み込み*/
        srider = (GameObject)Resources.Load("obj_Slider");          //スクロール
        dropDown_L = (GameObject)Resources.Load("obj_Dropdown_L");  //ドロップダウン(長い)
        dropDown_S = (GameObject)Resources.Load("obj_Dropdown_S");  //ドロップダウン(短い)
        input_L = (GameObject)Resources.Load("obj_input_L");        //テキストボックス(長い)
        input_S = (GameObject)Resources.Load("obj_input_S");        //テキストボックス(スライダー)
        text = (GameObject)Resources.Load("obj_Text");              //テキスト

        // プレハブを元にオブジェクトを生成する(テスト用)
        //GameObject instance = (GameObject)Instantiate(text, basisPos,Quaternion.identity);

        //アタッチされているキャンバスの子オブジェクトにする
        //instance.transform.SetParent(gameObject.transform, false);

        //生成したインスタンスの名前を変更
        //instance.name = "テキストボックス";

        /*テストで三つ生成する(スクロールのみ)*/
        for (int i= 0; i < createNum; i++)//生成する数だけ繰り返す
        {
         //     instances[i] = (GameObject)Instantiate(srider, basisPos - (space * i), Quaternion.identity);
         //     instances[i].transform.SetParent(gameObject.transform, false);
         //     itemName[i] = (GameObject)Instantiate(text, basisPos - (space * i), Quaternion.identity);
         //     itemName[i].transform.SetParent(gameObject.transform, false);
        }

        /*テスト*/
        
        /*スライダー*/
        instances[0] = (GameObject)Instantiate(srider, sliderPos - (space * 0), Quaternion.identity);
        sliderinput[0] = (GameObject)Instantiate(input_S, slidertextPos - (space * 0), Quaternion.identity);
        itemName[0] = (GameObject)Instantiate(text, textPos - (space * 0), Quaternion.identity);
        instances[0].transform.SetParent(gameObject.transform, false);
        sliderinput[0].transform.SetParent(gameObject.transform, false);
        itemName[0].transform.SetParent(gameObject.transform, false);
        instances[0].name = "スライダー";
        sliderinput[0].name = instances[0].name + "InputObj";
        itemName[0].name = instances[0].name + "NameObj";
        /*初期値の設定*/
        names[0] = itemName[0].GetComponent<Text>();
        names[0].text = instances[0].name ;
        sliders[0] = instances[0].GetComponent<Slider>();
        inputs[0] = sliderinput[0].GetComponent<InputField>();
        inputs[0].text = "1211";

        /*ドロップダウン(長)*/
        instances[1] = (GameObject)Instantiate(dropDown_L, basisPos - (space * 1), Quaternion.identity);
        itemName[1] = (GameObject)Instantiate(text, textPos - (space * 1), Quaternion.identity);
        instances[1].transform.SetParent(gameObject.transform, false);
        itemName[1].transform.SetParent(gameObject.transform, false);
        instances[1].name = "ドロップダウン_L";
        itemName[1].name = instances[1].name + "NameObj";
        /*初期設定*/
        names[1] = itemName[1].GetComponent<Text>();
        names[1].text = instances[1].name;
        dropDown[1] = instances[1].GetComponent<Dropdown>();
        dropDown[1].ClearOptions();                                         //項目クリア
        dropDown[1].options.Add(new Dropdown.OptionData { text = "logi1" });//項目追加
        dropDown[1].options.Add(new Dropdown.OptionData {text = "logi2" });
        dropDown[1].options.Add(new Dropdown.OptionData { text = "logi3" });
        dropDown[1].value = 2;                                              //生成時に選択されている項目


        /*ドロップダウン(短)*/
        instances[2] = (GameObject)Instantiate(dropDown_S, dropS_Pos - (space * 2), Quaternion.identity);
        itemName[2] = (GameObject)Instantiate(text, textPos - (space * 2), Quaternion.identity);
        instances[2].transform.SetParent(gameObject.transform, false);
        itemName[2].transform.SetParent(gameObject.transform, false);
        instances[2].name = "ドロップダウン_S";
        itemName[2].name = instances[2].name + "NameObj";
        /*初期設定*/
        names[2] = itemName[2].GetComponent<Text>();
        names[2].text = instances[2].name;
        dropDown[2] = instances[2].GetComponent<Dropdown>();
        dropDown[2].ClearOptions();                                        //項目クリア
        dropDown[2].options.Add(new Dropdown.OptionData { text = "true" });//項目追加
        dropDown[2].options.Add(new Dropdown.OptionData { text = "false" });
        dropDown[2].value = 1;                                             //生成時に選択されている項目


        /*テキストボックス*/
        instances[3] = (GameObject)Instantiate(input_L, basisPos - (space * 3), Quaternion.identity);
        itemName[3] = (GameObject)Instantiate(text, textPos - (space * 3), Quaternion.identity);
        instances[3].transform.SetParent(gameObject.transform, false);
        itemName[3].transform.SetParent(gameObject.transform, false);
        instances[3].name = "テキストボックス";
        itemName[3].name = instances[3].name + "NameObj";
        /*初期値の設定*/
        names[3] = itemName[3].GetComponent<Text>();
        names[3].text = instances[3].name;
        inputs[3] = instances[3].GetComponent<InputField>();
        inputs[3].text = "test input";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
