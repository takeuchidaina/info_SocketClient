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
    private GameObject[] sliderinput;   //スライダーの場合inputもあるので...
    private GameObject[] texts;         //項目名のオブジェクト

    /*生成数*/
    const int MaxCreateNum = 50;    //最大生成数
    int createNum = 3;              //表示する項目数

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
        sliderinput = new GameObject[MaxCreateNum];
        texts = new GameObject[MaxCreateNum];
        
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
         //     texts[i] = (GameObject)Instantiate(text, basisPos - (space * i), Quaternion.identity);
         //     texts[i].transform.SetParent(gameObject.transform, false);
        }

        /*テスト*/
        
        /*スライダー*/
        instances[0] = (GameObject)Instantiate(srider, sliderPos - (space * 0), Quaternion.identity);
        sliderinput[0] = (GameObject)Instantiate(input_S, slidertextPos - (space * 0), Quaternion.identity);
        texts[0] = (GameObject)Instantiate(text, textPos - (space * 0), Quaternion.identity);
        instances[0].transform.SetParent(gameObject.transform, false);
        sliderinput[0].transform.SetParent(gameObject.transform, false);
        texts[0].transform.SetParent(gameObject.transform, false);
        instances[0].name = "スライダー";

        /*ドロップダウン(長)*/
        instances[1] = (GameObject)Instantiate(dropDown_L, basisPos - (space * 1), Quaternion.identity);
        texts[1] = (GameObject)Instantiate(text, textPos - (space * 1), Quaternion.identity);
        instances[1].transform.SetParent(gameObject.transform, false);
        texts[1].transform.SetParent(gameObject.transform, false);
        instances[1].name = "ドロップダウン_L";

        /*ドロップダウン(短)*/
        instances[2] = (GameObject)Instantiate(dropDown_S, dropS_Pos - (space * 2), Quaternion.identity);
        texts[2] = (GameObject)Instantiate(text, textPos - (space * 2), Quaternion.identity);
        instances[2].transform.SetParent(gameObject.transform, false);
        texts[2].transform.SetParent(gameObject.transform, false);
        instances[2].name = "ドロップダウン_S";

        /*テキストボックス*/
        instances[3] = (GameObject)Instantiate(input_L, basisPos - (space * 3), Quaternion.identity);
        texts[3] = (GameObject)Instantiate(text, textPos - (space * 3), Quaternion.identity);
        instances[3].transform.SetParent(gameObject.transform, false);
        texts[3].transform.SetParent(gameObject.transform, false);
        instances[3].name = "テキストボックス";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
