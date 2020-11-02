using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class JsonCreater : MonoBehaviour
{

   public static  JsonCreater instance;
    Hashtable json = new Hashtable();
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create(string resMsg)
    {
        string[] vs = resMsg.Split(',');
        if (json.Count == 0)
        {
            if (vs.Length != 0)
            {

                //http://straylight-engineer.blogspot.com/2014/01/blog-post.html
                foreach (string hoge in vs)
                {
                    Debug.Log("君の気持ち、分割する！：" + hoge);
                    string patternName = "(.*?)(?=:)";
                    string patternValue = "(?<=:)(.*)";
                    Regex[] regex = { new Regex(patternName), new Regex(patternValue) };

                    Match[] match = { regex[0].Match(hoge), regex[1].Match(hoge) };


                    json.Add(match[0], match[1]);

                }

                //debug
                foreach (DictionaryEntry de in json)
                {
                    Debug.Log("連想配列Keys :" + de.Key);
                    Debug.Log("連想配列Values :" + de.Value);


                }
                //foreach (Hashtable j in json)
                //{

                //}

            }

        }
    }
}
