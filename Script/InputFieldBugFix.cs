using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;

/*InputFieldのバグ直し　95%ぐらい*/
/*倍加バグは発生しないはずです   */
public class InputFieldBugFix : MonoBehaviour
{
    public InputField FixInputField;

    public Text inputText;
    public string previousText;

    void Update()
    {
        //対象のInputFieldが選択されている
        if (FixInputField.GetComponent<InputField>().isFocused == true)
        {
            //マウスクリックしたけどまだ対象を選択している状態なら
            if (UnityEngine.Input.GetMouseButtonDown(0) == true ||
                UnityEngine.Input.GetMouseButtonDown(1) == true ||
                UnityEngine.Input.GetMouseButtonDown(2) == true)
            {
                Fix();    //修正関数
            }
            //バグ発生してないときのテキスト
            previousText = inputText.text;
        }
        else
        {
            //選択が解除orマウスのアクションがあったらバグ発生するので
            //修正する関数を呼び出す
            Fix();
        }
    }

    public void Fix()
    {
        //倍加する前のテキストを代入
        FixInputField.text = previousText;
    }
}