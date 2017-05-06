using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFound : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(TurnOff());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        
    }
}
