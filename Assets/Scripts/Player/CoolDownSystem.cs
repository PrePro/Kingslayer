using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoolDownSystem : MonoBehaviour
{
    [Header("Weapons")]
    [Tooltip("The weapon")]
    public GameObject Sword;
    public GameObject Bullet;
    public GameObject BulletTarget;

    public enum DashState
    {
        NotDashing,
        ForwardDash,
        LeftDash,
        RightDash
    }

    public float dashTime;
    public float dashTimeLeft;
    public float dashTimeRight;

    public float bulletSpeed;
    public float swingSpeed;
    public float swingTime;

    private bool swing = false;
    public List<Skills> skills;

    public bool rightIsPressed = false;
    public bool leftIsPressed = false;
    public float CooldownDoubleTap;
    [SerializeField]
    public DashState CurrentDashState;

    [SerializeField]
    private ProjectState CurrentState;
    private enum ProjectState
    {
        CanShoot,
        IsDone
    }


    void Start()
    {
        foreach (Skills x in skills)
        {
            x.currentcooldown = x.cooldown;
        }
        CurrentState = ProjectState.IsDone;
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
            if (CurrentState == ProjectState.CanShoot)
            {
                CurrentState = ProjectState.IsDone;
            }
            else
            {
                CurrentState = ProjectState.CanShoot;
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
                if (CurrentDashState == DashState.NotDashing)
                {
                    StartCoroutine("Dashtime", dashTime);
                    skills[0].currentcooldown = 0;
                }
            }
        }

        if (Input.GetButton("Fire1") && CurrentDashState != DashState.ForwardDash) // Sword [2]
        {
            if (skills[2].currentcooldown >= skills[2].cooldown)
            {
                //do something
                StartCoroutine("SwordSwing", 0.5f);
                skills[2].currentcooldown = 0;
            }
            if (CurrentState == ProjectState.CanShoot)
            {
                if (skills[1].currentcooldown >= skills[1].cooldown) // Projectile [1]
                {
                    Shoot();
                    skills[1].currentcooldown = 0;
                    CurrentState = ProjectState.IsDone;
                }
            }
        }

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
                    StartCoroutine("SetKeyPressLeft", CooldownDoubleTap);

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
                    StartCoroutine("SetKeyPressRight", CooldownDoubleTap);
                    
                }
            }
        }
    }

    IEnumerator Dashtime(float waitTime)
    {
        CurrentDashState = DashState.ForwardDash;
        yield return new WaitForSeconds(waitTime);
        CurrentDashState = DashState.NotDashing;
    }

    IEnumerator DashtimeLeft(float waitTime)
    {
        CurrentDashState = DashState.LeftDash;
        yield return new WaitForSeconds(waitTime);
        CurrentDashState = DashState.NotDashing;
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
        CurrentDashState = DashState.RightDash;
        yield return new WaitForSeconds(waitTime);
        CurrentDashState = DashState.NotDashing;
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

    void Shoot()
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
}

//Holder for cooldowns
[System.Serializable]
public class Skills
{
    [HideInInspector]
    public string name;
    public float cooldown;
    [HideInInspector]
    public float currentcooldown;
}
