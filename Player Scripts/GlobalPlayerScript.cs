using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayerScript : MonoBehaviour
{
    public static GlobalPlayerScript Instance;
    public float savedPlayerX;
    public float savedPlayerY;
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
}
