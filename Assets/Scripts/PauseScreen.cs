using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Canvas pauseScreen;
    private bool Start;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

    }
}
