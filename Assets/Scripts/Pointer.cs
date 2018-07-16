using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public ArrayHolder arH;
    //public float[] YVals = new float[5];
    public float xOffset;
    public int pPos = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (FightMenuController.currentScreen.Equals(FightMenuController.menuType.OptionSelect))
        {
            transform.position = new Vector2(xOffset, arH.textList[pPos].transform.position.y);
        }
	}
}
