using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMenuController : MonoBehaviour {
    public static FightMenuController mc;

    //In order: Determines current menu value, amount of items in the set, and how many items will be displayed.
    public int currentPointer, menuSize, itemsDisplayed;

    //Affected by menuSize, this determines which menu options are available.
    public int maxMenuItem, minMenuItem;

    //See if the menu is a 1d or 2d array
    public bool isThis2D;

    public Pointer p;

    public enum menuType
    {
        OptionSelect,
        Skill,
        SpecialSkill,
        Item,
        Run,
        TargetSelectE,
        TargetSelectP,
        StatChk,
        nullType
    }

    public static menuType currentScreen;
	
	// Update is called once per frame
	void Update () {
        //checkMenuSize();
        if (!isThis2D)
        {
            move1D();
        }
        else
        {
            //move2D();
        }
        Debug.Log(currentScreen);
        selectItem();
	}

    void Awake()
    {
        if (mc == null)
        {
            DontDestroyOnLoad(gameObject);
            mc = this;
        }
        else if (mc != this)
        {
            Destroy(gameObject);
        }
    }

    public void move1D()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentPointer == 0)
            {
                currentPointer = menuSize;
                //If there are more items in the menu than are present, change the items presented on screen.
                //Example: if there are 4 items available and 3 is the max shown, then this moves to item 4, sets that as the bottom item, and 
                //turns max to 3, min to 1. 0 counts as an item, and this also makes it easier to pull the index in the array.
                if (menuSize > maxMenuItem)
                {
                    maxMenuItem = menuSize;
                    minMenuItem = menuSize - itemsDisplayed;
                }
                //Otherwise, just show what items exist here and jump to the position of the aforementioned index. It updates on frame, anyhow.
            }
            else
            {
                //Check if we're at the top of the menu, and update the options if we are.
                if (currentPointer == minMenuItem)
                {
                    minMenuItem--;
                    maxMenuItem--;
                }
                currentPointer--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPointer == menuSize)
            {
                currentPointer = 0;
                //As in the first half, just check the menu size and fiddle with this accordingly.
                if (menuSize == maxMenuItem)
                {
                    maxMenuItem = itemsDisplayed;
                    minMenuItem = 0;
                }
            }
            else
            {
                //Check if we're at the bottom of the menu, and update the options if we are.
                if (currentPointer == maxMenuItem)
                {
                    minMenuItem++;
                    maxMenuItem++;
                }
                currentPointer++;
            }
        }
            p.pPos = currentPointer - minMenuItem;
        //And finally, we update the pointer's Y coordinate to line up with that of the given index.
    }

    public void selectItem()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (currentScreen)
            {
                case menuType.OptionSelect:
                    Menu1Select(currentPointer);
                    break;
                    
            }
        }
    }

    //Base menu select function. This is the default screen when you initiate the fight.
    public void Menu1Select(int option)
    {
        switch (option)
        {
            //For quick reference: 0 is attack, 1 is skill, 2 is item, 3 is run, 4 is special/super.
            case 0:
                //Save action as Attack; move to enemy select.
                break;

            case 1:
                //Move to skill menu
                break;

            case 2:
                //Move to item menu
                break;

            case 3:
                //Attempt to flee encounter.
                break;

            case 4:
                //Check current player's super gauge, then either use it or return an error.
                break;
        }
    }
}
