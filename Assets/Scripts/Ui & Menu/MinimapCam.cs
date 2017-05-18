using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour {
    public GameObject player;
    public float CameraHeight;
    private GameObject objective;
    public GameObject objectiveMarker;
	// Use this for initialization
	void Start () {
        objective = GameObject.FindGameObjectWithTag("Objective");
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = player.transform.position;
        pos.y += CameraHeight;
        transform.position = pos;
        findObjective();
	}

    void findObjective()
    {
        //Vector3 marker = player.transform.position - objective.transform.position;
        //Debug.Log(marker);
        //float distance = marker.sqrMagnitude;
        //Vector3 direction = marker / distance;
        //objectiveMarker.transform.rotation = Quaternion.Euler(marker);
    }
}
