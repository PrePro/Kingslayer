using UnityEngine;
using System.Collections;

public class ExampleReference : MonoBehaviour {

	public ChatManager chatManager;

	public void ChangeTarget(int targetNPC){
		chatManager.ChangeTarget (targetNPC);
	}

	public void ChangeConversation(int newConversation){
		chatManager.NewDialogue (newConversation);
	}

}
