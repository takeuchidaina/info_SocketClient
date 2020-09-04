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

    public void PushButton()
    {
        CancelFlg = true;
    }


    private void Awake()
    {
        cancelFlg = false;
    }


    void Update()
    {
      Debug.Log(cancelFlg);
    }

}
