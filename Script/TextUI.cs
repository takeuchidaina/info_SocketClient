using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    // UI Text指定用
    public Text TextFrame;

    // 表示する変数
    private double frame;

    GameObject jsonReader;    //jsonReaderScriptが入るゲームオブジェクトの変数

    JsonReader script;        //jsonReaderScriptが入る変数

    // Use this for initialization
    void Start()
    {
        jsonReader = GameObject.Find("kami");               //kamiオブジェクトの名前から取得して変数に格納する
        script = jsonReader.GetComponent<JsonReader>();     //kamiオブジェクトの中にあるjsonReaderScriptを取得して変数に格納する
    }

    // Update is called once per frame
    void Update()
    {
        //string unitychanHP = script.inputJson.NoisePath;    //新しく変数を宣言してその中にUnityChanScriptの変数HPを代入する
        //TextFrame.text = string.Format(unitychanHP);        //UI表示
    }

}
