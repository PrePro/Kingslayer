using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

 
	void Start()
	
    {

           }

    void OnTriggerEnter()
    {
        SceneManager.LoadScene("City");
    }
    
    void Update()

{

}
 
 }