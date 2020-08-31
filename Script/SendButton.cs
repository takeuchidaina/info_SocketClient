using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendButton : MonoBehaviour
{
    private static string ip;
    private static string listName;

    public InputField IP;
    public InputField Name;

    //ゲッター
    public static string Get_IP
    {
        get { return ip; }
    }

    public static string Get_Name
    {
        get { return listName; }
    }

    public void Send()
    {
        ip = IP.text;
        listName = Name.text;
        Debug.Log("サーバー名：" + listName);
        Debug.Log("サーバーIP：" + ip);
    }
}
