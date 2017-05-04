using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats
{
    public int Morality;

    public int moralityAoe; // 0 is bad / 100 is good
    public int moralityPorj;

    public float HealthTime;
    public int HealthingAmount;
    [HideInInspector]
    public int DialogActive = 0;
    private Animator myAnimator;

    public Vector3 startPosition;

    void Awake()
    {
    }

    void Start()
    {
        Morality = PlayerPrefs.GetInt("Morality", 0);
        moralityAoe = PlayerPrefs.GetInt("moralityAoe", 0);
        moralityPorj = PlayerPrefs.GetInt("moralityPorj", 0);

        myAnimator = GetComponent<Animator>();
        startPosition = transform.position;
        StartCoroutine(Regeneration());
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Morality", Morality);
        PlayerPrefs.SetInt("MoralityAoe", moralityAoe);
        PlayerPrefs.SetInt("MoralityPorj", moralityPorj);
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
            // Death animation
            transform.position = startPosition;
            //SceneManager.LoadScene("MainMenu");
        }
    }

    public override void ReceiveDamage(float damage)
    {
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
    public float GetMorality()
    {
        return Morality;
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