using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkTutorial : MonoBehaviour {
    public GameObject interactalk;
    private bool itsDone = false;
	// Use this for initialization
	void Start ()
    {
        interactalk.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && itsDone == false)
        {
            interactalk.SetActive(true);
            itsDone = true;
            StartCoroutine("endOfTutorial");
        }
    }
    IEnumerator endOfTutorial()
    {
        yield return new WaitForSeconds(2f);
        interactalk.SetActive(false);
    }
}
