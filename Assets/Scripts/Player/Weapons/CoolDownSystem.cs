//======================================================================================================
// CoolDownSystem.cs
// Description: Abilities and cooldown system 
// Author: Casey Stewart
//======================================================================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CoolDownSystem : MonoBehaviour
{

    public GameObject swordInHand;
    public GameObject swordInSheeth;
    public Avatar AswordInHand;
    public Avatar AswordInSheeth;
    [Header("Animation")]
    [Tooltip("...")]
    [SerializeField]
    private Animator myAnimator;
    public bool unSheeth;
    public bool reSheeth;
    public ParticleSystem ps;
    public ParticleSystem psDash;
    public ParticleSystem psSlash;
    public AudioSource slash;
    public AudioSource slash2;
    public AudioSource parry;
    public AudioSource stab;
    public AudioSource Sheathe;
    public AudioSource DashSound;
    public AudioSource Yell;
    public AudioSource AOESound;



    public enum DashDirection
    {
        None,
        Forward,
        Left,
        Right,
        Back,
        ForwardLeft,
        ForwadRight,
        BackLeft,
        BackRight,
        Controller
    }

    public enum PlayerState
    {
        SwordInHand,
        SwordInSheeth
    }


    public enum DashState
    {
        NotDashing,
        ForwardDash,
        LeftDash,
        RightDash
    }

    private enum ProjectState
    {
        CanShoot,
        IsDone
    }

    public enum AoeMorality
    {
        Stun,
        KnockBack,
        Steal,
        Nothin
    }
    public enum ProjectileMorality
    {
        Stun,
        Debuff,
        Blast,
        Nothin
    }
    //======================================================================================================
    // Variables
    //======================================================================================================
    #region Variables
    [Header("Weapons")]
    [Tooltip("Game Objects to be used as Weaponds")]
    [SerializeField]
    private GameObject Sword;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject BulletTarget;

    [Header("Dash Times")]
    [Tooltip("The amount of time the player will dash & doubleTapTimer is how long the player has to hit the double tap")]
    public float dashTimeForward;
    public float dashTimeLeft;
    public float dashTimeRight;
    [SerializeField]
    private float doubleTapTimer;

    [Header("Sword & Projectile")]
    [Tooltip("Variables for bullets and swords")]
    //public float swingSpeed;
    public float swingTime;


    public bool swing = false;

    private bool rightIsPressed = false;
    private bool leftIsPressed = false;

    [Header("AOE")]
    [Tooltip("Variables & GameObject for AOE")]
    public GameObject AoeSphere;
    private Vector3 AoeScale;
    private bool AoeExpand = false;
    public float ScaleRate = 0.5f;
    public float aoewaitTime = 60;

    [HideInInspector]
    public DashState currentDashState;
    private ProjectState currentState;
    public DashDirection dashDirection;
    public AoeMorality AoeState;
    public ProjectileMorality currentProjState;
    public PlayerState currentAnimState;

    [SerializeField]
    private bool canSmallDash;
    public bool canAttack = true;
    [Header("Player Abilities")]
    [Tooltip("These are the players abilities and cooldowns")]
    public List<Skills> skills;
    private PlayerStats stats;
    private Movement movement;
    bool InMyState;
    private float time;

    [Header("Parry")]
    [Tooltip("Parry stuff")]
    public float parryWaitTime;
    public bool isParry;
    public GameObject Blocker;
    private bool m_isAxisInUse = false;
    [SerializeField]

    public bool AoeIsAvailable;
    public bool ProjIsAvailable;
    #endregion
    bool fake;
    //======================================================================================================
    // GameObject Functions
    //======================================================================================================
    #region GameObject Functions
        void Awake()
    {
        currentAnimState = PlayerState.SwordInSheeth;
        reSheeth = true;
        unSheeth = false;
        myAnimator.SetBool("privoUnsheeth", unSheeth);
        myAnimator.SetBool("privoResheeth", reSheeth);
        StartCoroutine(sheethDelay()); // Yash
    }
    void Start()
    {
        //ps = GetComponent<ParticleSystem>();
        foreach (Skills x in skills)
        {
            x.currentcooldown = x.cooldown; // Setting current cooldown to cooldown
        }

        AoeScale = AoeSphere.transform.localScale; // Getting Scale of the Aoe to reset state later
        currentState = ProjectState.IsDone;
        stats = gameObject.GetComponent<PlayerStats>();
        //audio = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    void SwitchAnimators()
    {
        if(time >= 1.5f)
        {
            if (swordInHand.activeSelf)
            {
                Sheathe.PlayDelayed(0.1f);
                StartCoroutine(movement.StopMovement(1f));
                currentAnimState = PlayerState.SwordInSheeth;
                reSheeth = true;
                unSheeth = false;
                myAnimator.SetBool("privoUnsheeth", unSheeth);
                myAnimator.SetBool("privoResheeth", reSheeth);
                StartCoroutine(sheethDelay());

            }
            else
            {
                Sheathe.PlayDelayed(0.1f);
                StartCoroutine(movement.StopMovement(1f));
                currentAnimState = PlayerState.SwordInHand;
                myAnimator.avatar = AswordInHand;
                unSheeth = true;
                reSheeth = false;
                myAnimator.SetBool("privoUnsheeth", unSheeth);
                myAnimator.SetBool("privoResheeth", reSheeth);
                swordInHand.SetActive(true);
                swordInSheeth.SetActive(false);
            }
            time = 0;
        }
       
    }

    IEnumerator Thing()
    {
        fake = true;
        //Debug.Log("Fuck");
        yield return new WaitForSeconds(0.6f);
        AoeSphere.SetActive(true);
        if (AOESound.isPlaying != true)
        {
            AOESound.PlayDelayed(0.1f);
        }
        ps.Play();
        StartCoroutine("AoeTime", 0.6f);
        
        //Debug.Log("Duck");
        fake = false;
    }


    void Update()
    {
        time += Time.deltaTime;
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Privo_AOE"))
        {
            //Debug.Log("AOE ANIM");
            if (fake == false)
            {
                StartCoroutine("Thing");
            }
        }
        //Update current cool down for each skill
        foreach (Skills x in skills)
        {
            if (x.currentcooldown < x.cooldown)
            {
                x.currentcooldown += Time.deltaTime; // Applying cooldown 
                if (x.CDImage != null)
                {
                    x.CDImage.fillAmount = x.currentcooldown / x.cooldown;
                }
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (currentState == ProjectState.CanShoot)
            {
                currentState = ProjectState.IsDone;
            }
            else
            {
                currentState = ProjectState.CanShoot;
            }
        }

        if (swing)
        {
            StartCoroutine("SwordSwing", swingTime);
        }
        if (Input.GetAxisRaw("DpadH") != 0)
        {
            if (m_isAxisInUse == false)
            {
                Debug.Log("DPAD");
                // Call your event function here.
                m_isAxisInUse = true;
            }
        }
        if (Input.GetAxisRaw("DpadH") == 0)
        {
            m_isAxisInUse = false;
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetAxis("LeftTrigger") == 1)
        {
            SwitchAnimators();
        }

        if(currentAnimState == PlayerState.SwordInHand)
        {
            if (Input.GetMouseButton(1) || Input.GetAxis("RightTrigger") == 1)
            {
                Debug.Log("PARRY");

                StartCoroutine(ParryDelay(parryWaitTime));
            }
        }

        if (AoeExpand)
        {
            AoeSphere.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f); //Expand the Aoe Ability
        }
    }


    void FixedUpdate()
    {
        if (canAttack == true)
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton3) && currentDashState == DashState.NotDashing) // Sword [2]LeftBumper
            {
                if (skills[6].currentcooldown >= skills[6].cooldown)
                {
                    Debug.Log("Stab");
                    if (swordInHand.activeSelf)
                    {

                        StartCoroutine(movement.StopMovement(.5f));
                        swing = true;
                        if (stab.isPlaying != true)
                        {
                            stab.PlayDelayed(0.1f);
                        }
                        
                        myAnimator.SetTrigger("privoStab");

                    }
                    skills[6].currentcooldown = 0;
                }

            }
        }

        if (currentAnimState == PlayerState.SwordInHand)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0)) // Dash [0]
            {
                if (skills[0].currentcooldown >= skills[0].cooldown)
                {
                    Debug.Log("Dash");
                    myAnimator.SetBool("privoDash", true);
                    if (Player.ControllerState == Player.Controller.Xbox_One_Controller)
                    {
                        dashDirection = DashDirection.Controller;
                    }

                    if (currentDashState == DashState.NotDashing)
                    {
                        if (Input.GetKey(KeyCode.W))// && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S)
                        {
                            dashDirection = DashDirection.Forward;
                        }
                        else if (Input.GetKey(KeyCode.S))// && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)
                        {
                            dashDirection = DashDirection.Back;
                        }
                        else if (Input.GetKey(KeyCode.A))//&& !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)
                        {
                            dashDirection = DashDirection.Left;
                        }
                        else if (Input.GetKey(KeyCode.D))// && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S)
                        {
                            dashDirection = DashDirection.Right;
                        }

                        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                        {
                            dashDirection = DashDirection.BackRight;
                        }
                        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
                        {
                            dashDirection = DashDirection.ForwadRight;
                        }
                        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
                        {
                            dashDirection = DashDirection.BackLeft;
                        }
                        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                        {
                            dashDirection = DashDirection.ForwardLeft;
                        }
                        DashSound.PlayDelayed(0.01f);
                        StartCoroutine("Dashtime", dashTimeForward);
                        skills[0].currentcooldown = 0;
                    }
                }
                else
                {
                    myAnimator.SetBool("privoDash", false);
                }
            }
        }

        if (canAttack == true)
        { // Input.GetAxis("RightTrigger") == 1
            if (Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.JoystickButton2) && currentDashState == DashState.NotDashing) // Sword [2]LeftBumper
            {
                if (skills[2].currentcooldown >= skills[2].cooldown)
                {
                    Debug.Log("Swing");
                    swing = true;
                    
                    //swordInHand.SetActive(true);
                    //swordInSheeth.SetActive(false);
                    //myAnimator.avatar = Swiningamin;
                    //if(swordInHand.activeSelf)
                    //{
                    //    //Than you can swing
                    //}
                    if (swordInHand.activeSelf)
                    {
                        swing = true;
                        psSlash.Play();
                        if (slash.isPlaying != true)
                        {
                            slash.PlayDelayed(0.2f);
                        }
                        if (Yell.isPlaying != true)
                        {
                            Yell.PlayDelayed(0.1f);
                        }
                        
                        //GameObject.Find("Player").GetComponent<Movement>().enabled = false;
                        //Debug.Log("Slash in here/");
                        //StartCoroutine(movement.StopMovement(.3f));
                        myAnimator.SetTrigger("privoSlash");
                        
                        //audio.PlayOneShot(slash, 5F);
                    }
                    skills[2].currentcooldown = 0;
                }
                else
                {
                    //GameObject.Find("Player").GetComponent<Movement>().enabled = true;
                }

            }
        }

        //if (Input.GetButton("Fire1") && currentState == ProjectState.CanShoot)
        //{
        //    if (skills[1].currentcooldown >= skills[1].cooldown && ProjIsAvailable == true) // Projectile [1]
        //    {
        //        // Set animation here
        //        if (stats.moralityPorj == 0)  //Stun
        //        {
        //            currentProjState = ProjectileMorality.Stun;
        //        }

        //        if (stats.moralityPorj == 50) //Debuff

        //        {
        //            currentProjState = ProjectileMorality.Debuff;

        //        }

        //        if (stats.moralityPorj == 100) //Damage & Damage
        //        {
        //            currentProjState = ProjectileMorality.Blast;
        //        }
        //        //Shoot();
        //        skills[1].currentcooldown = 0;
        //        currentState = ProjectState.IsDone;
        //    }
        //}

        #region Dash Left/Right
        if (canSmallDash)
        {
            if (Input.GetKeyDown(KeyCode.A)) // Dashing left [3]
            {

                if (skills[3].currentcooldown >= skills[3].cooldown)
                {
                    if (leftIsPressed)
                    {
                        StartCoroutine("DashtimeLeft", dashTimeLeft);
                        skills[3].currentcooldown = 0;
                    }
                    else
                    {
                        StartCoroutine("SetKeyPressLeft", doubleTapTimer);

                    }
                }

            }

            if (Input.GetKeyDown(KeyCode.D)) // Dashing Right [4]
            {
                if (skills[4].currentcooldown >= skills[4].cooldown)
                {
                    if (rightIsPressed)
                    {
                        StartCoroutine("DashtimeRight", dashTimeRight);
                        skills[4].currentcooldown = 0;
                    }
                    else
                    {
                        StartCoroutine("SetKeyPressRight", doubleTapTimer);

                    }
                }
            }
        }
        #endregion
        if (canAttack == true)
        {
            if (skills[5].currentcooldown >= skills[5].cooldown && AoeIsAvailable == true) //Push Back AOE
            {
                if (Input.GetKey(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton1)) // AOE [5]
                {
                    myAnimator.SetTrigger("privoAOE");
                    //StartCoroutine(movement.StopMovement(.6f));

                    //StartCoroutine("AOEWait", aoewaitTime);

                    AoeState = AoeMorality.Nothin;
                    //if (stats.Morality < -66) //Stun
                    //{
                    //    AoeState = AoeMorality.Stun;
                    //    skills[5].currentcooldown = 0;
                    //}
                    if (stats.Morality >= 0 ) //Knock Back + Damage
                    {
                        AoeState = AoeMorality.KnockBack;
                        skills[5].currentcooldown = 0;
                    }
                    if (stats.Morality <= -1) // Heal Steal
                    {
                        AoeState = AoeMorality.Steal;
                        skills[5].currentcooldown = 0;
                    }

                }
            }
        }
    }
    #endregion

    //======================================================================================================
    // IEnumerator Functions
    //======================================================================================================
    #region IEnumerator Functions
    IEnumerator Dashtime(float waitTime)
    {
        psDash.Play();
        currentDashState = DashState.ForwardDash;
        yield return new WaitForSeconds(waitTime);
        dashDirection = DashDirection.None;
        currentDashState = DashState.NotDashing;
    }

    //IEnumerator AOEWait(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //}


    IEnumerator AoeTime(float waitTime)
    {
        AoeExpand = true;
        yield return new WaitForSeconds(waitTime);
        AoeSphere.transform.localScale = AoeScale;
        AoeSphere.SetActive(false);
        AoeExpand = false;

    }

    IEnumerator DashtimeLeft(float waitTime)
    {
        currentDashState = DashState.LeftDash;
        yield return new WaitForSeconds(waitTime);
        currentDashState = DashState.NotDashing;
    }
    IEnumerator SetKeyPressRight(float waitTime)
    {
        rightIsPressed = true;
        yield return new WaitForSeconds(waitTime);
        rightIsPressed = false;
    }
    IEnumerator SetKeyPressLeft(float waitTime)
    {
        leftIsPressed = true;
        yield return new WaitForSeconds(waitTime);
        leftIsPressed = false;
    }

    IEnumerator DashtimeRight(float waitTime)
    {
        currentDashState = DashState.RightDash;
        yield return new WaitForSeconds(waitTime);
        currentDashState = DashState.NotDashing;
    }
    IEnumerator ParryDelay(float waitTime)
    {
        parry.PlayDelayed(0.01f);
        isParry = true;
        Blocker.SetActive(true);
        myAnimator.SetBool("privoParry", true);
        yield return new WaitForSeconds(waitTime);
        myAnimator.SetBool("privoParry", false);
        Blocker.SetActive(false);
        isParry = false;
    }
    IEnumerator sheethDelay()       //Yash
    {
        myAnimator.SetBool("privoResheeth", true);
        yield return new WaitForSecondsRealtime(1.02f);
        swordInHand.SetActive(false);
        swordInSheeth.SetActive(true);
        myAnimator.avatar = AswordInSheeth;
    }
    IEnumerator SwordSwing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        swing = false;
    }
    //IEnumerator SwordSwingmove(float waitTime)
    //{
    //    //Debug.Log("Swing");
    //    Sword.transform.Rotate(Vector3.back * swingSpeed);
    //    yield return new WaitForSeconds(waitTime);
    //    Sword.transform.Rotate(Vector3.forward * swingSpeed);
    //}

    #endregion

    //======================================================================================================
    // Private Member Functions 
    //======================================================================================================

}

//======================================================================================================
// Skills Class
//======================================================================================================
#region Skills Class
[System.Serializable]
public class Skills
{
    [HideInInspector]
    public string name;
    public float cooldown;
    [HideInInspector]
    public float currentcooldown;
    public Image CDImage;
}
#endregion