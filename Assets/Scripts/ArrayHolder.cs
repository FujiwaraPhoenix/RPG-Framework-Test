using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayHolder : MonoBehaviour {
    public static ArrayHolder puppeteer;

    //Base Menu/Inv/Skill select options
    public Text[] textList = new Text[6];
    //Player/Enemy Select Options
    public Text[] textList2 = new Text[6];

    //Actual list of enemies
    public Enemy[] enemyList = new Enemy[5];
    //And players
    public PlayerCharacter[] pcList = new PlayerCharacter[5];
    //And the menu pointers.
    public Pointer[] pointers = new Pointer[2];
	// Use this for initialization
	void Start () {
		
	}

    void Awake()
    {
        if (puppeteer == null)
        {
            DontDestroyOnLoad(gameObject);
            puppeteer = this;
        }
        else if (puppeteer != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        if ((TextFileParser.tfp.itemList[0] != null) && ((FightMenuController.mc.currentScreen != FightMenuController.menuType.TargetSelectE) && (FightMenuController.mc.currentScreen != FightMenuController.menuType.TargetSelectP)))
        {
            updateListing();
        }
        else
        {
            updateTargetListing();
        }
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

    //To do: Move the 'activate enemy' section to another code (probably the fight initializer).
    void updateTargetListing()
    {
        if (FightMenuController.mc.currentScreen == FightMenuController.menuType.TargetSelectE)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < FightMenuController.mc.enemyCount)
                {
                    textList2[i].gameObject.SetActive(true);
                    textList2[i].text = enemyList[i].charName;
                }
                else
                {
                    textList2[i].gameObject.SetActive(false);
                }
            }
            //resize the menu, change max/min, etc.;
            FightMenuController.mc.changeMaxMin(0, FightMenuController.mc.enemyCount-1, FightMenuController.mc.enemyCount-1, FightMenuController.mc.enemyCount-1);
        }
        else if (FightMenuController.mc.currentScreen == FightMenuController.menuType.TargetSelectP)
        {
            for (int i = 0; i < FightMenuController.mc.playerCount; i++)
            {
                textList2[i].text = pcList[i].charName;
            }
        }
    }

}
