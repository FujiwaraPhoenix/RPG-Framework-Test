using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayHolder : MonoBehaviour {
    //Base Menu/Inv/Skill select options
    public Text[] textList = new Text[6];
    //Player/Enemy Select Options
    public Text[] textList2 = new Text[5];

    //Actual list of enemies
    public Enemy[] enemyList = new Enemy[5];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TextFileParser.tfp.itemList[0] != null)
            updateListing();
	}

    //This is a temporary version for testing; the full one will read the data for each individual character's skillset and parse with those instead.
    void updateListing()
    {
        for (int i = 0; i < textList.Length; i++)
        {
            if (i < FightMenuController.mc.itemsDisplayed + 1)
            {
                textList[i].text = TextFileParser.tfp.itemList[i + FightMenuController.mc.minMenuItem];
            }
            else
            {
                textList[i].enabled = false;
            }
        }
    }
}
