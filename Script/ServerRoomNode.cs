using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerRoomNode : MonoBehaviour
{
    private string serverName = "20.00.10.24";

    public string ServerName
    {
        get { return this.serverName; }
        set { this.serverName = value; }
    }

    private void Start()
    {
        Text nameText = this.GetComponentInChildren<Text>();
        nameText.text = serverName;
    }
}
