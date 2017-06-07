using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    private PlayerStats stats;
    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        if(stats == null)
        {
            Debug.Log("ASDA");
        }
    }

    public enum Controller
    {
        KeyBoard,
        Xbox_One_Controller,
        PS4_Controller
    }

    public Controller mController;

    void Update()
    {
        isDead = stats.isDead;
        ControllerSetUp();
    }

    private Player() { }

    static Player instance;
    static public bool isDead;
    static public Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

	public static Vector3 Position
    {
        get { return Instance.transform.position; }
    }
    public static bool DeathState
    {
        get { return Instance.stats.isDead; }
    }
    public static Controller ControllerState
    {
        get { return Instance.mController; }
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
