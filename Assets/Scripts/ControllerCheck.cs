using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCheck : MonoBehaviour
{
    public enum Controller
    {
        KeyBoard,
        Xbox_One_Controller,
        PS4_Controller
    }
    public Controller mController;

    static ControllerCheck instance;


    static public ControllerCheck Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ControllerCheck>();
            }
            return instance;
        }
    }

    public static Controller ControllerState
    {
        get { return Instance.mController; }
    }

    // Update is called once per frame
    void Update()
    {
        ControllerSetUp();
    }

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
}
