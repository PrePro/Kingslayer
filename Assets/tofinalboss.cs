using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tofinalboss : MonoBehaviour {
    public GameObject transScreen;
    public Movement movement;
    private bool done = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && done == false)
        {
            transScreen.SetActive(true);
            done = true;
            StartCoroutine("waitup");
        }
    }

    IEnumerator waitup()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("FinalBossCinematic");

    }
}
