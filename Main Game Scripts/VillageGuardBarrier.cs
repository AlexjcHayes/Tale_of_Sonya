using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGuardBarrier : MonoBehaviour
{
    public Animator anim;
    public GameObject AlertIcon;
    public Dialogue dialogue;
    GameObject gameManager;
    GameObject dialogueMan;
    GameManagement gameManage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            AlertIcon.GetComponent<SpriteRenderer>().enabled = true;
            gameManager = GameObject.Find("GameManager"); // finds the game manager game object
            gameManage = gameManager.GetComponent<GameManagement>(); // gets the GameManagement Script 
            gameManage.DialogueTextBox.SetActive(true); // enables the text box UI
            dialogueMan = GameObject.Find("Dialogue Manager");
            DialogueManager dialogueManager = dialogueMan.GetComponent<DialogueManager>();
            // dialogueManager.gameObject.SetActive(false); // this doesn't work
            // dialogueManager.gameObject.SetActive(true); // this doesn't work
            //print(dialogue);
            anim = gameManage.DialogueTextBox.GetComponent<Animator>();
            anim.SetBool("Dialogue_Box_Trigger", false);
            anim.SetBool("Dialogue_Box_Trigger", true);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //print("leaving");
            AlertIcon.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
