using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CameraSwitchMenu : MonoBehaviour
{
    public Button[] ObjectsList;

    public Button[] LevelsList;
    //public Button[] OptionsList;

    public GameObject cam1;
    public GameObject cam2;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    public GameObject House;
    public GameObject City;
    public GameObject Crypt;
    public GameObject Village;
    public GameObject Barracks;
    public GameObject Castle;
    public GameObject House1;
    public GameObject City1;
    public GameObject Crypt1;
    public GameObject Village1;
    public GameObject bgImage;
    public GameObject volume;
    public GameObject resTexture;
    public GameObject bgImage2;
    // Use this for initialization


    public enum Controller
    {
        KeyBoard,
        Xbox_One_Controller,
        PS4_Controller
    }
    public Controller mController;
    private bool axisInUse = false;
    public int Xbox_holder;
    private Color mButtonsColor;
    private bool FirstOption = true;
    private bool Levelseletion = false;
    private bool Options = false;
    private int BackHolder;

    private void ControllerSetUp()
    {
        string[] names = Input.GetJoystickNames();

        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 19)
            {
                //print("PS4 CONTROLLER IS CONNECTED");
                mController = Controller.PS4_Controller;

            }
            if (names[x].Length == 33)
            {
                //print("XBOX ONE CONTROLLER IS CONNECTED");
                //set a controller bool to true

                mController = Controller.Xbox_One_Controller;
            }
            else
            {
                Debug.Log("KeyBoard");
                mController = Controller.KeyBoard;
            }
        }
    }

    void Awake()
    {
        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);

        House.SetActive(false);
        City.SetActive(false);
        Crypt.SetActive(false);
        Village.SetActive(false);
        Barracks.SetActive(false);
        Castle.SetActive(false);
        House1.SetActive(false);
        City1.SetActive(false);
        Crypt1.SetActive(false);
        Village1.SetActive(false);
        bgImage.SetActive(false);
        volume.SetActive(false);
        resTexture.SetActive(false);
        bgImage2.SetActive(false);
        //cam2.gameObject.SetActive(false);
        //cam1.gameObject.SetActive(true);
        cam1.gameObject.SetActive(true);

        mButtonsColor = ObjectsList[0].GetComponent<Image>().color;
    }
    
    // Update is called once per frame
    void Update()
    {
        ControllerSetUp();
        if(Input.GetKeyDown(KeyCode.JoystickButton1) && !FirstOption)
        {
            ClearButtonColor();
            ClearButtonColorTwo();
            FirstOption = true;
            levelSelectOff();
            optionSelectOff();
            Xbox_holder = BackHolder;
        }
        if (FirstOption)
        {
            ControllerSupport();
        }
        else
        {

            if(Levelseletion)
            {
                //LevelsList
                SecondControllerSupport();
            }
            else if(Options)
            {

            }
        }
    }

    public void enableCamera1()
    {
        Debug.Log("enableCam1");
        cam1.SetActive(true);
        cam2.SetActive(false);

        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);

        House.SetActive(false);
        City.SetActive(false);
        Crypt.SetActive(false);
        Village.SetActive(false);
        Barracks.SetActive(false);
        Castle.SetActive(false);


    }

    public void enableCamera2()
    {
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);

        House.SetActive(true);
        City.SetActive(true);
        Crypt.SetActive(true);
        Village.SetActive(true);
        Barracks.SetActive(true);
        Castle.SetActive(true);

        Debug.Log("enableCam2");
        cam1.SetActive(false);
        cam2.SetActive(true);
    }

    public void levelSelect()
    {
        House1.SetActive(true);
        City1.SetActive(true);
        Crypt1.SetActive(true);
        Village1.SetActive(true);
        bgImage.SetActive(true);
    }
    public void levelSelectOff()
    {
        House1.SetActive(false);
        City1.SetActive(false);
        Crypt1.SetActive(false);
        Village1.SetActive(false);
        bgImage.SetActive(false);
    }
    public void optionSelectOn()
    {
        volume.SetActive(true);
        resTexture.SetActive(true);
        bgImage2.SetActive(true);
    }
    public void optionSelectOff()
    {
        volume.SetActive(false);
        resTexture.SetActive(false);
        bgImage2.SetActive(false);
    }
    void ClearButtonColor()
    {
        foreach (Button button in ObjectsList)
        {
            button.GetComponent<Image>().color = mButtonsColor;
        }
    }
    void ClearButtonColorTwo()
    {
        foreach (Button button in LevelsList)
        {
            button.GetComponent<Image>().color = mButtonsColor;
        }
    }

    void ControllerSupport()
    {
        if (mController == Controller.Xbox_One_Controller)
        {
            Debug.Log("Xbox Controller");
            ObjectsList[Xbox_holder].GetComponent<Image>().color = Color.yellow;
            if (Input.GetAxisRaw("DpadV") == 0)
            {
                axisInUse = false;
            }

            if (Input.GetAxisRaw("DpadV") == -1)
            {
                if (axisInUse == false)
                {
                    axisInUse = true;
                    if (Xbox_holder + 1 > 2)
                    {
                        return;
                    }
                    else
                    {
                        Xbox_holder += 1;
                        ObjectsList[Xbox_holder - 1].GetComponent<Image>().color = mButtonsColor;
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
                        ObjectsList[Xbox_holder + 1].GetComponent<Image>().color = mButtonsColor;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                if (Xbox_holder == 0)
                {
                    BackHolder = 0;
                    Xbox_holder = 0;
                    levelSelect();
                    optionSelectOff();
                    FirstOption = false;
                    Levelseletion = true;
                }
                else if (Xbox_holder == 1)
                {
                    BackHolder = 1;
                    Xbox_holder = 0;
                    optionSelectOn();
                    levelSelectOff();
                    FirstOption = false;
                    Options = true;
                }
                else if (Xbox_holder == 2)
                {
                    Application.Quit();
                }
            }
        }
    }

    void SecondControllerSupport()
    {
        if (mController == Controller.Xbox_One_Controller)
        {
            LevelsList[Xbox_holder].GetComponent<Image>().color = Color.yellow;
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
                        LevelsList[Xbox_holder - 1].GetComponent<Image>().color = mButtonsColor;
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
                        LevelsList[Xbox_holder + 1].GetComponent<Image>().color = mButtonsColor;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                if (Xbox_holder == 0)
                {
                    SceneManager.LoadScene("PrivoHouse");
                }
                else if (Xbox_holder == 1)
                {
                    SceneManager.LoadScene("City");
                }
                else if (Xbox_holder == 2)
                {
                    SceneManager.LoadScene("Crypt");
                }
                else if (Xbox_holder == 3)
                {
                    SceneManager.LoadScene("Village1.1");
                }
            }
        }
    }
}
