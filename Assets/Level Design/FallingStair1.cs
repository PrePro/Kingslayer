using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStair1 : MonoBehaviour {
    public Rigidbody rb;
    public Collider coll;
    public GameObject block;
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
            rb.isKinematic = false;
            rb.useGravity = true;
            block.SetActive(true);
        }

    }
}