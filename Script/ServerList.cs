using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerList : MonoBehaviour
{
    private GameObject content;
    private GameObject serverRoomPrefab;
    private List<string> ipList = new List<string>();

    private void Awake()
    {
        ipList.Add("20.00.1.17");
        ipList.Add("20.00.10.4");
        ipList.Add("20.00.10.24");
        ipList.Add("20.00.12.15");
        ipList.Add("20.01.1.7");

        content = GameObject.Find("Content");
        serverRoomPrefab = (GameObject)Resources.Load("ServerRoomNode");

        Debug.Log(ipList.Count);
    }

    private void Start()
    {
        foreach(string ip in ipList)
        {
            Debug.Log(ip);
            GameObject node = Instantiate(serverRoomPrefab, content.transform);
            ServerRoomNode serverRoomNode = node.GetComponent<ServerRoomNode>();
            serverRoomNode.ServerName = ip;
            //serverRoomNode.SetServerIp(ip);
        }
    }
}
