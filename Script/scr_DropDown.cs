using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*ドロップダウンの値を変更したときにラベル(選択されている値)を取得する*/
public class scr_DropDown : MonoBehaviour
{
    Dropdown dropDown;
    string label;
    bool flg = false;
    // Start is called before the first frame update
    void Start()
    {   //初期化
        dropDown = GetComponent<Dropdown>();
        label = dropDown.transform.FindChild("Label").GetComponent<Text>().text;
        flg = true;
        //Debug.Log(label);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string TextValue
    {
        get { return label; }
        set { label = value; }
    }

    public void OnValueChanged()
    {
        if (flg)
        {//初回時は動かしたくないため
            //値が変更された時ラベルの取得
            label = dropDown.transform.FindChild("Label").GetComponent<Text>().text;
            //Debug.Log(label);
        }

    }
}
