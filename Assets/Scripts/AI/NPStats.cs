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
    public bool Death = false;
    NPC npc;
    
    void Start()
    {
        npc = this.gameObject.GetComponent<NPC>();
    }

    void Update()
    {
        healthbar.fillAmount = currentHealth / maxHealth;

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
            currentHealth -= damage;
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

    /*IEnumerator turnON(float waitTime)
    {
        Debug.Log("Ienmum running");
        healthbar.enabled = true;
        yield return new WaitForSeconds(waitTime);
        healthbar.enabled = false;
    }*/
}
