using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour {

    //What character is performing the action?
    public Character user;

    //And which character is the target?
    public Character target;

    //Are they attacking? Defending? Using an item?
    public enum actionType
    {
        attack,
        defend,
        item,
        skill,
        special,
        run,
        notChosen
    }

    public actionType selectedAction = actionType.notChosen;

    //If it's an item or skill, what is its ID?
    public int itemID, skillID;
    public bool choseSkill = false;
    public bool choseItem = false;

    //What level of priority does the action have?
    public int priorityLevel;

    //And just to make sure, what's the spd of the user?
    public int cSpd;

    //And finally, is the queue list complete?
    public bool moveToNext = false;


    public void resetQueue()
    {
        selectedAction = actionType.notChosen;
        itemID = -1;
        skillID = -1;
        choseItem = false;
        choseSkill = false;
        priorityLevel = 0;
        moveToNext = false;
        cSpd = 0;
    }
}
