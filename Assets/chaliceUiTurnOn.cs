using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaliceUiTurnOn : MonoBehaviour {
    public GameObject chaliceStuff;
    public GameObject newQuest;
    public GameObject oldQuest;
    // Use this for initialization
    void Start () {
        chaliceStuff.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            chaliceStuff.SetActive(true);
            oldQuest.SetActive(false);
            newQuest.SetActive(true);
        }
    }
}
