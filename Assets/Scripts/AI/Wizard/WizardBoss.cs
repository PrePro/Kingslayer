using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardBoss : MonoBehaviour
{
    public GameObject mTraps;
    [Tooltip("Make Size 3")]
    public GameObject[] TelportPoints;
    WizardAOE AOE;
    public float mCurrentHealth;
    public float MaxHealth;
    public Image healthBar;
    public ParticleSystem psImpact;
    public ParticleSystem psDeath;
    public ParticleSystem psAOE;
    public ParticleSystem psTrap;
    [HideInInspector]
    public float HealthPercent;
    [Tooltip("Debugger dont use this")]
    public Phase CurrentPhase;
    int amountToBeSpawned;
    //[HideInInspector]
    public int mHitCounter;
    public int mHitAOE;
    [HideInInspector]
    public bool spawnerdone = false;
    public bool turnOnWizard = false;
    public float particleAdj;
    private Animator myAnimator;
    public AudioSource AOESound;
    public ParticleSystem psTele;
    public AudioSource trapstart;
    public GameObject traploop;
    public AudioSource traplooper;
    public AudioSource Grunt;
    public AudioSource Grunt1;
    public AudioSource Grunt2;
    public AudioSource blood;
    public int randomNumber;

    [Tooltip("Phase 1 time before the trap destroys")]
    public float P1Timer;
    [Tooltip("Phase 2 time before the trap destroys")]
    public float P2Timer;
    [Tooltip("Phase 3 time before the trap destroys")]
    public float P3Timer;

    public enum Phase

    {
        Phase1, // Phase 1. 100%-75% HP
        Phase2, // Phase 2. 75%-50% HP
        Phase3,  // Phase 3. 50%-0% HP
        Killed // When the wizard dies
    }

    void Start()
    {
        AOE = transform.GetComponentInChildren<WizardAOE>();
        AOE.gameObject.SetActive(false);
        myAnimator = GetComponent<Animator>();
        //myAnimator.SetTrigger("WizardIdle");
    }


    public void ReceiveDamage(float damage)
    {
        if (Grunt.isPlaying != true && Grunt1.isPlaying != true && Grunt2.isPlaying != true)
        {
            randomNumber = Random.Range(1, 3);

            if (randomNumber == 1)
            {
                Grunt.PlayDelayed(0.1f);
            }
            else if (randomNumber == 2)
            {
                Grunt1.PlayDelayed(0.1f);
            }
            else if (randomNumber == 3)
            {
                Grunt2.PlayDelayed(0.1f);
            }
            blood.PlayDelayed(0.1f);
        }



        Debug.Log("Taken Damage");
        mCurrentHealth -= damage;
        mHitCounter++;
        mHitAOE++; 
        myAnimator.SetTrigger("WizardHit");
        psImpact.Play();
    }
    void SpawnTraps()
    {
        float minX = transform.position.x;
        float minZ = transform.position.z;

        minX += (Random.Range(-10, 20));
        minZ += (Random.Range(-10, 20));

        Vector3 position = new Vector3(minX, transform.position.y, minZ);
        Instantiate(mTraps, position, Quaternion.identity);
        myAnimator.SetTrigger("WizardTrap");
        trapstart.PlayDelayed(0.6f);
        traploop.SetActive(true);
        traplooper.PlayDelayed(3f);
    }


    /*
        A min =
        A max =
        T min = 
        T max = 
        amountspawn = how many traps are spawned
    */
    void RunPhase(int Amin, int Amax, int Tmin, int Tmax, int amountspawn, int phaseIndex)
    {
        amountToBeSpawned = amountspawn;
        if (mHitAOE == Random.Range(Amin, Amax)) // AOE
        {
            myAnimator.SetTrigger("WizardAOE");
            AOESound.PlayDelayed(0.1f);
            psAOE.Play();
            StartCoroutine("ParticleTimer", particleAdj);
            mHitAOE = 0;
        }
        else if (mHitCounter == Random.Range(Tmin, Tmax)) // TP
        {
            transform.position = TelportPoints[phaseIndex].transform.position;
            psTele.Play();
            mHitCounter = 0;
        }
    }

    void Update()
    {
        if(turnOnWizard)
        {
            GetHealthPercent();
            switch (CurrentPhase)
            {
                case Phase.Phase1:
                    RunPhase(5,6, 9,10, 5, 1);
                        break;
                case Phase.Phase2:
                    RunPhase(5, 6, 9, 10, 10, 2); 
                        break;
                case Phase.Phase3:
                    RunPhase(5, 6, 9, 10, 15, 3);
                        break;
                case Phase.Killed:
                    myAnimator.SetTrigger("WizardDeath");
                    psDeath.Play();
                    spawnerdone = true;

                    break;
                default:
                    break;
            }
            if (spawnerdone == false)
            {
                for (int i = 0; i < amountToBeSpawned; i++)
                {
                    SpawnTraps();
                }
                spawnerdone = true;
            }
        }
        healthBar.fillAmount = mCurrentHealth / MaxHealth;
        Debug.Log(healthBar.fillAmount);

    }

    IEnumerator ParticleTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(AOE.ExpandTime(0.5f));
        AOE.gameObject.SetActive(true);
        // PUT STUFF HERE
    }

    void GetHealthPercent()
    {
        HealthPercent = mCurrentHealth / MaxHealth;
        HealthPercent *= 100;

        if (HealthPercent >= 75)
        {
            CurrentPhase = Phase.Phase1;
        }
        else if (HealthPercent < 75 && HealthPercent >= 50)
        {
            CurrentPhase = Phase.Phase2;
        }
        else if (HealthPercent < 50 && HealthPercent > 0)
        {
            CurrentPhase = Phase.Phase3;
        }
        else if (HealthPercent <= 0)
        {
            CurrentPhase = Phase.Killed;
        }

    }

    IEnumerator wizDeath()
    {
        yield return new WaitForSeconds(3.5f);
        gameObject.SetActive(false);
    }

}
