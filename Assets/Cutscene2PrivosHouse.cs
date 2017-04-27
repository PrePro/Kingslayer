using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2PrivosHouse : MonoBehaviour {
    public GameObject PrivoPlayer;
    public float speed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PrivoPlayer.transform.Translate(new Vector3(50, 50, 50) * Time.deltaTime);
            //chair.transform.Translate (Vector3.right* speed * Time.deltaTime);
        }
    }
}
