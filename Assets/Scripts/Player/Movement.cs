//======================================================================================================
// Movement.cs
// Description: Players Movement
// Author: Casey Stewart
//======================================================================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Movement : MonoBehaviour
{
    //======================================================================================================
    // Variables
    //======================================================================================================
    #region Variables
    [Header("Game Objects")]
    [Tooltip("Game Objects to be used")]
    public Transform target;
    public CoolDownSystem coolDownSystem;
    public Transform objectForward;
    public GameObject gameCamera;
    public ParticleSystem psWalk;

    [Header("Player Speeds")]
    [Tooltip("Speeds")]
    public float speed;
    public float crouchSpeed;
    public float runningSpeed;
    public float currentSpeed;

    private bool isRunning = false;
    private float relativePosx;
    private float relativePosz;

    [Header("Player Dash Speeds")]
    [Tooltip("How fast the player will dash")]
    public float dashSpeedForward;
    public float dashSpeedLeft;
    public float dashSpeedRight;

    [Header("Disable Movement")]
    [Tooltip("Disable movement and time it will stop")]
    public bool stopMovement = false;
    [SerializeField]
    private float StopTimer;
    [Header("Animation")]
    [Tooltip("...")]
    [SerializeField]
    private Animator myAnimator;
    [Header("Debugger")]
    [Tooltip("Dont Touch this is for testing")]
    [SerializeField]
    public bool isWalking;
    public bool isCrouching;
    private float mCooldown;


    public enum Controller
    {
        KeyBoard,
        Xbox_One_Controller,
        PS4_Controller
    }

    public Controller mController;
    #endregion

    //======================================================================================================
    // GameObject Functions
    //======================================================================================================
    #region GameObject Functions
    void Start()
    {
        isWalking = false;
        //isCrouching = false;
        StartCoroutine("StopMovement", StopTimer);
        currentSpeed = speed;
        target.position = transform.position;
    }

    void Update()
    {
        if (mCooldown <= 2f)
        {
            mCooldown += Time.deltaTime;
        }

        ControllerSetUp();
        if (stopMovement == false)
        {

            if (mController == Controller.Xbox_One_Controller)
            {
                Debug.Log("Controller");
                ControllerMovement();
            }
            else if (mController == Controller.PS4_Controller)
            {
                Debug.Log("PS4");
            }
            else
            {
                //Debug.Log("Key Board");
                Dashes();
                PlayerMove();
            }

        }
    }
    #endregion

    //======================================================================================================
    // Private Member Functions 
    //======================================================================================================
    #region Private Member Functions
    private void Dashes()
    {
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.LeftDash)
        {
            transform.Translate((Vector3.left * Time.deltaTime * dashSpeedLeft));
            target.transform.position = objectForward.transform.position;
        }

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.RightDash)
        {
            transform.Translate((Vector3.right * Time.deltaTime * dashSpeedRight));
            target.transform.position = objectForward.transform.position;
        }

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.ForwardDash)
        {
            transform.Translate((Vector3.forward * Time.deltaTime * dashSpeedForward));
            target.transform.position = objectForward.transform.position;
        }
    }

    private void ControllerSetUp()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 19)
            {
                print("PS4 CONTROLLER IS CONNECTED");
                mController = Controller.PS4_Controller;

            }
            if (names[x].Length == 33)
            {
                print("XBOX ONE CONTROLLER IS CONNECTED");
                //set a controller bool to true

                mController = Controller.Xbox_One_Controller;
            }
            else
            {
                mController = Controller.KeyBoard;
            }
        }
    }

    private void ControllerMovement()
    {
        relativePosx = target.position.x - transform.position.x;
        relativePosz = target.position.z - transform.position.z;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (x != 0 || y != 0)
            {
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePosx, 0, relativePosz));
                transform.rotation = rotation;
            }
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            isRunning = false;
            myAnimator.SetBool("privoRun", isRunning);//Added for sprint animation. 
        }

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            //Player wants to move make them move 
            if (x != 0 || y != 0)
            {
                if (Input.GetKey(KeyCode.Joystick1Button9))
                {
                    currentSpeed = crouchSpeed;
                    isCrouching = true;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                }
                else
                {
                    isCrouching = false;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                }
                transform.Translate((Vector3.forward * Time.deltaTime * currentSpeed));
                isWalking = true;
                myAnimator.SetBool("privoWalk", isWalking);
            }
            else
            {
                isWalking = false;
                myAnimator.SetBool("privoWalk", isWalking);
                //psWalk.Stop();
            }
        }
        //Target rotates around camera
        target.transform.rotation = gameCamera.transform.rotation;
        target.transform.eulerAngles = new Vector3(0, target.transform.eulerAngles.y, 0);

        // Moves the targets 
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (y >= 0 && y != 0)
            {
                Debug.Log("UP");
                if (isRunning == false)
                {
                    currentSpeed = speed;
                }
                else if (Input.GetKey(KeyCode.Joystick1Button9))
                {
                    currentSpeed = crouchSpeed;
                    isCrouching = true;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                    psWalk.Stop();
                }
                else
                {
                    isCrouching = false;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                    currentSpeed = runningSpeed;
                }
                target.Translate(Vector3.forward * Time.deltaTime * currentSpeed);


            }

            // Moves the targets 
            if (x >= 0 && x != 0)
            {
                Debug.Log("RIGHT");
                if (isRunning == false)
                {
                    currentSpeed = speed;
                }
                else if (Input.GetKey(KeyCode.Joystick1Button9))
                {
                    currentSpeed = crouchSpeed;
                    isCrouching = true;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                }
                else
                {
                    currentSpeed = runningSpeed;
                    isCrouching = false;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                }
                target.Translate(Vector3.right * Time.deltaTime * currentSpeed);

            }

            // Moves the targets 
            if (y <= 0 && y != 0)
            {
                Debug.Log("DOWN");

                if (isRunning == false)
                {
                    currentSpeed = speed;
                }
                else if (Input.GetKey(KeyCode.Joystick1Button9))
                {
                    currentSpeed = crouchSpeed;
                    isCrouching = true;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                }
                else
                {
                    currentSpeed = runningSpeed;
                    isCrouching = false;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                }
                target.Translate(Vector3.back * Time.deltaTime * currentSpeed);
            }

            // Moves the targets 
            if (x <= 0 && x != 0)
            {
                Debug.Log("LEFT");
                if (isRunning == false)
                {
                    currentSpeed = speed;
                }
                else if (Input.GetKey(KeyCode.Joystick1Button9))
                {
                    currentSpeed = crouchSpeed;
                    isCrouching = true;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                }
                else
                {
                    currentSpeed = runningSpeed;
                    isCrouching = false;
                    myAnimator.SetBool("privoCrouch", isCrouching);
                }
                target.Translate(Vector3.left * Time.deltaTime * currentSpeed);
            }
        }

        // Reset
        if (x == 0 || y == 0)
        {
            target.transform.position = objectForward.transform.position;
            //psWalk.Play();

            if (isRunning == false)
            {
                currentSpeed = speed;
            }
            else
            {
                currentSpeed = runningSpeed;
            }

        }

    }

    private void PlayerMove()
    {
        relativePosx = target.position.x - transform.position.x;
        relativePosz = target.position.z - transform.position.z;

        // Look at the target objects postion if its 0.4 away 
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (this.transform.rotation.x != relativePosx || this.transform.rotation.z != relativePosz)
            {
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePosx, 0, relativePosz));
                transform.rotation = rotation;
            }
        }

        //Cant walk in both directions
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            currentSpeed = 0;
        }

        // Starts Running
        if (Input.GetKeyDown(KeyCode.LeftShift) && isCrouching == false)
        {
            isRunning = true;
            myAnimator.SetBool("privoRun", isRunning);//Added for sprint animation. 
        }

        //Player stops running
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            myAnimator.SetBool("privoRun", isRunning);//Added for sprint animation. 
        }

        if (Input.GetKey(KeyCode.X) && mCooldown >= 0.8f && isRunning == false)
        {
            isCrouching = !isCrouching;
            mCooldown = 0;
            myAnimator.SetBool("privoCrouch", isCrouching);
        }

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                transform.Translate((Vector3.forward * Time.deltaTime * currentSpeed));
                isWalking = true;
                myAnimator.SetBool("privoWalk", isWalking);
            }
            else
            {
                isWalking = false;
                myAnimator.SetBool("privoWalk", isWalking);
                //psWalk.Stop();
            }
        }

        //Target rotates around camera
        target.transform.rotation = gameCamera.transform.rotation;
        target.transform.eulerAngles = new Vector3(0, target.transform.eulerAngles.y, 0);

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) // UP
            {
                target.Translate(Vector3.forward * Time.deltaTime * currentSpeed * 4);
            }

            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) // Down
            {
                target.Translate(Vector3.back * Time.deltaTime * currentSpeed * 4);
            }

            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) // Right
            {
                target.Translate(Vector3.right * Time.deltaTime * currentSpeed * 4);
            }

            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) // Left
            {
                target.Translate(Vector3.left * Time.deltaTime * currentSpeed * 4);
            }
        }

        // Reset
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            target.transform.position = objectForward.transform.position;
            //psWalk.Play();

            if (isRunning == true)
            {
                currentSpeed = runningSpeed;
            }
            else if (isCrouching == true)
            {
                currentSpeed = crouchSpeed;
            }
            else
            {
                currentSpeed = speed;
            }

        }

    }

    IEnumerator StopMovement(float waitTime)
    {
        stopMovement = true;
        yield return new WaitForSeconds(waitTime);
        stopMovement = false;
    }
    #endregion

}