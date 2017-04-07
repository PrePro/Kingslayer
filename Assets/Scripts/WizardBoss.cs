using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : MonoBehaviour
{
    public GameObject mTraps;
    public GameObject AoeBlast;

    public float mCurrentHealth;
    public float MaxHealth;
    public float HealthPercent;
    public Phase CurrentPhase;

    public bool spawnerdone = false;
    public enum Phase
    {
        Phase1, // Phase 1. 100%-75% HP
        Phase2, // Phase 2. 75%-50% HP
        Phase3  // Phase 3. 50%-0% HP
    }

    void Start()
    {
        //SpawnTraps();
    }

    void SpawnTraps()
    {
        Vector3 position = new Vector3(Random.Range(-10.0F, 10.0F), 1, Random.Range(-10.0F, 10.0F));
        Instantiate(mTraps, position, Quaternion.identity);
    }

    void Update()
    {
        GetHealthPercent();

        if (CurrentPhase == Phase.Phase1)
        {
            if (spawnerdone == false)
            {
                for (int i = 0; i < 2; i++)
                {
                    SpawnTraps();
                }
                spawnerdone = true;
            }

        }
        if (CurrentPhase == Phase.Phase2)
        {
            if (spawnerdone == false)
            {
                for (int i = 0; i < 2; i++)
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
        else if (HealthPercent < 50 && HealthPercent >= 0)
        {
            CurrentPhase = Phase.Phase3;
        }

    }
}
