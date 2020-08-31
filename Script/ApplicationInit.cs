using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationInit : MonoBehaviour
{
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(720, 540, false,60);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
