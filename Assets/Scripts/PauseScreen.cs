using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Canvas pauseScreen;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

    }
}