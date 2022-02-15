using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell : MonoBehaviour
{
    public bool dead = false;
    public float aliveTime = 5f;
    // Start is called before the first frame update
    public void Update(){
        aliveTime -= Time.deltaTime;
        if(aliveTime < 0){
            dead = true;
        }
    }
}
