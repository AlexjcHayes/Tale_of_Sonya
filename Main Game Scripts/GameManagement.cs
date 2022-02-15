using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{

    //////////// Dealing with game time \\\\\\\\\\\\\\
    public bool ChangeTime = true;
    public string GameTime; // string for if it is day or night
    public float DayLength = 1440; // length of a day in the game
    public int dayCycleSpeed = 1; // cycle speed for how fast time is changing
    public double GameTimeCounter; // current time in the game
    public int hours;
    public int minutes;

    //////////////////////////////////////////////////

    //////////// Dealing with game UI \\\\\\\\\\\\\\
    public GameObject UI;
    public Slider slider;
    public RectTransform TimePiece;
    public GameObject DialogueTextBox;
    public GameObject FadeController;
    public GameObject PauseScreen;
    private double clockAngle;
    public float playerMaxHealth = 100; // maximum health of the player *
    public float playerHealth = 100; // current heatlh of the player *

    [HideInInspector]
    public int healthIndicatorState = 5; // Values between 1-5 (5 being high health and 1 being no health left)
    [HideInInspector]
    public bool GamePaused = false;
    /////////////////////////////////////////

    //////////////// Game Ambient Audio \\\\\\\\\\\\
    public AudioManager audioManager;
    private bool playMusic = false;
    private bool playAmbient = false;
    ////////////////////////////////////////

    ///////////// Weather \\\\\\\\\\\\\\\\\\\
    [HideInInspector]
    public bool is_raining; // *
    public GameObject[] RainSystem;
    /////////////////////////////////////////

    public static GameManagement Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dayNightCounter());
    }

    // Update is called once per frame
    void Update()
    {
        weatherController();
        healthManagement();
        if (Input.GetKey(KeyCode.R) && playerHealth > 0)
        {
            TakeDamage(1);
        }
        if (Input.GetKey(KeyCode.T))
        {
            playerHealth = playerMaxHealth;
        }
        if (Input.GetButtonUp("Cancel"))
        {
            GamePaused = !GamePaused;
        }
        if (GamePaused)
        {  // if the game is paused
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1; // if the game is not paused
            PauseScreen.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "World Hub")
        {
            UI.SetActive(true);
            if (!playAmbient)
            {
                //print("playing ambient"); //debug

                audioManager.StopCoroutine(audioManager.ambientFadeOut());
                audioManager.StartCoroutine(audioManager.ambientFadeIn());
                audioManager.ambientSource.Play();
                //ambientSound.GetComponent<AudioSource>().Play();////
                playAmbient = true;
            }
            if (!playMusic)
            {
                //print(audioManager.musicSource.clip.name == "");
                if (audioManager.musicSource.clip == null) // need to check this first
                {
                    //print("changing music"); // debug
                    audioManager.changeMusicTrack("frozen-star-by-kevin-macleod");
                }
                else if (audioManager.musicSource.clip != null)
                {
                    if (audioManager.musicSource.clip.name != "frozen-star-by-kevin-macleod")
                    {
                        //print("changing music"); // debug
                        audioManager.changeMusicTrack("frozen-star-by-kevin-macleod");
                    }
                }
                
                //print("playing music"); //debug

                audioManager.StopCoroutine(audioManager.musicFadeOut());
                audioManager.StartCoroutine(audioManager.musicFadeIn());
                audioManager.musicSource.Play();
                //ambientSound.GetComponent<AudioSource>().Play();////
                playMusic = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Main Menu") // for if you're in the main menu
        {
            //print("not playing");
            UI.SetActive(false);
            audioManager.StopCoroutine(audioManager.ambientFadeIn());
            audioManager.StartCoroutine(audioManager.ambientFadeOut());
            playAmbient = false;
            playMusic = false;
            Destroy(gameObject); // gets rid of the game manager (technically resets the game)
        }
        else // for any other cases
        {
            //print("not playing");  // debug
            ///UI.SetActive(false);
            audioManager.StopCoroutine(audioManager.ambientFadeIn());
            audioManager.StartCoroutine(audioManager.ambientFadeOut());

            playAmbient = false;
            //playMusic = false;
        }
    }

    public double ConvertToDegrees(double radians)
    {
        return (180 / Mathf.PI) * radians;
    }

    IEnumerator dayNightCounter()
    {
        while (true)
        {
            if (ChangeTime) // bool to have time change
            {
                GameTimeCounter += 1;

                //////////////////////////// Time Clock Handle \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                clockAngle = ((GameTimeCounter - 360) * .25) % 360;
                float rotationOffset = (float)ConvertToDegrees((360 / (DayLength / 8)));
                TimePiece.eulerAngles = new Vector3(0, 0, (float)clockAngle + 270);
                ////////////////////////////



                hours = (int)(GameTimeCounter / 60);
                minutes = (int)GameTimeCounter % 60;

                if (hours >= 6 && hours < 18)
                {
                    GameTime = "Day";
                }
                else
                {
                    GameTime = "Night";
                }

                if (dayCycleSpeed != 0)
                {
                    yield return new WaitForSeconds(1F / dayCycleSpeed);
                }
                else if (dayCycleSpeed == 0)
                {
                    dayCycleSpeed = 1;
                    yield return new WaitForSeconds(1F / dayCycleSpeed);
                }
            }
            yield return null;
        }

    }


    ///////// PLayer functions 
    void TakeDamage(int damage)
    {
        playerHealth -= damage;
    }
    void healthManagement()
    {
        slider.maxValue = playerMaxHealth;
        slider.value = playerHealth;

        //// health indicator handling 
        if (playerHealth >= (.8 * playerMaxHealth))
        {
            healthIndicatorState = 5;
        }
        else if ((playerHealth >= (.5 * playerMaxHealth)) && (playerHealth < (.8 * playerMaxHealth)))
        {
            healthIndicatorState = 4;
        }
        else if ((playerHealth >= (.2 * playerMaxHealth)) && (playerHealth < (.5 * playerMaxHealth)))
        {
            healthIndicatorState = 3;
        }
        else if ((playerHealth > 0) && (playerHealth < (.2 * playerMaxHealth)))
        {
            healthIndicatorState = 2;
        }
        else if (playerHealth == 0) // dead
        {
            healthIndicatorState = 1;
        }

    }

    ///////// Weather Functions
    void weatherController()
    {
        RainSystem = GameObject.FindGameObjectsWithTag("Rain System");
        //Debug.Log(RainSystem); // debug
        if (GameTimeCounter == 360)
        {
            if (RainSystem.Length > 0)
            {
                AudioSource rainSFX = RainSystem[0].GetComponent<AudioSource>();
                //float rainSFXVolume=rainSFX.volume;
                float chance = Random.Range(0f, 10f);
                if (chance < 4)
                { // 40% chance of rain
                    float rainingLength = Random.Range(600, 1440); // random tick to turn off the rain
                    //print(rainingLength);
                    ParticleSystem.EmissionModule em = RainSystem[0].GetComponent<ParticleSystem>().emission;
                    em.enabled = true;// enabling the rain
                    rainSFX.Play();
                    if (GameTimeCounter == rainingLength)
                    {
                        em.enabled = false; // turns off the rain at that time
                        rainSFX.Stop();
                    }
                }
                else
                {
                    ParticleSystem.EmissionModule em = RainSystem[0].GetComponent<ParticleSystem>().emission;
                    em.enabled = false;// disables the rain
                    rainSFX.Stop();
                }
                //print(chance); // debug

            }
        }
    }

    ////////// Scene Loading Stuff \\\\\\\\\\\\\\\\\\\\\\\\\\\\
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        GameObject GameManagement = GameObject.Find("GameManager"); // finds the Game manager Object

        Day_NightScript day_NightScript = GameManagement.GetComponent<Day_NightScript>(); // gets the day_nightScript from the gameManager gameobject
        Animator fadeAnim;
        day_NightScript.enabled = false; // disables the script
        day_NightScript.foundLights = GameObject.FindGameObjectsWithTag("Time changing lights"); // resets the found lights when the script is off
        day_NightScript.enabled = true; // renables the script
        fadeAnim = FadeController.GetComponent<Animator>();
        fadeAnim.SetBool("Fade_Controller_Bool", false);

        // Handling Quests dialogue triggers
        GameObject qManagement = GameObject.Find("QuestHandler");
        QuestManager qManager = qManagement.GetComponent<QuestManager>();
        qManager.updateDiagTriggers = true;
        if (SceneManager.GetActiveScene().name == "World Hub")
        {
            day_NightScript.pallaraxLayer2Renderer = GameObject.Find("Layer_0009_2").GetComponentsInChildren<SpriteRenderer>();
        }
        //Debug.Log(qManager.dialogTriggers[0]);
        // Debug.Log("Level Loaded");
        // Debug.Log(scene.name);
        // Debug.Log(mode);
    }
    //////////////////////////////////////////////////////////////


}