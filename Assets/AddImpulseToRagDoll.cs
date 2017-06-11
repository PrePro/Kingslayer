using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddImpulseToRagDoll : MonoBehaviour {
    public Rigidbody rb;
    // Use this for initialization
    void Start () {

        rb.AddForce(Vector3.right * 2000);
    }
		// Update is called once per frame
	void Update () {
		
	}
}
