using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Experimental.Rendering.LWRP;   //OLD VERSIONS LIKE 2018
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS
public class Day_NightScript : MonoBehaviour
{

    private double lightColorCounter; // used for determing the mixture of day and night colors as game time increments
    public GameObject[] foundLights;
    public Color DayColor;
    public Color NightColor;

    public SpriteRenderer[] pallaraxLayer2Renderer; // layer 2 of the pallarax background
    public Color pallaraxDayColor; // color of the background during the day
    public Color pallaraxNightColor; // color of the background during the night
    public ParticleSystem starParticleSystem;

    GameObject gameManager;
    GameManagement gameManage;

    [HideInInspector]
    public static bool ambientchange = false;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        gameManage = gameManager.GetComponent<GameManagement>();
        foundLights = GameObject.FindGameObjectsWithTag("Time changing lights");
        if (SceneManager.GetActiveScene().name == "World Hub")
        {
            pallaraxLayer2Renderer = GameObject.Find("Layer_0009_2").GetComponentsInChildren<SpriteRenderer>();
        }
        StartCoroutine(dayNightTimer());

    }
    IEnumerator dayNightTimer() // coroutine function that handles the changing of the lights depending on the gameTime
    {

        while (true)
        {
            if (gameManage.ChangeTime) // bool to have time change
            {
                //gameManage.GameTimeCounter += 1;
                if (gameManage.GameTimeCounter >= gameManage.DayLength)
                {
                    gameManage.GameTimeCounter = 0;
                }
                lightColorCounter = Mathf.PingPong((float)gameManage.GameTimeCounter / (gameManage.DayLength / 2), 1); // increments and decrements the first argument (which is self incrementing) between 0 and the value of the second argument

                ///////////////  Color changing section \\\\\\\\\\\\\\\\
                if (foundLights.Length > 0) // scene lights
                { // handles when there are no lights configured
                    for (int i = 0; i < foundLights.Length; i++)
                    {
                        foundLights[i].GetComponent<Light2D>().color = Color.Lerp(NightColor, DayColor, (float)lightColorCounter);
                    }
                }
                if (SceneManager.GetActiveScene().name == "World Hub")
                {
                    
                    if (pallaraxLayer2Renderer.Length > 0)
                    { // layer 2 of pallarax background
                        for (int i = 0; i < pallaraxLayer2Renderer.Length; i++)
                        {
                            pallaraxLayer2Renderer[i].color = Color.Lerp(pallaraxNightColor, pallaraxDayColor, (float)lightColorCounter);
                        }
                    }

                    if (gameManage.GameTime == "Day" && ambientchange == false)
                    { // star particle system
                      //print("hdahsgkdsa");
                        starParticleSystem.Stop();
                        gameManage.audioManager.changeAmbientTrack("490846__burghrecords__birds-in-forest-scotland");
                        ambientchange = true;
                    }
                    else if (gameManage.GameTime == "Night" && ambientchange == true)
                    {
                        starParticleSystem.Play();
                        gameManage.audioManager.changeAmbientTrack("nightWildlife");
                        ambientchange = false;
                    }
                }
                //////////////////////////////////////////////////////////

                ////////////// Cycle speed handling \\\\\\\\\\\\\\\\\\\\\\\\\\
                if (gameManage.dayCycleSpeed != 0)
                {
                    yield return new WaitForSeconds(1F / gameManage.dayCycleSpeed);
                }
                else if (gameManage.dayCycleSpeed == 0)
                {
                    gameManage.dayCycleSpeed = 1;
                    yield return new WaitForSeconds(1F / gameManage.dayCycleSpeed);
                }
                ///////////////////////////////////////////////////////////////
            }
            yield return null;
        }
    }
}
