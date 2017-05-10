using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour {
    public GameObject player;
    public float CameraHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = player.transform.position;
        pos.y += CameraHeight;
        transform.position = pos;
		
	}
}
