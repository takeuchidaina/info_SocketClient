using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;

/*InputFieldのバグ直し　90%ぐらい*/
/*これは完全ではありません一部の条件下ではバグが発生します*/
/*誰か代わりにやってください*/
public class InputFieldBugFix : MonoBehaviour
{
    //対象のInputFieldを入れるために使用
    InputField FixInputField;

    //直前のテキストと長さを入れるのに使用
    public string previousText;

    //バグった文字を入れるのに使用
    public string bugText;

    //正常な文字、バグって増加した文字、直前の文字数を数えるために使用
    public int normalTextCount = 0;
    public int bugTextCount = 0;
    public int previousLength = 0;

    //バグった文字を見つけるのに使用
    public bool bugCheckFlg = false;

    //全角、半角文字を調べるためにshift_jisを使います
    Encoding encoding = Encoding.GetEncoding("shift_jis");

    void Start()
    {
        //対象のInputFieldを取得
        FixInputField = this.gameObject.GetComponent<InputField>();
    }

    void Update()
    {
        //対象のInputFieldが選択されている
        if (FixInputField.GetComponent<InputField>().isFocused == true)
        {
            //これはInputFieldの色を変えるだけ
            FixInputField.GetComponent<Image>().color = Color.green;

            //マウスクリックしたけどまだ対象を選択している状態なら
            if (UnityEngine.Input.GetMouseButtonDown(0) == true ||
                UnityEngine.Input.GetMouseButtonDown(1) == true ||
                UnityEngine.Input.GetMouseButtonDown(2) == true)
            {
                Fix();    //修正処理をする関数
            }

            //選択中は常にテキストを更新し続けておく
            previousText = FixInputField.text;
            previousLength = previousText.Length;
        }
        else
        {
            //これはInputFieldの色を変えるだけ
            FixInputField.GetComponent<Image>().color = Color.cyan;

            Fix();    //修正処理をする関数
        }
    }

    //修正処理をする関数
    void Fix()
    {
        //入力されたのが直前の状態と同じか調べる
        //半角文字は常に変更を検知できるが
        //全角文字は変換を確定させるまで変更を検知できないように
        //Unityではなっているのでこれを利用する

        //選択が解除された時のテキストを取得
        bugText = FixInputField.text;

        //previousTextとbugTextの中身が一緒ではないなら倍加バグが発生したと捉える
        //また、bugTextの中身が空なら処理しない
        if (bugText != previousText && bugText != "")
        {
            previousText = "";

            //正常な文字を入れ直していく
            for (int i = 0; i < previousLength; i++)
            {
                char setText = bugText[i];
                previousText += setText.ToString();
            }

            //入力されたテキストを見ていく
            for (int i = 0; i < bugText.Length; i++)
            {
                //1文字ずつ見ていく
                char character = bugText[i];

                //下の.GetByteCount()を使うには
                //string型ではないとダメなのでstring型に変換
                string stringChar = character.ToString();

                //2バイトは全角文字
                //そして全角文字が見つかるとその後の文字はすべて倍加してしまう
                //元々入ってるテキストは見ない
                if ( i > previousText.Length && encoding.GetByteCount(stringChar) == 2)
                {
                    bugCheckFlg = true;
                }

                //bugCheckFlgがtrueなら
                if (bugCheckFlg == true)
                {
                    //バグった文字数を数える
                    bugTextCount++;
                }
                else
                {
                    //正常な文字を数える
                    normalTextCount++;
                }
            }

            //倍加を検知し修正した文字の数を入れ直していく
            for (int i = previousLength; i < normalTextCount + bugTextCount / 2; i++)
            {
                char fixText = bugText[i];
                previousText += fixText.ToString();
            }

            //修正したのを代入
            FixInputField.text = previousText;

            //リセット
            normalTextCount = 0;
            bugTextCount = 0;
            bugCheckFlg = false;
        }
    }
}