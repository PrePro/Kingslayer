﻿using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    private bool mRunning = false;

    public int damage;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Player IN ENEMEY TIGGER");

        if (col.tag == "Player")
        {
            if (mRunning) return;
            Debug.Log("Player TAKES DAMAGE");
            col.GetComponent<PlayerStats>().ReceiveDamage(damage);
        }
    }
}


