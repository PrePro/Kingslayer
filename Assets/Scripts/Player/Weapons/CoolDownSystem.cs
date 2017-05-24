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
    public AudioClip slash;
    //AudioSource audio;

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
        BackRight
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

    IEnumerator Thing()
    {
        fake = true;
        //Debug.Log("Fuck");
        yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorStateInfo(0).length);
        AoeSphere.SetActive(true);
        StartCoroutine("AoeTime", 0.5f);
        ps.Play();
        //Debug.Log("Duck");
        fake = false;
    }


    void Update()
    {
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Privo_leftright"))
        {
            movement.stopMovement = true;
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Privo_Right_Left_Slash"))
        {
            movement.stopMovement = true;
        }
        else
        {
            movement.stopMovement = false;
        }

        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("PrivoAOE"))
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
                // Call your event function here.
                m_isAxisInUse = true;
            }
        }
        if (Input.GetAxisRaw("DpadH") == 0)
        {
            m_isAxisInUse = false;
        }

        if (Input.GetKeyDown(KeyCode.R) || m_isAxisInUse == true)
        {
            //stats.TurnOnAbility(1);
            if (swordInHand.activeSelf)
            {
                currentAnimState = PlayerState.SwordInSheeth;
                reSheeth = true;
                unSheeth = false;
                myAnimator.SetBool("privoUnsheeth", unSheeth);
                StartCoroutine(sheethDelay()); // Yash

            }
            else
            {
                currentAnimState = PlayerState.SwordInHand;
                myAnimator.avatar = AswordInHand;
                unSheeth = true;
                reSheeth = false;
                myAnimator.SetBool("privoUnsheeth", unSheeth);
                myAnimator.SetBool("privoResheeth", reSheeth);
                swordInHand.SetActive(true);
                swordInSheeth.SetActive(false);
            }
        }


        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Joystick1Button4))
        {
            Debug.Log("PARRY");
            StartCoroutine(ParryDelay(parryWaitTime));
        }
        if (AoeExpand)
        {

            AoeSphere.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f); //Expand the Aoe Ability

        }
    }


    void FixedUpdate()
    {


        if (currentAnimState == PlayerState.SwordInHand)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0)) // Dash [0]
            {
                if (skills[0].currentcooldown >= skills[0].cooldown)
                {
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

                        StartCoroutine("Dashtime", dashTimeForward);
                        skills[0].currentcooldown = 0;
                    }
                }
            }
        }

        if (canAttack == true)
        {
            if (Input.GetButton("Fire1") || Input.GetAxis("RightTrigger") == 1 && currentDashState == DashState.NotDashing) // Sword [2]LeftBumper
            {
                if (skills[2].currentcooldown >= skills[2].cooldown)
                {
                    if (swordInHand.activeSelf)
                    {
                        swing = true;
                        psSlash.Play();
                        //StartCoroutine(movement.StopMovement(1.3f));
                        //Debug.Log("Slash in here/");
                        myAnimator.SetTrigger("privoSlash");
                        //audio.PlayOneShot(slash, 5F);
                    }
                    skills[2].currentcooldown = 0;
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
                if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Joystick1Button2)) // AOE [5]
                {
                    myAnimator.SetTrigger("privoAOE");
                    StartCoroutine(movement.StopMovement(1f));

                    StartCoroutine("AOEWait", aoewaitTime);

                    AoeState = AoeMorality.Nothin;
                    if (stats.Morality < -66) //Stun
                    {
                        AoeState = AoeMorality.Stun;
                        skills[5].currentcooldown = 0;
                    }
                    else if (stats.Morality >= -67 && stats.Morality <= 65) //Knock Back + Damage
                    {
                        AoeState = AoeMorality.KnockBack;
                        skills[5].currentcooldown = 0;
                    }
                    if (stats.Morality >= 66) // Heal Steal
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

    IEnumerator AOEWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }


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