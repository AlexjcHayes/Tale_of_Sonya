using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_box_bed_script : MonoBehaviour
{
    public Animator anim;
    public GameObject Interactable_Object; // this is the object the popup box is attached to and is refrencing

    // Start is called before the first frame update
    void Start()
    {
        Interactable_Object.GetComponent<PlayerSleepScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Interactable_Object.GetComponent<PlayerSleepScript>().canSleep)
        { // getting the bool for being able to change scene from the door object
            if (Interactable_Object.GetComponent<PlayerSleepScript>().popupBoxBool)
            {
                anim.SetBool("Appear_Bool", true);
            }
            else
            {
                anim.SetBool("Appear_Bool", false);
            }
        }else{
            anim.SetBool("Appear_Bool", false);
        }
    }
}
