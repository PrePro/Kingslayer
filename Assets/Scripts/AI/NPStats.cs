//======================================================================================================
// NPStats.cs
// Description: Stat System for basic NPC
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class NPStats : UnitStats
{
    public Image healthbar;
    public ParticleSystem hitSpark;
    public bool Death = false;
    public bool tookDamage;
    public bool HitAoe;
    NPC npc;
    
    void Start()
    {
        npc = this.gameObject.GetComponent<NPC>();
        healthbar.gameObject.SetActive(false);
    }

    void Update()
    {
        Debug.Log(HitAoe);
        if(npc.canDie)
        {
            healthbar.fillAmount = currentHealth / maxHealth;
        }
        
        if(currentHealth <= 0 && Death == false)
        {
            Death = true;
            npc.SetState(NPCBase.State.Dead);
        }


    }

    public override void ReceiveDamage(float damage)
    {
        if(Death == false)
        {
            StartCoroutine("TookDamage", 0.5f);
            currentHealth -= damage;
            hitSpark.Play();
            if(!HitAoe)
            {
                npc.SetAnimation(NPCBase.AnimationState.HitFlinch);
            }
            healthbar.gameObject.SetActive(true);
        }
        // Set animation damage here
        
        //StartCoroutine("turnON", 1);

    }

    public override void RecieveHealing(int hpHealed)
    {
        currentHealth += hpHealed;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    IEnumerator TookDamage(float waitTime)
    {
        tookDamage = true;
        yield return new WaitForSeconds(waitTime);
        tookDamage = false;
    }

    /*IEnumerator turnON(float waitTime)
    {
        Debug.Log("Ienmum running");
        healthbar.enabled = true;
        yield return new WaitForSeconds(waitTime);
        healthbar.enabled = false;
    }*/
}
