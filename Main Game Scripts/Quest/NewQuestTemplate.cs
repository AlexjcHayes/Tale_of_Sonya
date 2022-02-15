using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewQuestTemplate : MonoBehaviour
{
    GameObject gameManager;
    QuestManager questManage;
    public QuestManager qManager;
    public QuestEvent qEvent;

    public bool questCompleted = false; // boolean to check if the requrements of the quest has been made

    //public QuestButton qButton; // add quest button
    public void Setup(QuestManager qm, QuestEvent qe) // add "QuestButton qb" later
    { // add "QuestButton qb" later after adding button
        gameManager = GameObject.Find("GameManager");
        questManage = gameManager.GetComponent<QuestManager>();
        //questManage.questScriptsList.Add(this);
        qManager = qm;
        qEvent = qe;
        //qButton=qb;
        ////setup link between event and button
        //qe.button = qButton; // add this later
    }

    void Update()
    {
        Debug.Log("hagdhsaghdsahgsdah");
        questGoal();
    }

    void questGoal()
    {
        // insert code here for the actual quest
        if (Input.GetKey(KeyCode.P))
        {
            questCompleted = true;
        }



        ////////////////////////////////////////
        if (questCompleted) // if the quest is completed, update the status of the quests and set new active quest
        {
            Debug.Log("This is working");
            qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
            //qButton.UpdateButton(QuestEvent.EventStatus.DONE); add this when button is made
            qManager.UpdateQuestsOnCompletion(qEvent);
            qManager.quest.printQuestPath(); // debug
        }
    }




}
