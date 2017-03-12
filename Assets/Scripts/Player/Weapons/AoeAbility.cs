using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAbility : MonoBehaviour
{
    public int Push;
    public int damage;
    private CoolDownSystem cdsystem;
    private PlayerStats stats;
    private NPC npc;
    private bool running;

    void Update()
    {
        cdsystem = GameObject.Find("Player").GetComponent<CoolDownSystem>();
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
                        if (running) return;
            npc = col.GetComponent<NPC>();
                        if (cdsystem.AoeState == CoolDownSystem.AoeMorality.Stun)
                            
            {
                                //Debug.Log("Stun");
                                //StartCoroutine(npc.StunAI(1f));
                npc.startStunAI(2f);
              
             }
            else if (cdsystem.AoeState == CoolDownSystem.AoeMorality.KnockBack)
            {
                col.GetComponent<NPStats>().ReceiveDamage(damage);
                Vector3 dir = (transform.position - col.transform.position).normalized;
                col.transform.position -= dir * Push;
            }
            else if (cdsystem.AoeState == CoolDownSystem.AoeMorality.Steal)
            {
                Debug.Log("Steal");
                col.GetComponent<NPStats>().ReceiveDamage(5);
                stats.RecieveHealing(5);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        running = false;
    }

 }