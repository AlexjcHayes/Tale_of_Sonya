using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuestEvent
{
    public enum EventStatus {WAITING, CURRENT, DONE};
    //WAITING - not yet completed but can't be worked on cause there's a prerequisite event
    //Current - the one the player should be trying to complete
    //Done - has been completed
    public string questName; // quest name
    public string description; // quest description
    public string id; // quest ID to identify the specific quest
    public GameObject[] DialogTriggers;
    //public bool questStatus; // boolean for checking the status of the quest Script
    public int questOrder = -1; // the order # of the quest
    public EventStatus status;
    //public QuestButton button; // add this later
    public List<QuestPath> pathlist = new List<QuestPath>();
    
    public QuestEvent(string n, string d)// "n"= name, "d"=description
    {
        id = Guid.NewGuid().ToString();
        questName=n;
        description=d;
        status=EventStatus.WAITING;
    }

    public void UpdateQuestEvent(EventStatus es){
        status= es;
        //button.UpdateButton(es);
    }
    public string GetQuestId(){
        return id;
    }
}
