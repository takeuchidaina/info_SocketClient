using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Slider : MonoBehaviour
{
    float value;
    string name;
    InputField inputObj;    //同期しているInputオブジェクト
    // Start is called before the first frame update
    void Start()
    {
        name = (transform.name);                //名前取得
        value = GetComponent<Slider>().value;   
        inputObj = GameObject.Find(name + "InputObj").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string TextValue
    {
        get { return inputObj.text; }
    }

    public void OnValueChanged()
    {   //値が変更されたとき
        value = GetComponent<Slider>().value;   //値取得
        inputObj.text = value.ToString("f1");   //取得した値をinputObjに送る
        //Debug.Log("slider" + value);
    }
}
