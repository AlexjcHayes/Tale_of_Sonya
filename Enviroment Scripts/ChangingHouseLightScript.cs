using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Rendering.LWRP;   //OLD VERSIONS LIKE 2018
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class ChangingHouseLightScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Color dayColor;
    public Color nightColor;
    public Light2D houseLight;
    GameObject GameManagement;
    GameManagement gm;
    void Start()
    {
        GameManagement = GameObject.Find("GameManager"); // finds the Game manager Object
        gm = GameManagement.GetComponent<GameManagement>(); // Gets the scene controllerscript  
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.GameTime == "Day")
        {
            houseLight.color = dayColor;
        }
        else if (gm.GameTime == "Night")
        {
            houseLight.color = nightColor;
        }
    }
}
