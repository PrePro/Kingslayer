using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesB : MonoBehaviour
{

    public GameObject combatA;
    public GameObject stealthB;

    void Start()
    {
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Enter B");
            stealthB.SetActive(true);
            Destroy(combatA);
            Destroy(this.gameObject);
        }
    }
}
