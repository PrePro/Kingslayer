using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject transScreen;
    public Movement movement;
 
	void Start()
    {
        transScreen.SetActive(false);

    }

    void OnTriggerEnter()
    {
        transScreen.SetActive(true);
        StartCoroutine(movement.StopMovement(1f));
        StartCoroutine("fade");
    }
    
    void Update()
    {

    }

    IEnumerator fade()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("City");
    }
 }