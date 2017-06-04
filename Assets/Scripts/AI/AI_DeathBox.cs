using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DeathBox : MonoBehaviour
{

    public AudioClip execute;
    public Animator PrivoAnimator;

    PlayerStats stats;
    bool PlayerInTrigger;
    [Header("Death")]
    [Tooltip("How much morality the player gets for killing the enemy\nShould be negative")]
    public int MoralityForKilling;
    public int MoralityForSaving;
    private bool isSaved = false;

    void Update()
    {
        if(PlayerInTrigger == true)
        {
            if (Input.GetKey(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton5) && !isSaved) // kill
            {
                Debug.Log("Kill");
                //PrivoAnimator.SetTrigger("Execute");
                stats.Morality -= MoralityForKilling;
                Destroy(transform.parent.gameObject);
            }
            if (Input.GetKey(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton4) && !isSaved) // Let them live
            {
                Debug.Log("Save");
                //PrivoAnimator.SetTrigger("Execute");
                stats.Morality += MoralityForSaving;
                isSaved = true;
                //Destroy(transform.parent.gameObject);
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
            PlayerInTrigger = false;
        }
    }
}
