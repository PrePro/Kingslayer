using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuImageUpdater : MonoBehaviour {


    public GameObject House;
    public GameObject City;
    public GameObject Crypt;
    public GameObject Village;
    public GameObject Barracks;
    public GameObject Castle;

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

}
