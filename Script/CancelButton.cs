using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    private static bool cancelFlg = false;

    //ゲッター
    public bool Get_CancelFlg()
    {
        return cancelFlg;
    }

    public void Set_CancelFlg(bool _flg)
    {
        cancelFlg = _flg;
    }

    void Update()
    {
      //  Debug.Log(cancelFlg);
    }

}
