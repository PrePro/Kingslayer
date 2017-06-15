using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStairs : MonoBehaviour {
    public Rigidbody rb;
    public Collider coll;
    public GameObject block;
    public GameObject startPoint;
    public GameObject newRespawn;
    void Start()
    {
        block.SetActive(false);
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            startPoint.transform.position = newRespawn.transform.position;
            rb.isKinematic = false;
            rb.useGravity = true;
            block.SetActive(true);
        }

    }
}