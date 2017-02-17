using UnityEngine;
using System.Collections;

public class DisableOnCollision : MonoBehaviour {

	public string playername;
	public Behaviour userControl;

	void Awake(){
		userControl = GameObject.Find(playername).GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl> ();
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			//userControl = col.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl> ();
			userControl.enabled = false;
			Animator anim = col.gameObject.GetComponent<Animator> ();
			anim.SetFloat ("Forward", 0);
		}
	}

	void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			userControl.enabled = true;
		}
	}

	public void EnablePlayer(){
		userControl.enabled = true;
	}

}
