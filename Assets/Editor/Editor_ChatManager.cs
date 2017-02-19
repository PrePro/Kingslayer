using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ChatManager))]
public class Editor_Chatmanager : Editor {

	ChatManager chatManager;

	static bool showGeneralSettings;
	static bool showNPCIndexes;
	static bool showHelp;

	void Awake(){
		if(chatManager == null){
			chatManager = Resources.Load ("ChatManager") as ChatManager;
		}
		chatManager.numberOfSlots = chatManager.currentDialogue.Length;
	}

	public override void OnInspectorGUI(){
		GUISkin editorSkin = Resources.Load("EditorSkin") as GUISkin;

		if(chatManager == null){
			chatManager = Resources.Load ("ChatManager") as ChatManager;
		}
		
		GUI.skin = editorSkin;
		EditorGUILayout.BeginVertical("Box");
		GUI.skin = null;
		Texture NPCChatTexture = Resources.Load("NPCChatTexture") as Texture;
		GUIStyle inspectorStyle = new GUIStyle(GUI.skin.label);
		//inspectorStyle.fixedWidth = 256;
		inspectorStyle.fixedHeight = 32;

		EditorGUILayout.BeginHorizontal ();
		GUILayout.Label(NPCChatTexture,inspectorStyle);
		GUILayout.Label ("v1.73", editorSkin.customStyles [1]);
		EditorGUILayout.EndHorizontal ();




		GUI.skin = editorSkin;
		EditorGUILayout.BeginVertical("Box");
		showGeneralSettings = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showGeneralSettings, "   >   Chat Manager Settings", true, editorSkin.customStyles [0]);
		EditorGUILayout.EndVertical();
		GUI.skin = null;

		if (showGeneralSettings) {

			EditorGUILayout.Space ();

			if (GUILayout.Button ("Toggle Inspector Hints")) {
				if (showHelp) {
					showHelp = false;
				} else {
					showHelp = true;
				}
			}

			EditorGUILayout.Space ();

			chatManager.noteText = EditorGUILayout.TextArea ( chatManager.noteText );
			if(showHelp)
				EditorGUILayout.LabelField ( "Use this text area as a note section to help keep track of NPC Index 1Numbers", editorSkin.customStyles [3]);
			
			chatManager.targetNPC = EditorGUILayout.IntField ("Target NPC", chatManager.targetNPC);
			if(showHelp)
				EditorGUILayout.LabelField ( "The index of Current Dialogue that will be changed when calling NewDialogue(int) on this Scriptable Object", editorSkin.customStyles [3]);

			chatManager.numberOfSlots = EditorGUILayout.IntSlider ("NPC Slots", chatManager.numberOfSlots, 1, 100);
			if(chatManager.numberOfSlots != chatManager.currentDialogue.Length){
				System.Array.Resize (ref chatManager.currentDialogue, chatManager.numberOfSlots);
			}


			GUI.skin = editorSkin;
			EditorGUILayout.BeginVertical("Box");
			showNPCIndexes = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showNPCIndexes, "   >   NPC Conversation Indexes", true, editorSkin.customStyles [0]);
			EditorGUILayout.EndVertical();
			GUI.skin = null;
			if(showNPCIndexes){
				for(int i = 0; i < chatManager.currentDialogue.Length; i++){
					chatManager.currentDialogue[i] = EditorGUILayout.IntField ("NPC Index " + i, chatManager.currentDialogue[i]);
				}
			}
			if(showHelp)
				EditorGUILayout.LabelField ( "Determines which conversation the assigned NPC will use", editorSkin.customStyles [3]);

