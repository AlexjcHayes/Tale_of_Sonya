using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestManager : MonoBehaviour
{
    //////////////////////////////// Quest Scripts \\\\\\\\\\\\\\\\\\\\\\\
    GameObject questObject;
    Quest1 quest1;
    Quest2 quest2;





    //public Type parentType = typeof(QuestsParent);
    //Assembly asssembly = AssemblyIsEditorAssembly.getExecutingAssembly();
    //////////////////////////////////////////////////////////////////////
    public Quest quest = new Quest();

    public GameObject[] dialogTriggers;

    public List<string> dialogTriggerNames = new List<string>(); // names of the triggers to check if they've been triggered once

    public bool updateDiagTriggers = false;

    void Start()
    {

        questObject = GameObject.Find("Quests"); // finds the Game manager Object
        // create each  Quest event
        QuestEvent a = quest.AddQuestEvent("Quest 1", "The Introduction");
        quest1 = questObject.GetComponentInChildren<Quest1>();
        quest1.Setup(this, a);
        //Debug.Log(a.questScript);
        //Debug.Log(scriptTest.questCompleted);
        QuestEvent b = quest.AddQuestEvent("Quest 2", "Discover the sacred forest");
        quest2 = questObject.GetComponentInChildren<Quest2>();
        quest2.Setup(this, b);
        // QuestEvent c = quest.AddQuestEvent("test3", "description 3");
        // QuestEvent d = quest.AddQuestEvent("test4", "description 4");
        // QuestEvent e = quest.AddQuestEvent("test5", "description 5");

        // define the paths between the events - e.g. the order they must be completed
        // this is an example of a parallel tree where you can either do quest "C" or "D" in order to do quest "E"
        quest.AddPath(a.GetQuestId(), b.GetQuestId());
        // quest.AddPath(b.GetQuestId(), c.GetQuestId());
        // quest.AddPath(b.GetQuestId(), d.GetQuestId());
        // quest.AddPath(c.GetQuestId(), e.GetQuestId());
        // quest.AddPath(d.GetQuestId(), e.GetQuestId());
        quest.BFS(a.GetQuestId());
        questSetup();
        //findQuestDiagTriggers(); // finds dialogue triggers in the scene
        quest.printQuestPath();
    }
    void Update()
    {
        if (updateDiagTriggers)
        {
            findQuestDiagTriggers();
            updateDiagTriggers = false;
        }
    }

    public void findQuestDiagTriggers()
    {
        for (int i = 0; i < quest.questEvents.Count; i++)
        {
            if (quest.questEvents[i].status.ToString() == "CURRENT")
            {
                int tag = i + 1;
                dialogTriggers = GameObject.FindGameObjectsWithTag((string)("Quest " + tag));
                //Debug.Log(dialogTriggers[0]); // debug
                for (int j = 0; j < dialogTriggers.Length; j++)
                {
                    //Debug.Log(dialogTriggers[j].GetComponent<DialogueTrigger>().dialogueTriggered);
                    //print(dialogTriggerNames.Contains(dialogTriggers[j].name));
                    //print("List: "+dialogTriggerNames.Count+ "Contains trigger in current Scene: "+dialogTriggerNames.Contains(dialogTriggers[j].name));
                    if (!dialogTriggerNames.Contains(dialogTriggers[j].name))
                    {
                        dialogTriggers[j].gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
            }
        }
    }

    public void DisableQuestDiagTriggers()
    {
        //DialogueTrigger dt=gameObject.GetComponent<DialogueTrigger>();
        for (int j = 0; j < dialogTriggers.Length; j++)
        {
            //Debug.Log(dialogTriggers[j].GetComponent<DialogueTrigger>().dialogueTriggered);
            if (dialogTriggers[j].GetComponent<DialogueTrigger>().dialogueTriggered == true)
            {
                dialogTriggers[j].gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    void questSetup()  // might need to change this later
    {
        foreach (QuestEvent n in quest.questEvents)
        {
            if (n.questOrder == 1 && n.status.ToString() == "WAITING")
            {
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
            }
        }
    }

    public void UpdateQuestsOnCompletion(QuestEvent e)// pass in the event of the quest
    {
        foreach (QuestEvent n in quest.questEvents)
        {
            //if this event is the next in order
            if (n.questOrder == (e.questOrder + 1))
            {
                //make the next in line available for completion
                //print("reseting list");
                dialogTriggerNames.Clear(); // clears the list before adding new triggers for the next quest
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
                findQuestDiagTriggers();

            }
        }
    }
}
