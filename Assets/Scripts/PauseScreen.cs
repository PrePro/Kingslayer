using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public Canvas pauseScreen;
    public Button[] ButtonList;
    private bool isPaused;

    private Color mButtonsColor;
    private bool axisInUse = false;
    public int Xbox_holder;
    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        mButtonsColor = ButtonList[0].GetComponent<Image>().color;
    }

    public void UnPause()
    {
        Debug.Log("Test");
        pauseScreen.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pauseScreen.gameObject.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    void ClearButtonColor()
    {
        foreach (Button button in ButtonList)
        {
            button.GetComponent<Image>().color = mButtonsColor;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            Pause();
        }

        if (Player.ControllerState == Player.Controller.Xbox_One_Controller && isPaused)
        {
            Selection();
        }
    }

    void Selection()
    {
        Debug.Log("Xbox Controller");
        ButtonList[Xbox_holder].GetComponent<Image>().color = Color.red;
        if (Input.GetAxisRaw("DpadV") == 0)
        {
            axisInUse = false;
        }

        if (Input.GetAxisRaw("DpadV") == -1)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                if (Xbox_holder + 1 > 3)
                {
                    return;
                }
                else
                {
                    Xbox_holder += 1;
                    ButtonList[Xbox_holder - 1].GetComponent<Image>().color = mButtonsColor;
                }
            }
        }
        if (Input.GetAxisRaw("DpadV") == 1)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                if (Xbox_holder - 1 < 0)
                {
                    return;
                }
                else
                {
                    Xbox_holder -= 1;
                    ButtonList[Xbox_holder + 1].GetComponent<Image>().color = mButtonsColor;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (Xbox_holder == 0)
            {
                UnPause();
                Xbox_holder = 0;
            }
            if (Xbox_holder == 1)
            {
                SceneManager.LoadScene("Forge");
                Xbox_holder = 0;
            }
            if (Xbox_holder == 2)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(scene.name);
                Xbox_holder = 0;
            }
            if (Xbox_holder == 3)
            {
                Application.Quit();
            }

        }
    }

}