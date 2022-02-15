using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_script : spell
{
    public Rigidbody2D rb;
    public float spellVelocity = 20f;
    void Start()
    {   
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector2 dir = mousePos - new Vector2(transform.position.x, transform.position.y);
        dir.Normalize();
        rb.velocity = dir * spellVelocity;
        transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "enemy"){
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "projectile"){
            return;
        }
        Destroy(gameObject);
    }
}
