using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrelroll : MonoBehaviour {
    public Rigidbody rb;
    public Collider coll;
    public GameObject fence;
    public GameObject block;
    void Start()
    {
        fence.SetActive(true);
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
            fence.SetActive(false);
            block.SetActive(true);
        }

    }
}