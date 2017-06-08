using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DeathBox : MonoBehaviour
{

    public AudioClip execute;
    public Animator PrivoAnimator;

    AudioSource audioexecute;

    PlayerStats stats;
    bool PlayerInTrigger;
    [Header("Death")]
    [Tooltip("How much morality the player gets for killing the enemy\nShould be negative")]
    public int MoralityForKilling;

    void Update()
    {
        if(PlayerInTrigger == true)
        {
            if (Input.GetKey(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                //audioexecute.PlayDelayed(1f);
                PrivoAnimator.SetTrigger("Execute");
                stats.Morality += MoralityForKilling;
                Destroy(transform.parent.gameObject);

            }
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            stats = col.GetComponent<PlayerStats>();
            PlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            stats = col.GetComponent<PlayerStats>();
            PlayerInTrigger = false;
        }
    }
}
