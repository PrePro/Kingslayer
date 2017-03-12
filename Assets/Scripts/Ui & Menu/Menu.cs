using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas pauseScreen;

    public void LoadCity()
    {
        SceneManager.LoadScene("FullCity");
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
        SceneManager.LoadScene("Barracks");
    }

    public void LoadCrypt()
    {
        SceneManager.LoadScene("CryptGrayblock");
    }

    public void LoadQuit()
    {
        Debug.Log("Clicked");

        Application.Quit();
    }

    
    public void UnPause()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}