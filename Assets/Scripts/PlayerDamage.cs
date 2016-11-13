using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDamage : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Bullet;
    public GameObject BulletTarget;
    public bool isDashing = false;
    public bool swing = false;
    public float dashSpeed;
    public float bulletSpeed;
    public float swingSpeed;
    public List<Skills> skills; 

    void Start()
    {
        foreach (Skills x in skills)
        {
            x.currentcooldown = x.cooldown;
        }
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

        if(isDashing)
        {
            transform.Translate((Vector3.forward * Time.deltaTime * dashSpeed));
        }
        
        if(swing)
        {
            Sword.transform.Rotate(Vector3.forward * swingSpeed);
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space)) // Dash
        {
            if(skills[0].currentcooldown >= skills[0].cooldown)
            {
                StartCoroutine("Dashtime", 2);
                skills[0].currentcooldown = 0;
            }
        }

        if (Input.GetKey(KeyCode.Keypad2)) // Projectile
        {
            if (skills[1].currentcooldown >= skills[1].cooldown)
            {
                if (skills[2].currentcooldown >= skills[2].cooldown)
                {
                    Shoot();
                    skills[1].currentcooldown = 0;
                }
            }
        }

        if (Input.GetButton("Fire1")) // Sword
        {
            if (skills[2].currentcooldown >= skills[2].cooldown)
            {
                //do something
                StartCoroutine("SwordSwing", 0.5f);
                skills[2].currentcooldown = 0;
            }
        }
    }

    IEnumerator Dashtime(float waitTime)
    {
        isDashing = true;
        yield return new WaitForSeconds(waitTime);
        isDashing = false;
    }
    IEnumerator SwordSwing(float waitTime)
    {
        Debug.Log("Swing");
        swing = true;
        yield return new WaitForSeconds(waitTime);
        swing = false;
    }
 

    void Shoot()
    {
        Vector3 firePosition = BulletTarget.transform.position;
        GameObject b = GameObject.Instantiate(Bullet, firePosition, BulletTarget.transform.rotation) as GameObject;

        if (b != null)
        {
            Rigidbody rb = b.GetComponent<Rigidbody>();
            Vector3 force = transform.forward * bulletSpeed;
            rb.AddForce(force);
        }
    }
}

//Holder for cooldowns
[System.Serializable]
public class Skills
{
    public float cooldown;
    [HideInInspector]
    public float currentcooldown;
}
