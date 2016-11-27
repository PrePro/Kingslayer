//======================================================================================================
// NPStats.cs
// Description: Stat System for basic NPC
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;
using System;

public class NPStats : UnitStats
{


    public override void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
    }

    public override void RecieveHealing(int hpHealed)
    {
        currentHealth += hpHealed;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    

}
