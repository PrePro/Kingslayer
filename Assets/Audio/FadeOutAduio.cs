using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAduio : MonoBehaviour {
    public AudioSource audioSource;
    public float startVolume; 
    void Start()
    {
        startVolume = audioSource.volume;
        StartCoroutine("FadeOut",startVolume);
    }
    // Use this for initialization
    public IEnumerator FadeOut( float i)
    {
        Debug.Log(audioSource.volume);
        while (audioSource.volume > 0)
        {
            audioSource.volume -= i * Time.deltaTime / 10;
            yield return null;
        }
        //audioSource.Stop();
        //audioSource.volume = startVolume;
    }
}
