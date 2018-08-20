using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public float xOffset;
    public int pPos = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (FightMenuController.mc.currentScreen.Equals(FightMenuController.menuType.OptionSelect))
        {
            transform.localPosition = new Vector2(xOffset, ArrayHolder.puppeteer.textList[pPos].transform.localPosition.y);
        }
        else if (FightMenuController.mc.currentScreen.Equals(FightMenuController.menuType.TargetSelectE))
        {
            transform.localPosition = new Vector2(xOffset, ArrayHolder.puppeteer.textList2[pPos].transform.localPosition.y);
        }
    }
}
