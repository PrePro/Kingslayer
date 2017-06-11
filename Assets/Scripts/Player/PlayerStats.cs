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
    public AudioClip YouDied;
    public AudioSource audiodeath;
    public GameObject startPosition;
    public bool isDead;
    CoolDownSystem cd;
    Movement movement;
    public bool deathSound = true;
    public AudioSource PlayerDamaged;
    public AudioSource Playerhurt;
    public ParticleSystem privoHurt;
    private GameObject deathScreen;
    private bool CalledDeathOnce = false;

    void Awake()
    {
        movement = GetComponent<Movement>();
        //startPosition.transform.position = transform.position;
        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
    }

    void Start()
    {
        Morality = PlayerPrefs.GetInt("Morality", 0);
        //moralityAoe = PlayerPrefs.GetInt("moralityAoe", 0);
        //moralityPorj = PlayerPrefs.GetInt("moralityPorj", 0);
        cd = GetComponent<CoolDownSystem>();

        myAnimator = GetComponent<Animator>();
        deathScreen.SetActive(false);
        StartCoroutine(Regeneration());
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Morality", Morality);
        //PlayerPrefs.SetInt("MoralityAoe", moralityAoe);
        //PlayerPrefs.SetInt("MoralityPorj", moralityPorj);
    }

    public void TurnOnAbility(int i) // 1 is aoe 2 proj
    {
        if (i == 1)
        {
            cd.AoeIsAvailable = true;
        }
    }

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

        if (currentHealth <= 0 && !CalledDeathOnce)
        {
            CalledDeathOnce = true;
            if (deathSound == true)
            {
                audiodeath.Play();
                deathSound = false;
            }
            Debug.Log("Called Death");
            isDead = true;
            myAnimator.SetBool("privoDeath", true);
            deathScreen.SetActive(true);
            StartCoroutine("DeathAnim", 8.5f);
            movement.StartCoroutine("StopMovement", 2.4f);
            // Death animation
            //SceneManager.LoadScene("MainMenu");
        }

    }

    IEnumerator DeathAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = startPosition.transform.position;
        myAnimator.SetBool("privoDeath", false);
        isDead = false;
        deathSound = true;
        SetHealth();
        deathScreen.SetActive(false);
    }



    public override void ReceiveDamage(float damage)
    {
        if (PlayerDamaged.isPlaying != true)
        {
            PlayerDamaged.PlayDelayed(0.1f);
        }
        if (Playerhurt.isPlaying != true)
        {
            Playerhurt.PlayDelayed(0.1f);
        }
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
        CalledDeathOnce = false;
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