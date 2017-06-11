using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAudio : MonoBehaviour
{

    public AudioSource audioSource;
    public float startVolume;
    public GameObject Enemy;

    void Start()
    {
        startVolume = audioSource.volume;
        StartCoroutine("FadeOut", startVolume);
    }

    void Update()
    {
       
    }
    // Use this for initialization
    public IEnumerator FadeOut(float i)
    {
        Debug.Log(audioSource.volume);
        while (audioSource.volume < .5)
        {
            audioSource.volume += i * Time.deltaTime / 10;
            yield return null;
        }
        audioSource.Stop();
        //audioSource.volume = startVolume;
    }
}