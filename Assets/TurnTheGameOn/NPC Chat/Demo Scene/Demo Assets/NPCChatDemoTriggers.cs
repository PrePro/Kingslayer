using UnityEngine;
using System.Collections;

public class NPCChatDemoTriggers : MonoBehaviour {

	public ChatManager chatManager;
	public Transform platformGate;
	public bool fanOn;
	public float fanSpeed;
	public GameObject fanParticles;
	public UnityStandardAssets.Utility.AutoMoveAndRotate rotateScript;
	public Vector3 closedPosition;
	public Vector3 openPosition;
	public bool gateClosed;
	public float gateSpeed;

	public void ToggleFan(){
		if (fanOn) {
			fanOn = false;
			fanParticles.SetActive(false);
			rotateScript.rotateDegreesPerSecond.value.x = 0;
		} else {
			fanOn = true;
			fanParticles.SetActive(true);
			rotateScript.rotateDegreesPerSecond.value.x = 46;
		}
	}

	public void TogglePlatformGate(){
		if (gateClosed) {
			gateClosed = false;
		} else {
			gateClosed = true;
		}
	}

	public void ResetLevel(){
		chatManager.currentDialogue [1] = 0;
		Application.LoadLevel (Application.loadedLevel);
	}

	void Update(){
		if (gateClosed) {
			platformGate.position = Vector3.MoveTowards(platformGate.position, closedPosition, gateSpeed );
		} else {
			platformGate.position = Vector3.MoveTowards(platformGate.position, openPosition, gateSpeed);
		}
	}

}

