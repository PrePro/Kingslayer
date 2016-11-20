using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDamage : MonoBehaviour
{
    [Header("Weapons")]
    [Tooltip("The weapon")]
    public GameObject Sword;
    public GameObject Bullet;
    public GameObject BulletTarget;

    public bool isDashing = false;
    
    public float dashSpeed;
    public float dashSpeedLeft;
    public float dashSpeedRight;
    public float dashTime;
    public float dashTimeLeft;
    public float dashTimeRight;
    public float bulletSpeed;
    public float swingSpeed;
    public float swingTime;

    public float LastTap = 0;
    public float doubleTapTimimg;
    public bool doubleTapLeft = false;
    public bool doubleTapRight = false;
    private bool swing = false;
    public List<Skills> skills;

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

        if (isDashing)
        {
            transform.Translate((Vector3.forward * Time.deltaTime * dashSpeed));
        }

        if (doubleTapLeft)
        {
            transform.Translate((Vector3.left * Time.deltaTime * dashSpeedLeft));
        }

        if (swing)
        {
            StartCoroutine("SwordSwingmove", swingTime);
        }

        if (Input.GetKey(KeyCode.A)) // Dashing left 
        {
            if (skills[3].currentcooldown >= skills[3].cooldown)
            {
                Debug.Log(LastTap);
                Debug.Log(Time.time - LastTap);
                if ((Time.time - LastTap) > doubleTapTimimg)
                {
                    Debug.Log("Single");
                }
                else
                {
                    StartCoroutine("DashtimeLeft", dashTimeLeft);
                }
                LastTap = Time.time;
                skills[3].currentcooldown = 0;
            }

        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) // Dash
        {
            if (skills[0].currentcooldown >= skills[0].cooldown)
            {
                StartCoroutine("Dashtime", dashTime);
                skills[0].currentcooldown = 0;
            }
        }

        if (Input.GetButton("Fire1") && !isDashing) // Sword
        {
            if (skills[2].currentcooldown >= skills[2].cooldown)
            {
                //do something
                StartCoroutine("SwordSwing", 0.5f);
                skills[2].currentcooldown = 0;
            }
            if (CurrentState == ProjectState.CanShoot)
            {
                if (skills[1].currentcooldown >= skills[1].cooldown)
                {
                    Shoot();
                    skills[1].currentcooldown = 0;
                    CurrentState = ProjectState.IsDone;
                }
            }
        }

      
    }

    IEnumerator Dashtime(float waitTime)
    {
        isDashing = true;
        yield return new WaitForSeconds(waitTime);
        isDashing = false;
    }

    IEnumerator DashtimeLeft(float waitTime)
    {
        doubleTapLeft = true;
        yield return new WaitForSeconds(waitTime);
        doubleTapLeft = false;
    }

    IEnumerator DashtimeRight(float waitTime)
    {
        doubleTapRight = true;
        yield return new WaitForSeconds(waitTime);
        doubleTapRight = false;
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
