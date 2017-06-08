using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesA : MonoBehaviour {

    public GameObject combatA;
    public GameObject stealthB;
    public GameObject blockersB;

    void Start ()
    {   
    }
	
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Enter A");
            combatA.SetActive(true);
            blockersB.SetActive(true);
            Destroy(stealthB);
            Destroy(this.gameObject);
        }
    }
}
