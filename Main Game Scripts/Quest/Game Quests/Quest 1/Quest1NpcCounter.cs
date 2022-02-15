using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1NpcCounter : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public GameObject questFind;
    Quest1 quest1;
    bool triggered = false;

    void Setup()
    {

        
    }
    // Update is called once per frame
    void Update()
    {
        questFind = GameObject.Find("Quest1");
        //print("Questfind: "+ questFind);
        quest1=questFind.GetComponent<Quest1>();
        if (dialogueTrigger.dialogueTriggered && !triggered)
        {
            quest1.npcCounter+=1;
            triggered = true;
        }
    }
}
