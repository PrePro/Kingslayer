using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats
{
    public int moralityAoe; // 0 is bad / 100 is good
    public int moralityPorj;

    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            currentHealth--;
        }
    }

    public override void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
    }

    public override void RecieveHealing(int hpHealed)
    {
        currentHealth += hpHealed;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
