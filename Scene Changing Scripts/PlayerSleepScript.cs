using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSleepScript : MonoBehaviour
{
    public bool canSleep; // might need this later for restricting the player to move to the next scene
    public bool popupBoxBool; // used to cue the animation for the pop up box
    private bool sleeping=false; // used to handle so that the player can't spam sleeping (so they only sleep once and have to leave the bed before sleeping again)

    void Start()
    {

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        GameObject GameManagement = GameObject.Find("GameManager"); // finds the Game manager Object
        GameManagement gm = GameManagement.GetComponent<GameManagement>(); // Gets the scene controllerscript
        if (collision.gameObject.name == "Player" && canSleep)
        {
            popupBoxBool = true;

            if (Input.GetKey("e")&&sleeping==false)
            {
                //Debug.Log("working");
                if(gm.GameTime=="Day"){
                    gm.GameTimeCounter=1180; 
                    Day_NightScript.ambientchange=true;
                }else if(gm.GameTime=="Night"){
                    gm.GameTimeCounter=600; // 10 AM
                    Day_NightScript.ambientchange=false;
                }
                sleeping=true;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && canSleep)
        {
            popupBoxBool = false;
            sleeping=false;
        }

    }
}
