using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStopTrigger : MonoBehaviour
{
    //public GameObject sfxAudio; // sfx file to play
    public GameObject audioTrigger; // file for where the sfx was triggered from
    public GameObject sfxAudio; // sfx file to play
    public DialogueTrigger dialogueTrigger; // dialogue trigger
    public GameObject audioManager;
    public float fadeDuration = 1;
    [HideInInspector]
    public bool triggered = false; // makes sure the sfx is only plays once (otherwise it will break)
    // Start is called before the first frame update
    void Start()
    {
        audioManager=GameObject.Find("Audio Manager");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (audioTrigger.GetComponent<DialogueTrigger>().dialogueTriggered && !triggered) // if no sfx hasn't been played yet, play it
            {
                print(sfxAudio.GetComponent<AudioSource>().volume);
                StartCoroutine("fadeSound");
                //triggered = true;
                triggered = true;
            }
        }
    }

    IEnumerator fadeSound()
    {
        while (sfxAudio.GetComponent<AudioSource>().volume > 0.01f)
        {
            sfxAudio.GetComponent<AudioSource>().volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }
        sfxAudio.GetComponent<AudioSource>().volume = 0;
        sfxAudio.GetComponent<AudioSource>().Stop();
        audioManager.GetComponent<AudioManager>().musicSource.Play(); // resumes music
    }
}
