using UnityEngine;
using System.Collections;

public class PlayerStats : UnitStats
{
    public int Morality;

    //public int moralityAoe; // 0 is bad / 100 is good
    //public int moralityPorj;

    public float HealthTime;
    public int HealthingAmount;
    [HideInInspector]
    public int DialogActive = 0;
    private Animator myAnimator;

    public Vector3 startPosition;
    public bool isDead;
    //  CoolDownSystem cd;
    Movement movement;
    public ParticleSystem privoHurt;
    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void Start()
    {
        Morality = PlayerPrefs.GetInt("Morality", 0);
        //moralityAoe = PlayerPrefs.GetInt("moralityAoe", 0);
        //moralityPorj = PlayerPrefs.GetInt("moralityPorj", 0);
        //cd = GetComponent<CoolDownSystem>();

        myAnimator = GetComponent<Animator>();
        startPosition = transform.position;
        StartCoroutine(Regeneration());
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Morality", Morality);
        //PlayerPrefs.SetInt("MoralityAoe", moralityAoe);
        //PlayerPrefs.SetInt("MoralityPorj", moralityPorj);
    }

   //public void TurnOnAbility(int i) // 1 is aoe 2 proj
   // {
   //     if (i == 1)
   //     {
   //         moralityAoe = Morality;
   //         Morality = 0;
   //         cd.AoeIsAvailable = true;
   //     }
   //     else if (i == 2)
   //     {
   //         moralityPorj = Morality;
   //         Morality = 0;
   //         cd.ProjIsAvailable = true;
   //     }
   // }

    void MoralityUpdater()
    {
        if(Morality > 100)
        {
            Morality = 100;
        }
        else if(Morality < -100)
        {
            Morality = -100;
        }
    }

    void Update()
    {
        MoralityUpdater();
        if (Input.GetKey(KeyCode.U))
        {
            currentHealth--;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
            myAnimator.SetBool("privoDeath", true);
            StartCoroutine("DeathAnim", 2.4f);
            movement.StartCoroutine("StopMovement", 2.4f);
            // Death animation
            //SceneManager.LoadScene("MainMenu");
        }

    }
    IEnumerator DeathAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SetHealth();
        transform.position = startPosition;
        isDead = false;
        myAnimator.SetBool("privoDeath", false);
    }



    public override void ReceiveDamage(float damage)
    {
        myAnimator.SetTrigger("privoHurt");
        currentHealth -= damage;
        privoHurt.Play();
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