using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : MonoBehaviour
{
    public GameObject mTraps;
    WizardAOE AOE;
    public float mCurrentHealth;
    public float MaxHealth;
    [HideInInspector]
    public float HealthPercent;
    [Tooltip("Debugger dont use this")]
    public Phase CurrentPhase;
    int amountToBeSpawned;
    [HideInInspector]
    public int mHitCounter;
    [HideInInspector]
    public bool spawnerdone = false;
    public bool turnOnWizard = false;

    [Tooltip("Phase 1 time before the trap destroys")]
    public float P1Timer;
    [Tooltip("Phase 2 time before the trap destroys")]
    public float P2Timer;
    [Tooltip("Phase 3 time before the trap destroys")]
    public float P3Timer;

    [Tooltip("Min Spawn size for the room example -10")]
    public int MinSpawn;
    [Tooltip("Max Spawn size for the room example 10")]
    public int MaxSpawn;
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
    }


    public void ReceiveDamage(float damage)
    {
        Debug.Log("Taken Damage");
        mCurrentHealth -= damage;
        mHitCounter++;

    }
    void SpawnTraps()
    {
        Vector3 position = new Vector3(Random.Range(MinSpawn, MaxSpawn), 1, Random.Range(MinSpawn, MaxSpawn));
        Instantiate(mTraps, position, Quaternion.identity);
    }

    void Update()
    {
        if(turnOnWizard)
        {
            GetHealthPercent();
            switch (CurrentPhase)
            {
                case Phase.Phase1:
                    amountToBeSpawned = 2;
                    if (mHitCounter == Random.Range(2, 4))
                    {
                        AOE.gameObject.SetActive(true);
                    }
                    break;
                case Phase.Phase2:
                    amountToBeSpawned = 4;
                    if (mHitCounter == Random.Range(2, 3))
                    {
                        AOE.gameObject.SetActive(true);
                    }
                    break;
                case Phase.Phase3:
                    amountToBeSpawned = 8;
                    if (mHitCounter == Random.Range(1, 2))
                    {
                        AOE.gameObject.SetActive(true);
                    }
                    break;
                case Phase.Killed:
                    spawnerdone = true;
                    Debug.Log("Wizard is Dead");
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
}
