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
    [Header("Animation")]
    [Tooltip("...")]
    [SerializeField]
    private Animator myAnimator;

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
    public float bulletSpeed;
    public float swingSpeed;
    public float swingTime;

    //public bool swing = false;
    private bool rightIsPressed = false;
    private bool leftIsPressed = false;

    [Header("AOE")]
    [Tooltip("Variables & GameObject for AOE")]
    public GameObject AoeSphere;
    private Vector3 AoeScale;
    private bool AoeExpand = false;
    public float ScaleRate = 0.5f;

    [HideInInspector]
    public DashState currentDashState;
    private ProjectState currentState;
    public AoeMorality AoeState;

    [SerializeField]
    private bool canSmallDash;
    [Header("Player Abilities")]
    [Tooltip("These are the players abilities and cooldowns")]
    public List<Skills> skills;
    private PlayerStats stats;

    #endregion

    //======================================================================================================
    // GameObject Functions
    //======================================================================================================
    #region GameObject Functions
    void Start()
    {
        foreach (Skills x in skills)
        {
            x.currentcooldown = x.cooldown; // Setting current cooldown to cooldown
        }

        AoeScale = AoeSphere.transform.localScale; // Getting Scale of the Aoe to reset state later
        currentState = ProjectState.IsDone;
        stats = gameObject.GetComponent<PlayerStats>();
    }

    void Update()
    {
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

        //if (swing)
        //{
        //    StartCoroutine("SwordSwingmove", swingTime);
        //}

        if(AoeExpand)
        {
            AoeSphere.transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f); //Expand the Aoe Ability
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) // Dash [0]
        {
            if (skills[0].currentcooldown >= skills[0].cooldown)
            {
                if (currentDashState == DashState.NotDashing)
                {
                    StartCoroutine("Dashtime", dashTimeForward);
                    skills[0].currentcooldown = 0;
                }
            }
        }

        if (Input.GetButton("Fire1") && currentDashState == DashState.NotDashing) // Sword [2]
        {
            if (skills[2].currentcooldown >= skills[2].cooldown)
            {
                myAnimator.SetTrigger("privoSlash");
                //StartCoroutine("SwordSwing", 0.5f); // Dont need this with animation
                skills[2].currentcooldown = 0;
            }
            if (currentState == ProjectState.CanShoot)
            {
                if (skills[1].currentcooldown >= skills[1].cooldown) // Projectile [1]
                {
                    Shoot();
                    skills[1].currentcooldown = 0;
                    currentState = ProjectState.IsDone;
                }
            }
        }
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

        if (skills[5].currentcooldown >= skills[5].cooldown) //Push Back AOE
        {
            if (Input.GetKey(KeyCode.Alpha3)) // AOE [5]
            {
                AoeState = AoeMorality.Nothin;
                if (stats.moralityAoe == 0) //Stun
                {
                    // Added stun enemy here
                    AoeSphere.SetActive(true);
                    AoeState = AoeMorality.Stun;
                    StartCoroutine("AoeTime", 0.5f);
                    skills[5].currentcooldown = 0;
                }
                else if(stats.moralityAoe == 50) //Knock Back + Damage
                {
                    AoeSphere.SetActive(true);
                    AoeState = AoeMorality.KnockBack;
                    StartCoroutine("AoeTime", 0.5f);
                    skills[5].currentcooldown = 0;
                }
                if(stats .moralityAoe == 100) // Heal Steal
                {
                    AoeSphere.SetActive(true);
                    AoeState = AoeMorality.Steal;
                    StartCoroutine("AoeTime", 0.5f);
                    skills[5].currentcooldown = 0;
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
        currentDashState = DashState.ForwardDash;
        yield return new WaitForSeconds(waitTime);
        currentDashState = DashState.NotDashing;
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
    //IEnumerator SwordSwing(float waitTime)
    //{
    //    swing = true;
    //    yield return new WaitForSeconds(waitTime);
    //    swing = false;
    //}
    IEnumerator SwordSwingmove(float waitTime)
    {
        //Debug.Log("Swing");
        Sword.transform.Rotate(Vector3.back * swingSpeed);
        yield return new WaitForSeconds(waitTime);
        Sword.transform.Rotate(Vector3.forward * swingSpeed);
    }
    #endregion

    //======================================================================================================
    // Private Member Functions 
    //======================================================================================================
    #region Private Member Functions
    private void Shoot()
    {
        Vector3 firePosition = BulletTarget.transform.position;
        GameObject bullet = GameObject.Instantiate(Bullet, firePosition, BulletTarget.transform.rotation) as GameObject;

        if (bullet != null)
        {
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 force = transform.forward * bulletSpeed;
            rigidbody.AddForce(force);
        }
    }
   
    #endregion

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