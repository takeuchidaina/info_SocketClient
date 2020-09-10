using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCheckButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CheckDisplay()
    {
        GameObject obj = transform.Find("Canvas_Delete").gameObject;    //オブジェクトを格納
        obj.SetActive(true);                                              //オブジェクトのアクティブ可
    }

    public void BackDisplay()           //戻る
    {
        GameObject obj = GameObject.Find("Canvas_Delete");  //オブジェクトを格納
        obj.SetActive(false);                               //オブジェクトの非アクティブ可
    }
    
}
