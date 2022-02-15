using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Enemy_movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D pl;
    public int speed = 3;
    public int bounds = 10;//how far the enemy is allowed to move around
    public int sense = 10;//how far away the enemy can detect the player
    int dir = 1;
    int initx, inity;
    public bool frozen = false;
    float freezeTime = 3f;
    Point spawn;
    
    private void Start(){
        spawn = new Point((int)rb.position.x, (int)rb.position.y);
    }
    private void Update(){
        if(frozen){
            rb.velocity = new Vector2(0, 0);
            freezeTime -= Time.deltaTime;
            if(freezeTime < 0){
                frozen = false;
            }
        }
        else{
            freezeTime = 3;
            if(Math.Abs(rb.position.x - pl.position.x) < sense){//player detection
                if(pl.position.x > rb.position.x){
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else{
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
            }
            else{
                if(dir == 1){
                rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else if(dir == -1){
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                if(rb.position.x < (spawn.x - bounds)){
                    dir = 1;
                }
                if(rb.position.x >  (spawn.x + bounds)){
                    dir = -1;
                }
            }
        }
        
    }
}
