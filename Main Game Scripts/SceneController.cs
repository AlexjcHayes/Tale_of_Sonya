using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public string prevScene = "";
    public string currentScene = "";
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name; // gets current scene name
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name; // gets current scene name

    }
    public Vector3 scenePosition()
    {
            GameObject player = GameObject.Find("Player");
            Transform playerPosition = player.GetComponent<Transform>();
            if (prevScene == "Home Village House 1") // gets the previous scene places player at the door
            {
                Debug.Log("spawning player at new location"); // debug
                return new Vector3(11.20805f,-5.760141f,49.89551f); // coordinate of the home village house 1 door
            }else if (prevScene == "Home Village House 2") // gets the previous scene places player at the door
            {
                Debug.Log("spawning player at new location"); // debug
                return new Vector3(24.15116f,-5.760141f,49.89551f); // coordinate of the home village house 1 door
            }if (prevScene == "Home Village House 3") // gets the previous scene places player at the door
            {
                Debug.Log("spawning player at new location"); // debug
                return new Vector3(68.2438f,-1.71638f,49.89551f); // coordinate of the home village house 1 door
            }if (prevScene == "Home Village House 4") // gets the previous scene places player at the door
            {
                Debug.Log("spawning player at new location"); // debug
                return new Vector3(84.27806f,-1.716383f,49.89551f); // coordinate of the home village house 1 door
            }else{ // this returns a dummy vector which will be ignored so it doesn't place the player at an unwanted location
                return new Vector3(-1000,-1000,-1000);
            }
    }
}
