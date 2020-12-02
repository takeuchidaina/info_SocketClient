using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigChanger : MonoBehaviour
{
    string objName = "テキストボックス";
    string text;
    [SerializeField]
    SendSlashCommand sendSCommand;

    private void Start()
    {
        sendSCommand = this.GetComponent<SendSlashCommand>();
    }

    public void ChangeConfig()
    {
        Debug.Log("世界");

        Text[] myText = GameObject.Find(objName).GetComponentsInChildren<Text>();

        text = myText[1].text;

        sendSCommand.ChangeSlashCommand(objName, text);

        Debug.Log(myText[1].text);

        myText = null;
    }
}
