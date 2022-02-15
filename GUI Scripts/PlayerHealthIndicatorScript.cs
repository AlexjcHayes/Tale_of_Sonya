using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthIndicatorScript : MonoBehaviour
{
    public Animator anim; // importing animation module for player as "anim"
    GameObject gameManager;
    GameManagement gameManage;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        gameManage = gameManager.GetComponent<GameManagement>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("HeathIndicatorState",gameManage.healthIndicatorState);
    }
}
