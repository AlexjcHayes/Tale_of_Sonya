using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool boundries_set; // boolean to set the bondries in which the camera can follow the playerTrans
    public Transform cameraObject;
    public int X_offset;
    public int Y_offset;
    public float aheadAmount, aheadSpeed;
    public float yDistanceConstraint = -5;
    public float boundry_start_x;// start x position for boundry setting
    public float boundry_stop_x; // stop x position for boundry setting
    public float move_offset = 0; // offset for when the camera should move it's y position with respect to the player
    //public float lookAhead;
    //public float lookAheadSpeed;
    GameObject is_player;
    Player_movement Player;
    void Start()
    {
        is_player = GameObject.Find("Player"); // finds the Game manager Object
        Player = is_player.GetComponent<Player_movement>(); // Gets the scene controllerscript
    }
    void Update()
    {
        if (!boundries_set)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                cameraObject.localPosition = new Vector3(Mathf.Lerp(cameraObject.localPosition.x, aheadAmount * -Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), cameraObject.localPosition.y, cameraObject.localPosition.z);
                //Debug.Log(Mathf.Lerp(cameraObject.localPosition.x, aheadAmount * Mathf.Abs(Input.GetAxisRaw("Horizontal")), aheadSpeed * Time.deltaTime));
            }
            transform.position = new Vector3(cameraObject.position.x, transform.position.y, transform.position.z); // this needs to be outside of the if statement
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                cameraObject.localPosition = new Vector3(Mathf.Lerp(cameraObject.localPosition.x, aheadAmount * -Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), cameraObject.localPosition.y, cameraObject.localPosition.z);
                //Debug.Log(Mathf.Lerp(cameraObject.localPosition.x, aheadAmount * Mathf.Abs(Input.GetAxisRaw("Horizontal")), aheadSpeed * Time.deltaTime));
            }
            transform.position = new Vector3(Mathf.Clamp(cameraObject.position.x,boundry_start_x,boundry_stop_x), transform.position.y, transform.position.z); // this needs to be outside of the if statement
        }
    }

}
