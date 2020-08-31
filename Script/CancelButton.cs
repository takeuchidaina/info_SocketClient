using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    private static bool cancelFlg = false;

    //Cancelされたらリストを作らない
    public static bool CancelFlg
    {
        get { return cancelFlg; }
        set { cancelFlg = value; }
    }

    void Update()
    {
      //  Debug.Log(cancelFlg);
    }

}
