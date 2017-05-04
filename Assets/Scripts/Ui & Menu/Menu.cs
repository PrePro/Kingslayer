using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas pauseScreen;
    public Slider volumeSlider;
    public AudioSource volumeAudio;
    void Start()
    {
        if(volumeAudio == null || volumeSlider == null)
        {
            return;
        }
        volumeSlider.value = volumeAudio.volume;
    }

    public void LoadCity()
    {
        SceneManager.LoadScene("City");
    }
    public void LoadVillage()
    {
        SceneManager.LoadScene("Village1.1");
    }
    public void LoadTestWorld()
    {
        SceneManager.LoadScene("BaseTestingWorld");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPrivoHouse()
    {
        SceneManager.LoadScene("PrivoHouse");
    }

    public void LoadBarracks()
    {
        SceneManager.LoadScene("City");
    }

    public void LoadCrypt()
    {
        SceneManager.LoadScene("Crypt");
    }

    public void LoadQuit()
    {
        Debug.Log("Clicked");

        Application.Quit();
    }
    public void LoadMainMenuNew()
    {
        SceneManager.LoadScene("Forge");
    }

    
    public void UnPause()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void VolumeControl()
    {
        volumeAudio.volume = volumeSlider.value;
    }

}