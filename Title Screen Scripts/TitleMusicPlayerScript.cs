using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusicPlayerScript : MonoBehaviour
{
    public static TitleMusicPlayerScript Instance;
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
