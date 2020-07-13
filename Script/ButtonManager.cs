using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackServerList()
    {
        GameObject.Find("Sceane_Manipulate").SetActive(false);
        GameObject.Find("Sceane_ServerSelect").SetActive(true);
    }

    public void ApplicationEnd()
    {
        Application.Quit();
    }
}