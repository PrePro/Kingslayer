﻿using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats
{
    public int moralityAoe; // 0 is bad / 100 is good
    public int moralityPorj;
    public float HealthTime;
    public int HealthingAmount;
    public int DialogActive = 0;
    private Animator myAnimator;

    public Vector3 startPosition;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        startPosition = transform.position;
        StartCoroutine(Regeneration());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            currentHealth--;
        }

        if (currentHealth <= 0)
        {
            SetHealth();
            transform.position = startPosition;
            //SceneManager.LoadScene("MainMenu");
        }
    }

    public override void ReceiveDamage(float damage)
    {
        Debug.Log("TAKE DAMAGE");
        myAnimator.SetTrigger("privoHurt");
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

    IEnumerator Regeneration()
     {
         while (true)
         {
             if (currentHealth<maxHealth)
             { 
                 currentHealth += HealthingAmount;
                 yield return new WaitForSeconds(HealthTime);
             }
             else
             { 
                 yield return null;
             }
         }
     }
 } 