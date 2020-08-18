using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************************************
ToDo:ファイルにサーバー名、IP、識別時間を格納させる
     ファイルからサーバー名、IP、識別時間を格納させる
*****************************************************************************************************/
public class ServerRoom
{
    private string serverRoomName;
    private string serverRoomIP;

    private DateTime serverRoomIdentTime;

    public string ServerRoomName
    {
        get { return this.serverRoomName; }
    }

    public string ServerRoomIP
    {
        get { return this.serverRoomIP; }
    }

    public DateTime ServerRoomIdentTime
    {
        get { return this.serverRoomIdentTime; }
    }

    public ServerRoom(string _Name,string _IP)
    {
        serverRoomName = _Name;
        serverRoomIP = _IP;
        serverRoomIdentTime = DateTime.Now;

        Debug.Log(serverRoomIdentTime);
    }
}

public class ServerList : MonoBehaviour
{
    private GameObject content;
    private GameObject serverRoomPrefab;
    private List<ServerRoom> serverRoomList = new List<ServerRoom>();
    private ServerRoom selectServerRoom = null;

    private void Awake()
    {
        serverRoomList.Add(new ServerRoom("浦部の部屋", "20.00.10.24"));
        serverRoomList.Add(new ServerRoom("河本の部屋", "20.00.1.15"));

        content = GameObject.Find("Content");
        serverRoomPrefab = (GameObject)Resources.Load("ServerRoomNode");
    }

    private void Start()
    {
        foreach(var serverRoom in serverRoomList)
        {
            GameObject node = Instantiate(serverRoomPrefab, content.transform) as GameObject;
            node.name = serverRoom.ServerRoomName + serverRoom.ServerRoomIdentTime;
            ServerRoomNode serverRoomNode = node.GetComponent<ServerRoomNode>();
            serverRoomNode.ServerRoom = serverRoom;
        }
    }

    public void AddServerList()
    {
        serverRoomList.Add(new ServerRoom("新規", "00.00.00.00"));
        GameObject node = Instantiate(serverRoomPrefab, content.transform) as GameObject;
        node.name = serverRoomList[serverRoomList.Count - 1].ServerRoomName + 
            serverRoomList[serverRoomList.Count - 1].ServerRoomIdentTime;
        ServerRoomNode serverRoomNode = node.GetComponent<ServerRoomNode>();
        serverRoomNode.ServerRoom = serverRoomList[serverRoomList.Count - 1];
    }

    public void RemoveServerList()
    {
        if(selectServerRoom == null)
        {
            return;
        }

        serverRoomList.RemoveAt(serverRoomList.IndexOf(selectServerRoom));
        Destroy(GameObject.Find(selectServerRoom.ServerRoomName + selectServerRoom.ServerRoomIdentTime));

        selectServerRoom = null;
    }

    public void SelectServerRoom(ServerRoom _serverRoom)
    {
        selectServerRoom = _serverRoom;

        Debug.Log(selectServerRoom.ServerRoomName);
        Debug.Log(selectServerRoom.ServerRoomIP);
    }
}
