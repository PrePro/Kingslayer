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
    public PlayerPerception playerperception;
    //private Rigidbody rigidbody;

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
        
        if (stopMovement == false)
        {
            if (Player.ControllerState == Player.Controller.Xbox_One_Controller)
            {
                ControllerMovement();
            }
            else if (Player.ControllerState == Player.Controller.PS4_Controller)
            {
                //Debug.Log("PS4");
            }
            else
            {
                PlayerMove();
            }
        }
        else
        {
            myAnimator.SetBool("privoWalk", false);
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
            switch (coolDownSystem.dashDirection)
            {
                case CoolDownSystem.DashDirection.None:
                    {

                    }
                    break;
                case CoolDownSystem.DashDirection.Forward:
                    {
                        transform.Translate(Vector3.forward * Time.deltaTime * dashSpeedForward, target.transform);
                    }
                    break;
                case CoolDownSystem.DashDirection.Left:
                    {
                        transform.Translate(Vector3.left * Time.deltaTime * dashSpeedForward, target.transform);
                    }
                    break;
                case CoolDownSystem.DashDirection.Right:
                    {
                        transform.Translate(Vector3.right * Time.deltaTime * dashSpeedForward, target.transform);
                    }
                    break;
                case CoolDownSystem.DashDirection.Back:
                    {
                        transform.Translate(Vector3.back * Time.deltaTime * dashSpeedForward, target.transform);
                    }
                    break;
                case CoolDownSystem.DashDirection.ForwardLeft:
                    {
                        transform.Translate((Vector3.forward + Vector3.left) * Time.deltaTime * dashSpeedForward, target.transform);
                    }
                    break;
                case CoolDownSystem.DashDirection.ForwadRight:
                    {
                        transform.Translate((Vector3.forward + Vector3.right) * Time.deltaTime * dashSpeedForward, target.transform);
                    }
                    break;
                case CoolDownSystem.DashDirection.BackLeft:
                    {
                        transform.Translate((Vector3.back + Vector3.left) * Time.deltaTime * dashSpeedForward, target.transform);
                    }
                    break;
                case CoolDownSystem.DashDirection.BackRight:
                    {
                        transform.Translate((Vector3.back + Vector3.right) * Time.deltaTime * dashSpeedForward, target.transform);
                    }
                    break;
                case CoolDownSystem.DashDirection.Controller:
                    {
                        transform.Translate((Vector3.forward * Time.deltaTime * dashSpeedForward));
                    }
                    break;
                default:
                    break;
            }

            target.transform.position = objectForward.transform.position;
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

        if (coolDownSystem.currentAnimState == CoolDownSystem.PlayerState.SwordInSheeth)
        {

            if (Input.GetKey(KeyCode.Joystick1Button0) && isCrouching == false) //Input.GetKey(KeyCode.Joystick1Button8
            {
                isRunning = true;
                //mCooldown = 0;
                myAnimator.SetBool("privoRun", isRunning);
            }
            if (Input.GetKeyUp(KeyCode.Joystick1Button0) && isCrouching == false) //Input.GetKey(KeyCode.Joystick1Button8
            {
                isRunning = false;
                //mCooldown = 0;
                myAnimator.SetBool("privoRun", isRunning);
            }
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
                if (playerperception.LookAtEnemy)
                {
                    return;
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
                //Debug.Log("UP");

                if (isRunning == false && isCrouching == false)
                {
                    if (coolDownSystem.currentAnimState == CoolDownSystem.PlayerState.SwordInHand)
                    {
                        currentSpeed = runningSpeed;
                    }
                    else
                    {
                        currentSpeed = speed;
                    }
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
                // Debug.Log("RIGHT");
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
                // Debug.Log("DOWN");

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
            //Debug.Log("Stop moving");
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

        if (CanCrouch)
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
            if (playerperception.LookAtEnemy)
            {
                return;
            }

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
                if(coolDownSystem.currentAnimState == CoolDownSystem.PlayerState.SwordInHand)
                {
                    currentSpeed = runningSpeed;
                }
                else
                {
                    currentSpeed = speed;
                }
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