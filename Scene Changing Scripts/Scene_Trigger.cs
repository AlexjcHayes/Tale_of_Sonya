using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Trigger : MonoBehaviour
{
    public int next_Scene_To_Load;

    public bool can_change_scene; // might need this later for restricting the player to move to the next scene
    public bool popupBoxBool; // used to cue the animation for the pop up box
    private bool toggleDebounce = false; // to keep from constantly switching scenes too fast

    void Start()
    {

    }
    void Update()
    {

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        GameObject GameManagement = GameObject.Find("GameManager"); // finds the Game manager Object
        GameManagement gameManage;
        SceneController SceneControllerScript = GameManagement.GetComponent<SceneController>(); // Gets the scene controllerscript
        Animator anim;
        Animator fadeAnim;

        if (collision.gameObject.name == "Player" && can_change_scene)
        {
            popupBoxBool = true;

            if (Input.GetKey("e") == false)
            {
                toggleDebounce = true;
            }
            if (Input.GetKey("e") && toggleDebounce)
            {
                gameManage = GameManagement.GetComponent<GameManagement>(); // gets the GameManagement Script 
                anim = gameManage.DialogueTextBox.GetComponent<Animator>();
                fadeAnim = gameManage.FadeController.GetComponent<Animator>();
                // Quest Dialogue Triggers
                GameObject qManagement = GameObject.Find("QuestHandler");
                QuestManager qManager = qManagement.GetComponent<QuestManager>();
                qManager.DisableQuestDiagTriggers(); // disables the triggers in the scene
                ////////////
                anim.SetBool("Dialogue_Box_Trigger", false); // turns off of dialog box when the player leaves a room
                SceneControllerScript.prevScene = SceneManager.GetActiveScene().name;//SceneControllerScript.currentScene; // sets the current scene as the previous for keeping where the player transistion from
                SceneControllerScript.currentScene = ""; //  resets the current scene before being reninitialized when the next scene loads
                fadeAnim.SetBool("Fade_Controller_Bool", true);
                SceneManager.LoadScene(next_Scene_To_Load);

            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && can_change_scene)
        {
            popupBoxBool = false;
        }

    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        GameObject GameManagement = GameObject.Find("GameManager"); // finds the Game manager Object
        GameManagement gameManage;
        SceneController SceneControllerScript = GameManagement.GetComponent<SceneController>(); // Gets the scene controllerscript
        Animator anim;
        Animator fadeAnim;
        if (collision.gameObject.name == "Player" && can_change_scene)
        {
            popupBoxBool = true;

            // if (Input.GetKey("e"))
            // {
            gameManage = GameManagement.GetComponent<GameManagement>(); // gets the GameManagement Script 
            anim = gameManage.DialogueTextBox.GetComponent<Animator>();
            fadeAnim = gameManage.FadeController.GetComponent<Animator>();
            // Quest Dialogue Triggers
            GameObject qManagement = GameObject.Find("QuestHandler");
            QuestManager qManager = qManagement.GetComponent<QuestManager>();
            qManager.DisableQuestDiagTriggers(); // disables the triggers in the scene
                                                 ////////////
            anim.SetBool("Dialogue_Box_Trigger", false); // turns off of dialog box when the player leaves a room
            SceneControllerScript.prevScene = SceneManager.GetActiveScene().name;//SceneControllerScript.currentScene; // sets the current scene as the previous for keeping where the player transistion from
            SceneControllerScript.currentScene = ""; //  resets the current scene before being reninitialized when the next scene loads
            fadeAnim.SetBool("Fade_Controller_Bool", true);
            SceneManager.LoadScene(next_Scene_To_Load);

            //}
        }
    }


    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && can_change_scene)
        {
            popupBoxBool = false;
        }
    }

}
