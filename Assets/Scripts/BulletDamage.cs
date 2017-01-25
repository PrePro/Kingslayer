using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{ 
    public int damage;
    private CoolDownSystem cdsystem;
    private PlayerStats stats;

    void Update()
    {
        cdsystem = GameObject.Find("Player").GetComponent<CoolDownSystem>();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Debug.Log("Damage");

            if (cdsystem.currentProjState == CoolDownSystem.ProjectileMorality.Stun)  //Stun
            {
                //Call stun
                Debug.Log("Stun Enemy w bullet");
            }

            else if (cdsystem.currentProjState == CoolDownSystem.ProjectileMorality.Debuff) //Debuff
            {
                Debug.Log("DeBuff");
            }

            else if (cdsystem.currentProjState == CoolDownSystem.ProjectileMorality.Blast) //Damage & Damage
            {
                Debug.Log("Damage");
                col.GetComponent<NPStats>().ReceiveDamage(damage);
            }
        }
    }

}
