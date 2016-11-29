using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadCity()
    {
        SceneManager.LoadScene("City");
    }
    public void LoadVillage()
    {
        SceneManager.LoadScene("Village");
    }
    public void LoadTestWorld()
    {
        SceneManager.LoadScene("BaseTestingWorld");
    }

}
