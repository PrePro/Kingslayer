using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControls : MonoBehaviour
{
    public CoolDownSystem player;
    public GameObject unsheathControls;
    public GameObject sheathControls;
    // Use this for initialization
 

    // Update is called once per frame
    void Update()
    {
        if(player.currentAnimState == CoolDownSystem.PlayerState.SwordInHand)
        {
            unsheathControls.SetActive(true);
            sheathControls.SetActive(false);
        }
        else
        {
            unsheathControls.SetActive(false);
            sheathControls.SetActive(true);
        }
    }
}
