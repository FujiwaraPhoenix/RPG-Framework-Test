using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayHolder : MonoBehaviour {
    public Text[] textList = new Text[5];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TextFileParser.tfp.itemList[0] != null)
            updateListing();
	}

    void updateListing()
    {
        for (int i = 0; i < textList.Length; i++)
        {
            if (i < MenuController.mc.itemsDisplayed + 1)
            {
                textList[i].text = TextFileParser.tfp.itemList[i + MenuController.mc.minMenuItem];
            }
            else
            {
                textList[i].enabled = false;
            }
        }
    }
}
