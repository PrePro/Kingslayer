using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToVillage : MonoBehaviour
    {
    public GameObject transScreen;
    public Movement movement;
    // Use this for initialization
    void Start ()
    {
        transScreen.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transScreen.SetActive(true);
            StartCoroutine(movement.StopMovement(1f));
            StartCoroutine("fade");
        }
    }
    // Update is called once per frame
    void Update () {
	}
    IEnumerator fade()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Village1.1");
    }

}
