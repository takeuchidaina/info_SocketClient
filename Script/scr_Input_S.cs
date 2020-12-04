﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*obj_Input_S用のスクリプトです         */
/*入力された値をinputValueに格納してます*/
/*数字と小数点のみを認識させます        */
public class scr_Input_S : MonoBehaviour
{
    InputField inputField;
    Slider slider;      //同期しているスライダー
    string name;        //このオブジェクトの名前
    string sliderName;  //同期しているスライダーの名前
    string inputValue;  //入力された値
    float value;        //実数型


    void Start()
    {
        inputField = GetComponent<InputField>();
        name = (transform.name);                    //名前取得
        sliderName = name.Replace("InputObj", "");  //余計なもの削除
        slider = GameObject.Find(sliderName).GetComponent<Slider>();

        inputValue = inputField.text;

        value = float.Parse(inputValue);    //範囲外か確認するため一度floatに変換
        if (value < 0) value = 0;           //0未満の時は0に固定
        else if (100 < value) value = 100;  //上限値を超えていたら上限値に設定

        inputValue = value.ToString("f1");  //ここで代入
        inputField.text = inputValue;

        slider.value = value;               //スライダーの値を変更
    }

    public void InputLogger()
    {

        inputValue = inputField.text;
        value = float.Parse(inputValue);    //範囲外か確認するため一度floatに変換
        if (value < 0) value = 0;           //0未満の時は0に固定
        else if (100 < value) value = 100;  //上限値を超えていたら上限値に設定

        inputValue = value.ToString("f1");  //ここで代入

        slider.value = value;

        //Debug.Log("obj_Input_S :" + inputValue);
        //ログ表示
        InitInputField();
    }

    public string TextValue
    {
        //ゲッター
        get { return this.inputValue; }
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
}
