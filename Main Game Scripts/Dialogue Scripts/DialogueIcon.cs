using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIcon : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public bool iconTrigger;

    void Update()
    {
        if (iconTrigger)
        {
            anim.SetBool("Appear_Bool", true);
        }
        else
        {
            anim.SetBool("Appear_Bool", false);
        }
    }

}
