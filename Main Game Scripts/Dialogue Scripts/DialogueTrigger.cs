using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Animator anim;
    public Animator DialogueIconOpen;
    public Animator DialogueIconClose;
    public Dialogue dialogue;
    public bool npcTriggered; // trigger for if it's attached to an npc
    GameObject gameManager;
    GameObject dialogueMan;
    GameManagement gameManage;
    public GameObject DialogueIcon; // refrence to the icon script for displaying the dialogue icon

    public bool dialogueTriggered = false; // boolean for when the dialogue has already been triggered

    bool showIcons = true; // bool to continue to show the quest Icons
    void Update()
    {
        if (gameObject.GetComponent<BoxCollider2D>().enabled == false)
        {
            if (DialogueIconOpen != null && DialogueIconClose != null)
            {
                DialogueIconOpen.GetComponent<SpriteRenderer>().enabled = false;
                DialogueIconClose.GetComponent<SpriteRenderer>().enabled = false;
            }

        }
        else
        {
            if (DialogueIconOpen != null && DialogueIconClose != null)
            {
                DialogueIconOpen.GetComponent<SpriteRenderer>().enabled = true;
                DialogueIconClose.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void OnTriggerEnter2D(Collider2D collision)
    { // triggers if the player collides with the trigger box (must have the trigger bool enabled for the collider)
      // DialogueIcon.GetComponent<DialogueIcon>().iconTrigger = false;
        if (showIcons)
        {
            if (DialogueIconClose != null && DialogueIconOpen != null)
            {
                DialogueIconClose.SetBool("Dialogue_Icon_Close_Bool", false);
                DialogueIconOpen.SetBool("Dialogue_Icon_Open_Bool", true);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !dialogueTriggered)
        {
            if (npcTriggered)
            {
                DialogueIconOpen.SetBool("Dialogue_Icon_Open_Bool", true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    gameManager = GameObject.Find("GameManager"); // finds the game manager game object
                    gameManage = gameManager.GetComponent<GameManagement>(); // gets the GameManagement Script 
                    gameManage.DialogueTextBox.SetActive(true); // enables the text box UI
                    dialogueMan = GameObject.Find("Dialogue Manager");
                    DialogueManager dialogueManager = dialogueMan.GetComponent<DialogueManager>();
                    // dialogueManager.gameObject.SetActive(false); // this doesn't work
                    // dialogueManager.gameObject.SetActive(true); // this doesn't work
                    anim = gameManage.DialogueTextBox.GetComponent<Animator>();
                    anim.SetBool("Dialogue_Box_Trigger", false);
                    anim.SetBool("Dialogue_Box_Trigger", true);
                    // foreach(string sentence in dialogue.sentences){
                    //     print(sentence);
                    // }
                    for (int i = 0; i < dialogue.sentences.Length; i++) // might not need this
                    {
                        dialogue.sentences[i] = dialogue.sentences[i].ToString();
                    }
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                    DialogueIconOpen.SetBool("Dialogue_Icon_Open_Bool", false);
                    DialogueIconClose.SetBool("Dialogue_Icon_Close_Bool", false);
                    showIcons = false;
                    GameObject qManagement = GameObject.Find("QuestHandler");
                    QuestManager qManager = qManagement.GetComponent<QuestManager>();
                    qManager.dialogTriggerNames.Add(gameObject.name); // adds the object this trigger is attached to make sure it doesn't reactivate again 
                                                                      //gameObject.SetActive(false); // turns off the game Object that it's attached to
                    dialogueTriggered = true;
                }

            }
            else if (!npcTriggered)
            {
                gameManager = GameObject.Find("GameManager"); // finds the game manager game object
                gameManage = gameManager.GetComponent<GameManagement>(); // gets the GameManagement Script 
                gameManage.DialogueTextBox.SetActive(true); // enables the text box UI
                dialogueMan = GameObject.Find("Dialogue Manager");
                DialogueManager dialogueManager = dialogueMan.GetComponent<DialogueManager>();
                // dialogueManager.gameObject.SetActive(false); // this doesn't work
                // dialogueManager.gameObject.SetActive(true); // this doesn't work
                anim = gameManage.DialogueTextBox.GetComponent<Animator>();
                anim.SetBool("Dialogue_Box_Trigger", false);
                anim.SetBool("Dialogue_Box_Trigger", true);

                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                GameObject qManagement = GameObject.Find("QuestHandler");
                QuestManager qManager = qManagement.GetComponent<QuestManager>();
                qManager.dialogTriggerNames.Add(gameObject.name); // adds the object this trigger is attached to make sure it doesn't reactivate again 
                                                                  //gameObject.SetActive(false); // turns off the game Object that it's attached to
                dialogueTriggered = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (showIcons)
        {
            if (DialogueIconClose != null && DialogueIconOpen != null)
            {
                DialogueIconOpen.SetBool("Dialogue_Icon_Open_Bool", false);
                DialogueIconClose.SetBool("Dialogue_Icon_Close_Bool", true);
            }
        }
    }
}


