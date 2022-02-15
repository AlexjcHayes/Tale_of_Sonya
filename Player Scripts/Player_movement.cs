using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public bool can_Jump = true;
    [HideInInspector]
    public bool can_jump_temp; // temperay bool variable to keep track of the can_jump when dealing with collisons when climbing ladders
    [HideInInspector]
    public bool jumping = false;
    [HideInInspector]
    public bool is_climbing = false;
    public Rigidbody2D rb;  // importing the rigid body module for the player as "rb"
    public Transform trans;
    public Animator anim; // importing animation module for player as "anim"
    Animator dialogAnim;
    //public CompositeCollider2D compC2;
    public int speed = 5;
    public float climbSpeed = 2;
    public float jumpHeight = 5f;
    public double maxAirTimer = 3; // timer for how long the player can be in the air before falling back down
    public GameObject spell;
    public GameObject tmpSpl;
    GameManagement gameManager;
    GameObject GameManagement;
    private float dirTimer = 0f;
    //private double air_timer = 0;   // timer for when player is initially jumped in the air
    public PhysicsMaterial2D friction_material;
    public PhysicsMaterial2D no_friction_material;
    public SpriteRenderer player_sr;
    public float playerX; // get the x position of the player 
    public float playerY; // get the y position of the player
    Vector3 spawnPoint;
    public ParticleSystem feetParticleSystem;
    private float feetParticleSystemCounter = 0;

    bool inDialog; // boolean for turning off player movement when talking
    void Awake() // this is called before the scene is loaded 
    {
        GameManagement = GameObject.Find("GameManager"); // finds the Game manager Object
        SceneController SceneControllerScript = GameManagement.GetComponent<SceneController>(); // Gets the scene controllerscript
        gameManager = GameManagement.GetComponent<GameManagement>();
        dialogAnim = gameManager.DialogueTextBox.GetComponent<Animator>();
        spawnPoint = SceneControllerScript.scenePosition();
        if (spawnPoint.Equals(new Vector3(-1000, -1000, -1000)))
        { // sees if the return value is the dummy vector and ignores placing the player in a new spot
            //Debug.Log("working"); // debug 
        }
        else
        {
            trans.localPosition = spawnPoint;  // places player in new spot before the scenes loads
        }

    }

    private void start()
    {
        rb.GetComponent<Rigidbody2D>();
        trans.GetComponent<Transform>();
        can_jump_temp = can_Jump;

    }
    private void Update()
    {
        dialogueHandler();
        movement();
        playerX = this.trans.localPosition.x;
        playerY = this.trans.localPosition.y;

        //Debug.Log(this.trans.localPosition.x+ ", "+ this.trans.localPosition.y);
        //  attack();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Map_Foreground")
        {
            jumping = false;
            //feetParticleSystem.gameObject.SetActive(true);
            feetParticleSystem.Stop();
            feetParticleSystem.Play();
            feetParticleSystemCounter = 0;
            // air_timer = 0; // resets jump timer
            // Debug.Log(gameObject.name + " has collided with " + collision.gameObject.name);  // debug
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Map_Foreground")
        {
            jumping = false;
            feetParticleSystem.gameObject.SetActive(true);
            if (feetParticleSystemCounter <= 10)
            {
                feetParticleSystemCounter += 1;
            }
            else
            {
                feetParticleSystem.Stop();
            }
            //feetParticleSystem.Stop();

            // Debug.Log(gameObject.name + " has collided with " + collision.gameObject.name);  // debug
            // air_timer = 0; // resets jump timer
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Map_Foreground")
        {
            jumping = true;
            feetParticleSystem.Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player_Ladder")
        {
            is_climbing = true;
            can_Jump = false;
            //Debug.Log("entering ladder");
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player_Ladder")
        {
            is_climbing = true;
            can_Jump = false;
            //Debug.Log("triggered ladder");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player_Ladder")
        {
            is_climbing = false;
            can_Jump = can_jump_temp; // reassigns it to the value origionally before being triggered by the ladder
            //Debug.Log("leaving ladder");
        }
    }

    private void movement()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");

        float yDirection = Input.GetAxisRaw("Vertical");
        if (!inDialog &&!gameManager.GamePaused&& xDirection != 0)
        {  // getting keyboard input when pressing "A"
            rb.velocity = new Vector2(xDirection * speed, rb.velocity.y);  // declares a new velocity with a 2D vector
                                                                           // if (dirTimer <= 0)
                                                                           // {//if it has been long enough since the player shot

            // }
            player_sr.flipX = (xDirection > 0) ? true : false;
            anim.SetBool("running_Bool", true);
            rb.sharedMaterial = no_friction_material;
        }
        else
        {
            anim.SetBool("running_Bool", false);
            rb.sharedMaterial = friction_material;
        }

        if (Input.GetButtonDown("Jump") && !jumping && !inDialog&&can_Jump)
        {  // getting keyboard input when pressing "W"
            //Debug.Log($"{jumping} - 1");
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            //Debug.Log($"{jumping} - 2");
            anim.SetBool("jumping_Bool", true);
            //Debug.Log($"{jumping} - 3");
            // if (air_timer < maxAirTimer)
            // {
            //     rb.velocity = new Vector2(rb.velocity.x, jumpHeight);  // jumping
            //     anim.SetBool("jumping_Bool", true);
            //     air_timer += .1;
            // }

        }
        else
        {
            anim.SetBool("jumping_Bool", false);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0 && jumping&&can_Jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);

        }

        ////// Climbing  ( Need to fix this)  \\\\\\\\\\\\\\\\\\\\\\\\\

        // if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && is_climbing)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, climbSpeed);  // jumping
        //     rb.gravityScale = 0;
        // }else if((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space)) && is_climbing){
        //     rb.velocity = new Vector2(rb.velocity.x, -climbSpeed);  // jumping
        // }
        // if (Input.GetKeyDown(KeyCode.S) && is_climbing)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, -climbSpeed);  // jumping
        //     rb.gravityScale = 0;
        // }else if(is_climbing){
        //     rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y);
        //     rb.gravityScale = 0;
        // }
        // if (!is_climbing)
        // {
        //     rb.gravityScale = 5; // resets the gravity
        // }


        ///////////////////////

        if (Input.GetKey(KeyCode.S) && !inDialog)
        {  // getting keyboard input when pressing "S"
            rb.velocity = new Vector2(0, -speed);  // declares a new velocity with a 2D vector
        }
        if (Input.GetButtonDown("Fire1") && !inDialog)
        {//updating direction if the player shoots a spell
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mousePos - new Vector2(transform.position.x, transform.position.y);
            dir.Normalize();
            if (xDirection != 0)
            {
                player_sr.flipX = (xDirection > 0) ? true : false;
            }
            dirTimer = .5f;
        }
        dirTimer -= Time.deltaTime;//updating the timer based on deltaTime
    }

    private void dialogueHandler()
    {  // function to handle the input for dialogue interaction
        if (Input.GetKeyUp(KeyCode.F) && dialogAnim.GetBool("Dialogue_Box_Trigger") == true)
        { // go to the next sentence of the dialogue
            FindObjectOfType<DialogueManager>().displayNextSentence();
        }
        if (dialogAnim.GetBool("Dialogue_Box_Trigger") == true)
        {
            inDialog = true;
        }
        else if (dialogAnim.GetBool("Dialogue_Box_Trigger") == false)
        {
            inDialog = false;
        }
    }
}
