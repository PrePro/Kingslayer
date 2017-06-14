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
    private NPStats npcstats;
    private bool running;
    //Rigidbody r;

    void Update()
    {
        cdsystem = GetComponentInParent<CoolDownSystem>();
        stats = GetComponentInParent<PlayerStats>();
    }

    //IEnumerator PushBack(float waitTime, Vector3 dir)
    //{
    //    r.AddForce(dir * Push);
    //    yield return new WaitForSeconds(waitTime);
    //    Debug.Log("Set back 0");
    //    r.velocity = Vector3.zero;

    //}

    void OnTriggerExit()
    {
        //npc = null;
    }

    void OnTriggerEnter(Collider col)
    {
        //r = col.GetComponentInParent<Rigidbody>();
        if (col.tag == "Enemy")
        {
            npc = col.GetComponent<NPC>();
            if (npc == null)
            {
                npc = col.GetComponentInParent<NPC>();
            }
            if (npcstats == null)
            {
                npc = col.GetComponentInParent<NPC>();
            }
            npcstats = col.GetComponent<NPStats>();

            if (npcstats == null)
            {
                npcstats = col.GetComponentInParent<NPStats>();
            }

            if (cdsystem.AoeState == CoolDownSystem.AoeMorality.Stun)
            {
                npc.startStunAI(2f);
            }
            else if (cdsystem.AoeState == CoolDownSystem.AoeMorality.KnockBack)
            {
                Debug.Log("KnockBack");

                npcstats.HitAoe = true;
                npcstats.ReceiveDamage(damage);

                npc.PushBack(transform.position);

            }
            else if (cdsystem.AoeState == CoolDownSystem.AoeMorality.Steal)
            {
                Debug.Log("Steal");

                //npcstats.HitAoe = true;
                npcstats.ReceiveDamage(damage);
                stats.RecieveHealing(10);
            }
        }
    }
}