			chatManager.materialRef = (Material) EditorGUILayout.ObjectField ("Indicator Material", chatManager.materialRef, typeof(Material), false);
			if(showHelp)
				EditorGUILayout.LabelField ( "Range notification material, used for visual debugging to determine if player is able to chat", editorSkin.customStyles [3]);
		}

		EditorGUILayout.EndVertical ();
	}
}
/*

chatComponent.chatManager = Resources.Load ("ChatManager") as ChatManager;

GUI.skin = editorSkin;
EditorGUILayout.BeginVertical("Box");
GUI.skin = null;
Texture NPCChatTexture = Resources.Load("NPCChatTexture") as Texture;
GUIStyle inspectorStyle = new GUIStyle(GUI.skin.label);
//inspectorStyle.fixedWidth = 256;
inspectorStyle.fixedHeight = 32;

EditorGUILayout.BeginHorizontal ();
GUILayout.Label(NPCChatTexture,inspectorStyle);
GUILayout.Label ("v1.73", editorSkin.customStyles [1]);

EditorGUILayout.EndHorizontal ();

GUI.skin = editorSkin;
EditorGUILayout.BeginVertical("Box");

showGeneralSettings = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showGeneralSettings, "   >   Component Settings", true, editorSkin.customStyles [0]);
EditorGUILayout.EndVertical();
GUI.skin = null;

if (showGeneralSettings) {
	if (GUILayout.Button ("Toggle Inspector Hints")) {
		if (showHelp) {
			showHelp = false;
		} else {
			showHelp = true;
		}
	}
	///Chat Manager
	SerializedProperty chatManager = serializedObject.FindProperty ("chatManager");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (chatManager, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Scriptable Object that manages the current conversation index of NPC Chat", editorSkin.customStyles [3]);
	///Player Transform
	SerializedProperty player = serializedObject.FindProperty ("player");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (player, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Transform used for distance and collision checking, Tag must be 'Player'", editorSkin.customStyles [3]);
	///Chat On Collision
	SerializedProperty chatOnCollision = serializedObject.FindProperty ("chatOnCollision");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (chatOnCollision, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Trigger chat on player collision", editorSkin.customStyles [3]);
	///Chat On Mouse Up
	SerializedProperty chatOnMouseUp = serializedObject.FindProperty ("chatOnMouseUp");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (chatOnMouseUp, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Trigger chat on mouse-up-over-collider if in range", editorSkin.customStyles [3]);
	///Chat On Key Up
	SerializedProperty chatOnKeyUp = serializedObject.FindProperty ("chatOnKeyUp");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (chatOnKeyUp, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Trigger chat on key-up if in range", editorSkin.customStyles [3]);
	///Distance To Chat
	SerializedProperty distanceToChat = serializedObject.FindProperty ("distanceToChat");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (distanceToChat, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Radius determines if the player is close enough to trigger chat, NPC Chat will close if the player leaves this area", editorSkin.customStyles [3]);
	///Text Scroll Speed
	SerializedProperty textScrollSpeed = serializedObject.FindProperty ("textScrollSpeed");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (textScrollSpeed, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "0.0001 is fastest, 20 is slowest", editorSkin.customStyles [3]);
	///Disable On Chat
	SerializedProperty disableOnChat = serializedObject.FindProperty ("disableOnChat");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (disableOnChat, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Disable behaviours when chat is triggered and enable when chat closes", editorSkin.customStyles [3]);
	showChatEvents = EditorGUI.Foldout (EditorGUILayout.GetControlRect (), showChatEvents, "OnChat Events", true);
	if (showChatEvents) {
		///OnChatEvent
		SerializedProperty OnChatEvent = serializedObject.FindProperty ("OnChatEvent");
		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.PropertyField (OnChatEvent, true);
		if (EditorGUI.EndChangeCheck ())
			serializedObject.ApplyModifiedProperties ();
		if(showHelp)
			EditorGUILayout.LabelField ( "Unity Event called when chat is triggered", editorSkin.customStyles [3]);
		///OnStopChatEvent
		SerializedProperty OnStopChatEvent = serializedObject.FindProperty ("OnStopChatEvent");
		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.PropertyField (OnStopChatEvent, true);
		if (EditorGUI.EndChangeCheck ())
			serializedObject.ApplyModifiedProperties ();
		if(showHelp)
			EditorGUILayout.LabelField ( "Unity Event called when chat closes", editorSkin.customStyles [3]);
	}
	if(showHelp && !showChatEvents)
		EditorGUILayout.LabelField ( "Unity Events that are called when chat is triggered or closes", editorSkin.customStyles [3]);
}

GUI.skin = editorSkin;
EditorGUILayout.BeginVertical("Box");
showConversationSettings = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showConversationSettings, "   >   NPC Conversation Settings", true, editorSkin.customStyles [0]);
EditorGUILayout.EndVertical();
GUI.skin = null;

if (showConversationSettings) {

	///NPC Number
	SerializedProperty nPCNumber = serializedObject.FindProperty ("nPCNumber");
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (nPCNumber, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Chat Manager index of Current Dialogue used by this NPC", editorSkin.customStyles [3]);
	//Number Of Conversations

	EditorGUILayout.BeginHorizontal ();
	SerializedProperty conversations = serializedObject.FindProperty ("conversations");
	if (conversations.intValue == 0)
		conversations.intValue = 1;
	EditorGUI.BeginChangeCheck ();
	EditorGUILayout.PropertyField (conversations, true);
	if (EditorGUI.EndChangeCheck ())
		serializedObject.ApplyModifiedProperties ();
	if (GUILayout.Button ("Update")) {
		editorConversation = 0;
		editorPage = 0;
		chatComponent.UpdateConversations ();
	}
	EditorGUILayout.EndHorizontal ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Page threads for this NPC, increase to create structured dialogue", editorSkin.customStyles [3]);

	EditorGUILayout.BeginHorizontal ();
	EditorGUILayout.LabelField ("Conversation " + editorConversation.ToString ());
	if (editorConversation >= 1) {
		if (GUILayout.Button ("-", GUILayout.MaxWidth (40))) {
			editorConversation -= 1;
			EditorGUI.FocusTextInControl (null);
		}
	} else {
		GUILayout.Box ("", GUILayout.MaxWidth (40));
	}
	if (editorConversation < chatComponent._NPCDialogue.Length - 1) {
		if (GUILayout.Button ("+", GUILayout.MaxWidth (40))) {
			editorConversation += 1;
			EditorGUI.FocusTextInControl (null);
		}
	} else {
		GUILayout.Box ("", GUILayout.MaxWidth (40));
	}

	EditorGUILayout.EndHorizontal ();
	EditorGUILayout.Space ();
	if(showHelp)
		EditorGUILayout.LabelField ( "Current conversation being edited", editorSkin.customStyles [3]);


	///Set Next Conversation
	chatComponent._NPCDialogue [editorConversation].useNextDialogue = EditorGUILayout.Toggle ("Set Next Conversation", chatComponent._NPCDialogue [editorConversation].useNextDialogue);
	if(showHelp)
		EditorGUILayout.LabelField ( "Automatically set a new conversation in the Chat Manager after this one completed", editorSkin.customStyles [3]);
	chatComponent.CalculateArrays ();
	if (chatComponent._NPCDialogue [editorConversation].useNextDialogue == true) {
		chatComponent._NPCDialogue [editorConversation].nextDialogue = EditorGUILayout.IntSlider ("Next", chatComponent._NPCDialogue [editorConversation].nextDialogue, 0, (chatComponent._NPCDialogue.Length - 1));
		if(showHelp)
			EditorGUILayout.LabelField ( "Conversation set in Chat Manager after this conversation is completed", editorSkin.customStyles [3]);
	}


	chatComponent._NPCDialogue [editorConversation].pagesOfChat = EditorGUILayout.IntSlider ("Chat Pages", chatComponent._NPCDialogue [editorConversation].pagesOfChat, 1, 1000);
	if(showHelp)
		EditorGUILayout.LabelField ( "Dialogue pages in this conversation", editorSkin.customStyles [3]);

	///Page UI Box

	EditorGUILayout.BeginHorizontal ();
	EditorGUILayout.Space ();
	GUI.skin = editorSkin;
	EditorGUILayout.BeginVertical("Box");
	showPageDialogue = EditorGUI.Foldout (EditorGUILayout.GetControlRect(), showPageDialogue, "   >   Page Dialogue", true, editorSkin.customStyles [0]);
	EditorGUILayout.EndVertical();
	GUI.skin = null;
	EditorGUILayout.EndHorizontal ();
	EditorGUILayout.Space ();

	if (showPageDialogue) {


		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Page " + editorPage.ToString ());

		if (editorPage >= 1) {
			if (GUILayout.Button ("-", GUILayout.MaxWidth (40))) {
				editorPage -= 1;
				EditorGUI.FocusTextInControl (null);
			}
		} else {
			GUILayout.Box ("", GUILayout.MaxWidth (40));
		}
		if (editorPage < chatComponent._NPCDialogue [editorConversation].pagesOfChat - 1) {
			if (GUILayout.Button ("+", GUILayout.MaxWidth (40))) {
				editorPage += 1;
				EditorGUI.FocusTextInControl (null);
			}
		} else {
			GUILayout.Box ("", GUILayout.MaxWidth (40));
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		if(showHelp)
			EditorGUILayout.LabelField ( "Current page being edited", editorSkin.customStyles [3]);

		chatComponent._NPCDialogue [editorConversation].pageAudio [editorPage] = 
			(AudioClip)EditorGUILayout.ObjectField ("Page Audio", chatComponent._NPCDialogue [editorConversation].pageAudio [editorPage], typeof(AudioClip), true) as AudioClip;
		if(showHelp)
			EditorGUILayout.LabelField ( "AudioClip played when page starts", editorSkin.customStyles [3]);

		chatComponent._NPCDialogue [editorConversation].loopAudio [editorPage] = EditorGUILayout.Toggle ("Loop Clip", chatComponent._NPCDialogue [editorConversation].loopAudio [editorPage]);
		if(showHelp)
			EditorGUILayout.LabelField ( "Loop this page's AudioCllip", editorSkin.customStyles [3]);

		chatComponent._NPCDialogue [editorConversation].setActiveAfter [editorPage] =
			(GameObject)EditorGUILayout.ObjectField ("Set Active After", chatComponent._NPCDialogue [editorConversation].setActiveAfter [editorPage], typeof(GameObject), true) as GameObject;
		if(showHelp)
			EditorGUILayout.LabelField ( "Calls SetActive(true) on assigned GameObject when page completes", editorSkin.customStyles [3]);

		chatComponent._NPCDialogue [editorConversation].disableAfter [editorPage] =
			(GameObject)EditorGUILayout.ObjectField ("Disable After", chatComponent._NPCDialogue [editorConversation].disableAfter [editorPage], typeof(GameObject), true) as GameObject;
		if(showHelp)
			EditorGUILayout.LabelField ( "Calls SetActive(false) on assigned GameObject when page completes", editorSkin.customStyles [3]);

		chatComponent._NPCDialogue [editorConversation].destroyAfter [editorPage] =
			(GameObject)EditorGUILayout.ObjectField ("Destroy After", chatComponent._NPCDialogue [editorConversation].destroyAfter [editorPage], typeof(GameObject), true) as GameObject;
		if(showHelp)
			EditorGUILayout.LabelField ( "Calls DestroyGameObject() on assigned GameObject when page completes", editorSkin.customStyles [3]);

		chatComponent._NPCDialogue [editorConversation].chatBoxes [editorPage] = 
			(GameObject)EditorGUILayout.ObjectField ("Chat Box", chatComponent._NPCDialogue [editorConversation].chatBoxes [editorPage], typeof(GameObject), true) as GameObject;
		if(showHelp)
			EditorGUILayout.LabelField ( "Assigned Chat Box for the page", editorSkin.customStyles [3]);

		EditorGUILayout.LabelField ("Chat Box Header Text");
		chatComponent._NPCDialogue [editorConversation].NPCName [editorPage] = EditorGUILayout.TextField (chatComponent._NPCDialogue [editorConversation].NPCName [editorPage]);
		EditorGUILayout.Space ();
		if(showHelp)
			EditorGUILayout.LabelField ( "Header field text, typically the NPC's name", editorSkin.customStyles [3]);

		EditorStyles.textField.wordWrap = true;
		EditorGUILayout.LabelField ("Chat Box Text");
		chatComponent._NPCDialogue [editorConversation].chatPages [editorPage] = EditorGUILayout.TextArea (chatComponent._NPCDialogue [editorConversation].chatPages [editorPage]);
		if(showHelp)
			EditorGUILayout.LabelField ( "Dialogue or message for the Chat Box", editorSkin.customStyles [3]);


		EditorGUILayout.Space ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.Space ();
		GUI.skin = editorSkin;
		EditorGUILayout.BeginVertical ("Box");
		showButtonSettings = EditorGUI.Foldout (EditorGUILayout.GetControlRect (), showButtonSettings, "   >   Page Buttons", true, editorSkin.customStyles [0]);
		EditorGUILayout.EndVertical ();
		GUI.skin = null;
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();


		if (showButtonSettings) {
			for (int i = 0; i < 6; i++) {

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.Space ();

				SerializedProperty enableButton = serializedObject.FindProperty ("_NPCDialogue").GetArrayElementAtIndex (editorConversation).FindPropertyRelative ("NPCButtons").GetArrayElementAtIndex (editorPage).FindPropertyRelative ("buttonComponent").GetArrayElementAtIndex (i);

				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (enableButton, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();



				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();
				if(showHelp)
					EditorGUILayout.LabelField ( "Enable or Disable this button", editorSkin.customStyles [4]);

				SerializedProperty buttonString = serializedObject.FindProperty ("_NPCDialogue").GetArrayElementAtIndex (editorConversation).FindPropertyRelative ("NPCButtons").GetArrayElementAtIndex (editorPage).FindPropertyRelative ("buttonString").GetArrayElementAtIndex (i);
				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (buttonString, true);
				if (EditorGUI.EndChangeCheck ())
					serializedObject.ApplyModifiedProperties ();
				if(showHelp)
					EditorGUILayout.LabelField ( "Button text", editorSkin.customStyles [4]);

				SerializedProperty NPCClick = serializedObject.FindProperty ("_NPCDialogue").GetArrayElementAtIndex (editorConversation).FindPropertyRelative ("NPCButtons").GetArrayElementAtIndex (editorPage).FindPropertyRelative ("NPCClick").GetArrayElementAtIndex (i);

				EditorGUI.BeginChangeCheck ();
				EditorGUILayout.PropertyField (NPCClick, true);
				if (EditorGUI.EndChangeCheck ()) {
					serializedObject.ApplyModifiedProperties ();
				}
				if(showHelp)
					EditorGUILayout.LabelField ( "Assign a ButtonClickedEvent for this button", editorSkin.customStyles [3]);

				EditorGUILayout.Space ();
			}
		}

	}


}

EditorGUILayout.EndVertical ();
*/