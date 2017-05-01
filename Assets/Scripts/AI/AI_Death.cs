using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Death : MonoBehaviour
{

    PlayerStats stats;
    bool PlayerInTrigger;

    void Update()
    {
        if(PlayerInTrigger == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("I KILLL YOU BITCH");
                stats.Morality -= 10;
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
