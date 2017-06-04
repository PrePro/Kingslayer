using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Canvas pauseScreen;

    public void UnPause()
    {
        Debug.Log("Test");
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            Pause();
        }
    }

    //void ControllerSupport()
    //{
    //    if (mController == Controller.Xbox_One_Controller)
    //    {
    //        Debug.Log("Xbox Controller");
    //        ObjectsList[Xbox_holder].GetComponent<Image>().color = Color.yellow;
    //        if (Input.GetAxisRaw("DpadV") == 0)
    //        {
    //            axisInUse = false;
    //        }

    //        if (Input.GetAxisRaw("DpadV") == -1)
    //        {
    //            if (axisInUse == false)
    //            {
    //                axisInUse = true;
    //                if (Xbox_holder + 1 > 2)
    //                {
    //                    return;
    //                }
    //                else
    //                {
    //                    Xbox_holder += 1;
    //                    ObjectsList[Xbox_holder - 1].GetComponent<Image>().color = mButtonsColor;
    //                }
    //            }
    //        }
    //        if (Input.GetAxisRaw("DpadV") == 1)
    //        {
    //            if (axisInUse == false)
    //            {
    //                axisInUse = true;
    //                if (Xbox_holder - 1 < 0)
    //                {
    //                    return;
    //                }
    //                else
    //                {
    //                    Xbox_holder -= 1;
    //                    ObjectsList[Xbox_holder + 1].GetComponent<Image>().color = mButtonsColor;
    //                }
    //            }
    //        }
    //        if (Input.GetKeyDown(KeyCode.JoystickButton0))
    //        {
    //            if (Xbox_holder == 0)
    //            {
    //                BackHolder = 0;
    //                Xbox_holder = 0;
    //                levelSelect();
    //                optionSelectOff();
    //                FirstOption = false;
    //                Levelseletion = true;
    //            }
    //            else if (Xbox_holder == 1)
    //            {
    //                BackHolder = 1;
    //                Xbox_holder = 0;
    //                optionSelectOn();
    //                levelSelectOff();
    //                FirstOption = false;
    //                Options = true;
    //            }
    //            else if (Xbox_holder == 2)
    //            {
    //                Application.Quit();
    //            }
    //        }
    //    }
    //}
}
