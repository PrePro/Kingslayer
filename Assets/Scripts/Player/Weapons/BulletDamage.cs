using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{

    public int damage;
    private CoolDownSystem cdsystem;
    private PlayerStats stats;
    private NPC npc;

    void Update()
    {
        cdsystem = GameObject.Find("Player").GetComponent<CoolDownSystem>();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

     void OnTriggerEnter(Collider col)
{
    if (col.tag == "Enemy")
    {
        npc = col.GetComponent<NPC>();
        
                    if (cdsystem.currentProjState == CoolDownSystem.ProjectileMorality.Stun)  //Stun
                        {
                            //Call stun
            Debug.Log("Stun Enemy w bullet");
            StartCoroutine(npc.RootAI(2f));
                        }
        
                    else if (cdsystem.currentProjState == CoolDownSystem.ProjectileMorality.Debuff) //Debuff
                        {
            Debug.Log("DeBuff");
            StartCoroutine(npc.StunAI(2f));
                        }
        
                    else if (cdsystem.currentProjState == CoolDownSystem.ProjectileMorality.Blast) //Damage & Damage
                        {
            Debug.Log("Damage");
            col.GetComponent<NPStats>().ReceiveDamage(damage);

            }

      
    }
}
 }