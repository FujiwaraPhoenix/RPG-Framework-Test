using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileParser : MonoBehaviour {
    public static TextFileParser tfp;
    public string heldData;
    public string[] itemList;

    private void Start()
    {
        readString();
    }

    void Awake()
    {
        if (tfp == null)
        {
            DontDestroyOnLoad(gameObject);
            tfp = this;
        }
        else if (tfp != this)
        {
            Destroy(gameObject);
        }
    }

    void readString()
    {
        string path = "Assets/Text Files/TestSkillList.txt";
        StreamReader reader = new StreamReader(path);
        heldData = reader.ReadToEnd();
        reader.Close();
        tStringToList();
    }

    void tStringToList()
    {
        itemList = heldData.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
    }
}
