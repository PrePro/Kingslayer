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
    public bool isRunning;
    public bool isWalking;
    public bool isCrouching;
    public bool CanCrouch = false;
    private float mCooldown;

    public GameObject PlayerHead;
    //private Rigidbody rigidbody;


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
        //rigidbody = GetComponent<Rigidbody>();
        //isCrouching = false;
        StartCoroutine("StopMovement", StopTimer);
        currentSpeed = speed;
        target.position = transform.position;
    }

    void Update()
    {
        if (isCrouching == true)
        {
            //PlayerHead.transform.position.Set(Player.Position.x, Player.Position.y - 1, Player.Position.z);
        }

        Dashes();
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
                PlayerMove();
                //PMove();
                //MovementWithMouse();
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

    //void PMove()
    //{
    //    var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    //    Debug.Log(moveDirection);
    //    moveDirection = Camera.main.transform.TransformDirection(moveDirection);
    //    moveDirection.y = 0;

    //    rigidbody.AddForce(rigidbody.position + moveDirection * speed * Time.deltaTime);
    //}

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

        if (Input.GetKey(KeyCode.Joystick1Button8) && mCooldown >= 0.8f && isCrouching == false)
        {
            isRunning = !isRunning;
            mCooldown = 0;
            myAnimator.SetBool("privoRun", isRunning);
        }
        if (CanCrouch)
        {
            if (Input.GetKey(KeyCode.Joystick1Button1) && mCooldown >= 0.8f && isRunning == false) //Set that to b
            {
                isCrouching = !isCrouching;
                mCooldown = 0;
                myAnimator.SetBool("privoCrouch", isCrouching);
            }
        }

        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            //Player wants to move make them move 
            if (x != 0 || y != 0)
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

        // Moves the targets 
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (y >= 0 && y != 0)
            {
                Debug.Log("UP");
                if (isRunning == false && isCrouching == false)
                {
                    currentSpeed = speed;
                }

                else
                {
                    if (isCrouching == true && isRunning == false)
                    {
                        currentSpeed = crouchSpeed;
                    }
                    if (isRunning == true && isCrouching == false)
                    {
                        currentSpeed = runningSpeed;
                    }
                }
                target.Translate(Vector3.forward * Time.deltaTime * currentSpeed);


            }

            // Moves the targets 
            if (x >= 0 && x != 0)
            {
                Debug.Log("RIGHT");
                if (isRunning == false && isCrouching == false)
                {
                    currentSpeed = speed;
                }

                else
                {
                    if (isCrouching == true && isRunning == false)
                    {
                        currentSpeed = crouchSpeed;
                    }
                    if (isRunning == true && isCrouching == false)
                    {
                        currentSpeed = runningSpeed;
                    }
                }
                target.Translate(Vector3.right * Time.deltaTime * currentSpeed);

            }

            // Moves the targets 
            if (y <= 0 && y != 0)
            {
                Debug.Log("DOWN");

                if (isRunning == false && isCrouching == false)
                {
                    currentSpeed = speed;
                }

                else
                {
                    if (isCrouching == true && isRunning == false)
                    {
                        currentSpeed = crouchSpeed;
                    }
                    if (isRunning == true && isCrouching == false)
                    {
                        currentSpeed = runningSpeed;
                    }
                }
                target.Translate(Vector3.back * Time.deltaTime * currentSpeed);
            }

            // Moves the targets 
            if (x <= 0 && x != 0)
            {
                if (isRunning == false && isCrouching == false)
                {
                    currentSpeed = speed;
                }

                else
                {
                    if (isCrouching == true && isRunning == false)
                    {
                        currentSpeed = crouchSpeed;
                    }
                    if (isRunning == true && isCrouching == false)
                    {
                        currentSpeed = runningSpeed;
                    }
                }
                target.Translate(Vector3.left * Time.deltaTime * currentSpeed);
            }
        }

        // Reset
        if (x == 0 && y == 0)
        {
            Debug.Log("Stop moving");
            target.transform.position = objectForward.transform.position;
            //psWalk.Play();

            if (isRunning == false && isCrouching == false)
            {
                currentSpeed = speed;
            }

            else
            {
                if (isCrouching == true && isRunning == false)
                {
                    currentSpeed = crouchSpeed;
                }
                if (isRunning == true && isCrouching == false)
                {
                    isRunning = false;
                    myAnimator.SetBool("privoRun", isRunning);
                }
            }
        }

    }

    private void PlayerMove()
    {
        relativePosx = target.position.x - transform.position.x;
        relativePosz = target.position.z - transform.position.z;

        //Look at the target objects postion if its 0.4 away
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

        if(CanCrouch)
        {
            if (Input.GetKey(KeyCode.X) && mCooldown >= 0.8f && isRunning == false)
            {
                isCrouching = !isCrouching;
                mCooldown = 0;
                myAnimator.SetBool("privoCrouch", isCrouching);
            }
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
            if (isRunning == false && isCrouching == false)
            {
                currentSpeed = speed;
            }
            else
            {
                if (isCrouching == true && isRunning == false)
                {
                    currentSpeed = crouchSpeed;
                }
                if (isRunning == true && isCrouching == false)
                {
                    currentSpeed = runningSpeed;
                }
            }

            if (Input.GetKey(KeyCode.W))// && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S)
            {
                target.position = transform.position + target.forward;
            }
            else if (Input.GetKey(KeyCode.S))// && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)
            {
                target.position = transform.position + -target.forward;
            }
            else if (Input.GetKey(KeyCode.A))//&& !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)
            {
                target.position = transform.position + -target.right;
            }
            else if (Input.GetKey(KeyCode.D))// && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S)
            {
                target.position = transform.position + target.right;
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {
                target.position = transform.position + target.right + -target.forward;
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                target.position = transform.position + target.right + target.forward;
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                target.position = transform.position + -target.right + -target.forward;
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                target.position = transform.position + -target.right + target.forward;
            }
        }

        // Reset
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            target.transform.position = objectForward.transform.position;
            //psWalk.Play();
            if (isRunning == false && isCrouching == false)
            {
                currentSpeed = speed;
            }
            else
            {
                if (isCrouching == true && isRunning == false)
                {
                    currentSpeed = crouchSpeed;
                }
                if (isRunning == true && isCrouching == false)
                {
                    currentSpeed = runningSpeed;
                }
            }

        }

    }

    private void MovementWithMouse()
    {
        currentSpeed = 1;
        if (coolDownSystem.currentDashState == CoolDownSystem.DashState.NotDashing)
        {
            if (this.transform.rotation.x != relativePosx || this.transform.rotation.z != relativePosz)
            {
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePosx, 0, relativePosz));
                transform.rotation = rotation;
            }
        }

        target.transform.rotation = gameCamera.transform.rotation;
        target.transform.eulerAngles = new Vector3(0, target.transform.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.W)) // Up
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * 4, target.transform);

            target.Translate(Vector3.forward * currentSpeed * Time.deltaTime, target.transform);
        }
        if (Input.GetKey(KeyCode.S)) // Down
        {
            //transform.Translate(-Vector3.forward * Time.deltaTime * currentSpeed * 4, target.transform);
            target.Translate(-Vector3.forward * currentSpeed * Time.deltaTime, target.transform);
        }
        if (Input.GetKey(KeyCode.A)) // Left
        {
            // transform.Translate(-Vector3.right * Time.deltaTime * currentSpeed * 4, target.transform);
            target.Translate(-Vector3.right * currentSpeed * Time.deltaTime, target.transform);

        }
        if (Input.GetKey(KeyCode.D)) // Right
        {
            //transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * 4, target.transform);
            target.Translate(Vector3.right * currentSpeed * Time.deltaTime, target.transform);
        }


    }

    public IEnumerator StopMovement(float waitTime)
    {
        stopMovement = true;
        yield return new WaitForSeconds(waitTime);
        stopMovement = false;
    }
    #endregion

}