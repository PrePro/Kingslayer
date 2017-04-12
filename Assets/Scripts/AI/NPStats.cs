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

    void Update()
    {
        healthbar.fillAmount = currentHealth / maxHealth;

        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }

    public override void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
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
