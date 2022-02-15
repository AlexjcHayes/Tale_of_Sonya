using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public QuestManager qManager;
    public QuestEvent qEvent;

    public GameObject[] dialogTriggers;
    public int npcCounter = 0;
    public bool questCompleted = false; // boolean to check if the requrements of the quest has been made

    bool stopRunning = false; // to stop running the script if the quest has been completed
    //public QuestButton qButton; // add quest button
    public void Setup(QuestManager qm, QuestEvent qe) // add "QuestButton qb" later
    { // add "QuestButton qb" later after adding button
        qManager = qm;
        qEvent = qe;
        //dialogTriggers=GameObject.FindGameObjectsWithTag("Quest 1");
        //qButton=qb;
        ////setup link between event and button
        //qe.button = qButton; // add this later
    }

    void Update()
    {
        if (!stopRunning)
        {
            questGoal();
        }
        //print(npcCounter);  // debug
    }

    void questGoal()
    {
        if (npcCounter == 4)
        {
            questCompleted = true;
        }

        // insert code here for the actual quest
        if (Input.GetKey(KeyCode.P))
        {
            questCompleted = true;
        }



        ////////////////////////////////////////
        if (questCompleted) // if the quest is completed, update the status of the quests and set new active quest
        {
            //Debug.Log("This is working  aaa");
            qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
            //qButton.UpdateButton(QuestEvent.EventStatus.DONE); add this when button is made
            qManager.UpdateQuestsOnCompletion(qEvent);
            qManager.quest.printQuestPath(); // debug
            stopRunning=true;
        }
    }
}
