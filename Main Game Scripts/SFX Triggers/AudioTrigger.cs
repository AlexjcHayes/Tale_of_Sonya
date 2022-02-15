using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public GameObject sfxAudio; // sfx file to play
    public AudioTrigger audioTrigger; // file for where the sfx was triggered from

    public GameObject audioManager;
    public DialogueTrigger dialogueTrigger; // dialogue trigger
    [HideInInspector]
    public bool triggered = false; // makes sure the sfx is only plays once (otherwise it will break)
    // Start is called before the first frame update
    void Start()
    {
        audioManager= GameObject.Find("Audio Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (audioTrigger == null) // if no sfx hasn't been played yet, play it
        {
            if (dialogueTrigger.dialogueTriggered && !triggered)
            {
                if(audioManager.GetComponent<AudioManager>().musicSource.volume>0){
                    audioManager.GetComponent<AudioManager>().musicSource.Pause(); // pauses music
                }
                sfxAudio.GetComponent<AudioSource>().Play();
                //triggered = true;
                triggered=true;
            }
        }
    }

}
