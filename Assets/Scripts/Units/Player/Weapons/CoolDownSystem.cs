﻿//======================================================================================================
// CoolDownSystem.cs
// Description: Abilities and cooldown system 
// Author: Casey Stewart
//======================================================================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoolDownSystem : MonoBehaviour
{
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

    private bool swing = false;
    private bool rightIsPressed = false;
    private bool leftIsPressed = false;

    [HideInInspector]
    public DashState currentDashState;
    private ProjectState currentState;

    [SerializeField]
    private bool canSmallDash;
    [Header("Player Abilities")]
    [Tooltip("These are the players abilities and cooldowns")]
    public List<Skills> skills;

    #endregion

    //======================================================================================================
    // GameObject Functions
    //======================================================================================================
    #region GameObject Functions
    void Start()
    {
        foreach (Skills x in skills)
        {
            x.currentcooldown = x.cooldown;
        }
        currentState = ProjectState.IsDone;
    }

    void Update()
    {
        //Update current cool down for each skill
        foreach (Skills x in skills)
        {
            if (x.currentcooldown < x.cooldown)
            {
                x.currentcooldown += Time.deltaTime;
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
            StartCoroutine("SwordSwingmove", swingTime);
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
                //do something YASH HERE BITCH
                StartCoroutine("SwordSwing", 0.5f);
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
        if(canSmallDash)
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
    IEnumerator SwordSwing(float waitTime)
    {
        swing = true;
        yield return new WaitForSeconds(waitTime);
        swing = false;
    }
    IEnumerator SwordSwingmove(float waitTime)
    {
        Debug.Log("Swing");
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
}

#endregion