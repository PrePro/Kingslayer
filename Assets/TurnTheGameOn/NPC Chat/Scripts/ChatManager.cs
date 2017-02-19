using UnityEngine;
using System.Collections;

[System.Serializable]
public class ChatManager : ScriptableObject {


	public int targetNPC;
	[TextArea(5,5)]	public string noteText;
	public int[] currentDialogue;
	[HideInInspector]
	public int numberOfSlots;
	[Header("Project References")]
	public Material materialRef;

	void Awake(){
		numberOfSlots = currentDialogue.Length;
	}

	public void ChangeTarget( int newNPC){
		targetNPC = newNPC;
	}
	
	public void NewDialogue( int newDialogue ){
		currentDialogue[targetNPC] = newDialogue;
	}

}