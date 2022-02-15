using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest2 : MonoBehaviour
{
    public QuestManager qManager;
    public QuestEvent qEvent;
    public GameObject[] dialogTriggers;
    GameObject gameManager;
    GameManagement gameManage;
    public GameObject GuardWall;
    public bool questCompleted = false; // boolean to check if the requrements of the quest has been made

    bool stopRunning = false; // to stop running the script if the quest has been completed
    //public QuestButton qButton; // add quest button
    public void Setup(QuestManager qm, QuestEvent qe) // add "QuestButton qb" later
    { // add "QuestButton qb" later after adding button
        gameManager = GameObject.Find("GameManager"); // finds the game manager game object
        gameManage = gameManager.GetComponent<GameManagement>(); // gets the GameManagement Script 
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
        //Debug.Log(qManager.dialogTriggerNames.Contains("Diag_3"));
        if (qManager.dialogTriggerNames.Contains("Diag_3"))
        {
            //print(" YAYAYAYAYAYAYAY");
            gameManage.ChangeTime = true;
        }
        if (gameManage.GameTime == "Night" && SceneManager.GetActiveScene().name == "World Hub"&&qEvent.status.ToString()=="CURRENT")
        {
            GuardWall=GameObject.Find("Guard Wall");
            GuardWall.GetComponent<BoxCollider2D>().enabled = false; // turns of the collider to allow the player to leave the village
        }else if(gameManage.GameTime == "Day" && SceneManager.GetActiveScene().name == "World Hub"&&qEvent.status.ToString()=="CURRENT"){
            GuardWall=GameObject.Find("Guard Wall");
            GuardWall.GetComponent<BoxCollider2D>().enabled = true; // turns of the collider to allow the player to leave the village
        }
        // insert code here for the actual quest
        // if (Input.GetKey(KeyCode.P))
        // {
        //     questCompleted = true;
        // }



        ////////////////////////////////////////
        if (questCompleted) // if the quest is completed, update the status of the quests and set new active quest
        {
            //Debug.Log("This is working bbb");
            qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE); // set the current status of this quest as done
            //qButton.UpdateButton(QuestEvent.EventStatus.DONE); add this when button is made
            qManager.UpdateQuestsOnCompletion(qEvent);
            qManager.quest.printQuestPath(); // debug
            stopRunning = true;
        }
    }
}
