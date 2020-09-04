using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI処理のクラスを使用する宣言
using UnityEngine.UI;

public class Animetion : MonoBehaviour
{
    //接続中なら画像をアニメーション再生させて接続試行中...って表示させたい
    //接続できたら画像をアニメーションを止めて、接続中って表示にしたい

    bool flg = false; //持ってくるフラグ

    //Image コンポーネントを格納する変数
    private Image ani_Image;

    //スプライトオブジェクトを格納する配列
    public Sprite[] ani_Sprite;

    //アニメーション画像の番号
    int number;

    //アニメーションのスピード用タイマー
    int timer;


    //ゲーム開始時に実行する処理
    void Start()
    {
        //Image コンポーネントを取得して変数 ani_Image に格納
        ani_Image = GetComponent<Image>();

        number = 0;
        timer = 0;
    }

    //ゲーム実行中に毎フレーム実行する処理
    void Update()
    {

        if (Mathf.Approximately(Time.timeScale, 0f)) return;

        //今は左クリックで代用
        if (UnityEngine.Input.GetMouseButtonDown(0) == true)
        {
            flg = true;
        }

        if (flg == true)
        {

        }
        else
        {
            PlayAnimetion();
        }
    }
    //アニメーション再生
    void PlayAnimetion()
    {
        //カウントアップ
        timer++;

        //一定までカウントしたら画像を切り替える
        if (timer >= 10)
        {
            number++;

            //タイマーリセット
            timer = 0;
        }
        //画像の枚数分まで超えたら0に戻す
        if (number == 7)
        {
            number = 0;
        }
        //アニメーション再生番号
        switch (number)
        {
            case 0:
                // スプライトオブジェクトの変更
                //（配列 ani_Sprite[0] に格納したスプライトオブジェクトを変数 ani_Image に格納したImage コンポーネントに割り当て）
                ani_Image.sprite = ani_Sprite[0];
                break;

            case 1:
                // スプライトオブジェクトの変更
                //（配列 ani_Sprite[1] に格納したスプライトオブジェクトを変数 ani_Image に格納したImage コンポーネントに割り当て）
                ani_Image.sprite = ani_Sprite[1];
                break;

            case 2:
                // スプライトオブジェクトの変更
                //（配列 ani_Sprite[2] に格納したスプライトオブジェクトを変数 ani_Image に格納したImage コンポーネントに割り当て）
                ani_Image.sprite = ani_Sprite[2];
                break;

            case 3:
                // スプライトオブジェクトの変更
                //（配列 ani_Sprite[3] に格納したスプライトオブジェクトを変数 ani_Image に格納したImage コンポーネントに割り当て）
                ani_Image.sprite = ani_Sprite[3];
                break;

            case 4:
                // スプライトオブジェクトの変更
                //（配列 ani_Sprite[4] に格納したスプライトオブジェクトを変数 ani_Image に格納したImage コンポーネントに割り当て）
                ani_Image.sprite = ani_Sprite[4];
                break;

            case 5:
                // スプライトオブジェクトの変更
                //（配列 ani_Sprite[5] に格納したスプライトオブジェクトを変数 ani_Image に格納したImage コンポーネントに割り当て）
                ani_Image.sprite = ani_Sprite[5];
                break;

            case 6:
                // スプライトオブジェクトの変更
                //（配列 ani_Sprite[6] に格納したスプライトオブジェクトを変数 ani_Image に格納したImage コンポーネントに割り当て）
                ani_Image.sprite = ani_Sprite[6];
                break;

            case 7:
                // スプライトオブジェクトの変更
                //（配列 ani_Sprite[7] に格納したスプライトオブジェクトを変数 ani_Image に格納したImage コンポーネントに割り当て）
                ani_Image.sprite = ani_Sprite[7];
                break;

            default:
                break;
        }
    }

}
