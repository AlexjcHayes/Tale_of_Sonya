using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource musicSource; // music source
    public AudioSource ambientSource; // ambient source
    public float musicFadeDuration;
    public float musicMaxVolume;
    ////////////////////////////////////////////
    public float ambientFadeDuration;
    public float ambientMaxVolume;


    public AudioClip[] audioClips; // audio clips for the game
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeMusicTrack(string musicClip)
    {
        foreach (AudioClip mcClip in audioClips)
        {
            if (mcClip.name == musicClip)
            {
                musicSource.volume = 0;
                StopCoroutine(musicFadeIn());
                //StartCoroutine(musicFadeOut());
                musicSource.Stop();
                musicSource.clip = mcClip;
                musicSource.Play();
                //StopCoroutine(musicFadeOut());
                StartCoroutine(musicFadeIn());

            }
        }

    }
    public void changeAmbientTrack(string ambientClip)
    {
        foreach (AudioClip amClip in audioClips)
        {
            if (amClip.name == ambientClip)
            {
                ambientSource.volume = 0;
                StopCoroutine(ambientFadeIn());
                //StartCoroutine(ambientFadeOut());
                ambientSource.Stop();
                ambientSource.clip = amClip;
                ambientSource.Play();
                //StopCoroutine(ambientFadeOut());
                StartCoroutine(ambientFadeIn());

            }
        }

    }
    public IEnumerator musicFadeOut()
    {
        while (musicSource.volume > 0.01f)
        {
            musicSource.volume -= Time.deltaTime / musicFadeDuration;
            yield return null;
        }
        musicSource.volume = 0;
        musicSource.Pause();
        //ambientSource.GetComponent<AudioSource>().Stop();
    }

    public IEnumerator musicFadeIn()
    {
        while (musicSource.volume < musicMaxVolume)
        {
            musicSource.volume += Time.deltaTime / musicFadeDuration;
            yield return null;
        }
        musicSource.volume = musicMaxVolume;
    }

    public IEnumerator ambientFadeOut()
    {
        while (ambientSource.volume > 0.01f)
        {
            ambientSource.volume -= Time.deltaTime / ambientFadeDuration;
            yield return null;
        }
        ambientSource.volume = 0;
        ambientSource.Pause();
        //ambientSource.GetComponent<AudioSource>().Stop();
    }

    public IEnumerator ambientFadeIn()
    {
        while (ambientSource.volume < ambientMaxVolume)
        {
            ambientSource.volume += Time.deltaTime / ambientFadeDuration;
            yield return null;
        }
        ambientSource.volume = ambientMaxVolume;
    }
}
