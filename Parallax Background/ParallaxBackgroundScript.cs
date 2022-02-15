using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundScript : MonoBehaviour
{

    private float length, startpos;
    public GameObject Camera;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (Camera.transform.position.x * (1 - parallaxEffect));
        float distance = (Camera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        // Looping effect
        if (temp > startpos + (length))
        {
            startpos += (length*2);
        }
        else if (temp < startpos - (length))
        {
            startpos -= (length*2);
        }
    }
}
