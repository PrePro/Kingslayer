using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats
{
    public int moralityAoe; // 0 is bad / 100 is good
    public int moralityPorj;

    public Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            currentHealth--;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("ASD");
            SetHealth();
            transform.position = startPosition;
            //SceneManager.LoadScene("MainMenu");
        }
    }

    public override void ReceiveDamage(float damage)
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

    public void SetHealth()
    {
        currentHealth = maxHealth;
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