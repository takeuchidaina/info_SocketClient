using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Canvas/AccessGuide/AccessPoint/Text_AccessPoint").GetComponent<Text>().text = 
            ServerList.SelectServer.ServerRoomName;
        //ManipulateScene、上部の接続名表示

        GameObject.Find("Canvas/AccessGuide/IPAdress/Text_IPAdress").GetComponent<Text>().text = 
            ServerList.SelectServer.ServerRoomIP;
        //ManipulateScene、上部のIP表示

    }

}
