using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMenuController : MonoBehaviour {
    public static FightMenuController mc;

    //In order: Determines current menu value, last menu value, amount of items in the set, and how many items will be displayed.
    public int currentPointer, lastPointer, menuSize, itemsDisplayed;

    //Affected by menuSize, this determines which menu options are available.
    public int maxMenuItem, minMenuItem;

    //See if the menu is a 1d or 2d array
    public bool isThis2D;

    //Is this a normal attack? Skill? Item? Memorize the value thus. This compiles all actions from both sides. Later, we'll have a function to reorder the queue based on speed/priority of the fighters.
    public ActionQueue[] toDoCharacters = new ActionQueue[10];
    public ActionQueue currentQueue;
    public int currentPlayer;

    //How many enemies on the field? Players? We'll move this to the cross-map controller later. At max, both are 5/5.
    public int enemyCount = 1;
    public int playerCount = 5;

    //Has the initialization function been run?
    public bool initFin;

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

    public menuType currentScreen, lastScreen;


    // Update is called once per frame
    void Update () {
        if (!initFin)
        {
            enableDisableChars();
            initFin = !initFin;
        }
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
                    OptMenuSelect(currentPointer);
                    break;

                case menuType.TargetSelectE:
                    ETargetSelect(currentPointer);
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentScreen != menuType.OptionSelect)
            {
                switch (lastScreen)
                {
                    case menuType.OptionSelect:
                        currentScreen = menuType.OptionSelect;
                        currentQueue.selectedAction = ActionQueue.actionType.notChosen;
                        changeMaxMin(0, 5, 5, 5);
                        ArrayHolder.puppeteer.pointers[1].gameObject.SetActive(false);
                        ArrayHolder.puppeteer.pointers[0].gameObject.SetActive(true);
                        p = ArrayHolder.puppeteer.pointers[0];
                        break;

                    case menuType.TargetSelectE:
                        currentScreen = menuType.TargetSelectE;
                        changeMaxMin(0, enemyCount, enemyCount, enemyCount);
                        currentPointer = lastPointer;
                        currentQueue.target = null;
                        ArrayHolder.puppeteer.pointers[0].gameObject.SetActive(false);
                        ArrayHolder.puppeteer.pointers[1].gameObject.SetActive(true);
                        if (currentQueue.choseItem)
                        {
                            currentQueue.choseItem = false;
                            lastScreen = menuType.Item;
                            //Insert shenanigans for finding the exact menu value based on what the item was.
                            //lastPointer = blah
                            //changeMaxMin(blah)
                        }
                        else if (currentQueue.choseSkill)
                        {
                            currentQueue.choseSkill = false;
                            lastScreen = menuType.Skill;
                            //Insert shenanigans for finding the exact menu value based on skill/player.
                            //lastPointer = blah
                            //changeMaxMin(blah)
                        }
                        else
                        {
                            Debug.Log("Did it.");
                            lastScreen = menuType.OptionSelect;
                            lastPointer = 0;
                        }
                        break;
                }
            }
            else
            {
                prevChar();
            }
        }
    }

    //Base menu select function. This is the default screen when you initiate the fight.
    public void OptMenuSelect(int option)
    {
        lastPointer = currentPointer;
        switch (option)
        {
            //For quick reference: 0 is attack, 1 is defend, 2 is skill, 3 is item, 4 is run, 5 is special/super.
            case 0:
                //Save action as Attack; move to enemy select.
                currentQueue.selectedAction = ActionQueue.actionType.attack;
                //Fancy menu-ing stuff here.
                ArrayHolder.puppeteer.pointers[0].gameObject.SetActive(false);
                ArrayHolder.puppeteer.pointers[1].gameObject.SetActive(true);
                p = ArrayHolder.puppeteer.pointers[1];
                lastScreen = menuType.OptionSelect;
                currentScreen = menuType.TargetSelectE;
                break;

            case 1:
                //Save action as defend; move to next player.
                currentQueue.selectedAction = ActionQueue.actionType.defend;
                nextChar();
                break;

            case 2:
                //Move to skill menu
                break;

            case 3:
                //Move to item menu
                break;

            case 4:
                //Attempt to flee encounter.
                break;

            case 5:
                //Check current player's super gauge, then either use it or return an error.
                break;
        }
    }

    public void ETargetSelect(int target)
    {
        //'Target' determines the poor sap getting blasted by the person today.
        currentQueue.target = ArrayHolder.puppeteer.enemyList[target];
        //Fancy menu-ing stuff here.
        ArrayHolder.puppeteer.pointers[1].gameObject.SetActive(false);
        ArrayHolder.puppeteer.pointers[0].gameObject.SetActive(true);
        p = ArrayHolder.puppeteer.pointers[0];
        nextChar();
        //This sort of thing is always the last thing in the queue
    }

    
    public void changeMaxMin(int newMin, int newMax, int newMenuSize, int newItemsShown)
    {
        minMenuItem = newMin;
        maxMenuItem = newMax;
        menuSize = newMenuSize;
        itemsDisplayed = newItemsShown;
    }

    public void enableDisableChars()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < playerCount)
            {
                ArrayHolder.puppeteer.pcList[i].gameObject.SetActive(true);
            }
            else
            {
                ArrayHolder.puppeteer.pcList[i].gameObject.SetActive(false);
            }
            if (i < enemyCount)
            {
                ArrayHolder.puppeteer.enemyList[i].gameObject.SetActive(true);
            }
            else
            {
                ArrayHolder.puppeteer.enemyList[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (i < playerCount + enemyCount)
            {
                toDoCharacters[i].gameObject.SetActive(true);
            }
            else
            {
                toDoCharacters[i].gameObject.SetActive(false);
            }
        }
    }

    void prepNextTurn()
    {
        for (int i = 0; i < playerCount + enemyCount; i++)
        {
            toDoCharacters[i].resetQueue();
        }
        for (int i = 0; i < ArrayHolder.puppeteer.pointers.Length; i++)
        {
            ArrayHolder.puppeteer.pointers[i].pPos = 0;
        }
        currentScreen = menuType.OptionSelect;
        changeMaxMin(0, 5, 5, 5);
        currentPointer = 0;
    }

    void nextChar()
    {
        if (currentPlayer < playerCount)
        {
            //Update player name (wherever that's shown)
            currentPlayer++;
            currentQueue = toDoCharacters[currentPlayer];
            lastScreen = currentScreen;
            currentScreen = menuType.OptionSelect;
            changeMaxMin(0, 5, 5, 5);
            lastPointer = currentPointer;
            currentPointer = 0;
        }
        //Else, calc for enemies and run shit.
    }

    void prevChar()
    {
        if (currentPlayer > 0)
        {
            currentPlayer--;
            currentQueue = toDoCharacters[currentPlayer];
            currentScreen = lastScreen;
            currentPointer = lastPointer;
            if (currentQueue.selectedAction == ActionQueue.actionType.attack)
            {
                currentScreen = menuType.TargetSelectE;
                changeMaxMin(0, enemyCount, enemyCount, enemyCount);
                currentQueue.target = null;
                ArrayHolder.puppeteer.pointers[0].gameObject.SetActive(false);
                ArrayHolder.puppeteer.pointers[1].gameObject.SetActive(true);
                lastScreen = menuType.OptionSelect;
                lastPointer = 0;
            }
            else if (currentQueue.choseItem)
            {
                //Parse the type of item (target enemy or ally), then act accordingly.
            }
            else if (currentQueue.choseSkill)
            {
                //Likewise.
            }
        }
        else
        {
            currentPointer = 0;
            lastPointer = 0;
        }
    }
}
