using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesB : MonoBehaviour
{

    public GameObject combatA;
    public GameObject stealthB;
    public GameObject blockersA;

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
            blockersA.SetActive(true);
            Destroy(combatA);
            Destroy(this.gameObject);
        }
    }
}
