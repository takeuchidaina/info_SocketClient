﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Input_int : MonoBehaviour
{
    InputField inputField;
    Slider slider;      //同期しているスライダー
    string name;        //このオブジェクトの名前
    string sliderName;  //同期しているスライダーの名前
    string inputValue;  //入力された値
    int value;          //整数
    const int MAX_VALUE = 300;

    public string TextValue
    {
        //ゲッターセッター
        get { return this.inputValue; }
        set { inputValue = value;}
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
        name = (transform.name);                    //名前取得
        sliderName = name.Replace("InputObj", "");  //余計なもの削除
        slider = GameObject.Find(sliderName).GetComponent<Slider>();

        ValCheck();

        inputField.text = inputValue;
    }

    public void InputLogger()
    {

        ValCheck();

        //Debug.Log("obj_Input_S :" + inputValue);
        //ログ表示
        InitInputField();
    }

    void InitInputField()
    {
        inputField.text = "";
        // 値をリセット(フォーカスを外す)
        inputField.text = inputValue;
        //フォーカスを外した後に値を入れることで入力内容がわかる

        //inputField_S.ActivateInputField();
        // フォーカス
    }

    public void ValCheck()
    {
        inputValue = inputField.text;     
        value = int.Parse(inputValue);      //範囲外か確認するため一度intに変換
        if (value < 0) value = 0;           //0未満の時は0に固定
        else if (MAX_VALUE < value) value = MAX_VALUE;  //上限値を超えていたら上限値に設定

        inputValue = value.ToString();  //ここで代入

        slider.value = value;
    }
}
