using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAOE : MonoBehaviour
{

    public Main_Dialogue MD;
    public PlayerStats stats;
    public GameObject Trap;

    void Update()
    {
        if(MD.mEndTalk)
        {
            Trap.SetActive(false);
            stats.TurnOnAbility(1);
        }
    }
}
