using UnityEngine;
using UnityEditor;

public class Editor_NPCChatWindow : EditorWindow {

	public ChatManager chatManager;
	static bool showGeneralSettings;
	static bool showNPCIndexes;
	static bool showHelp;

	[MenuItem ("Window/NPC Chat")]
	static void Init () {
		Editor_NPCChatWindow window = (Editor_NPCChatWindow)EditorWindow.GetWindow (typeof (Editor_NPCChatWindow));
		Texture icon = Resources.Load("NPCChatIcon") as Texture;
		GUIContent titleContent = new GUIContent ("ChatManager", icon);
		#if UNITY_5_3_OR_NEWER
		window.titleContent = titleContent;
		#endif
		#if !UNITY_5_3_OR_NEWER
		window.title = titleContent.text;
		#endif
		window.minSize = new Vector2(300f,500f);
		window.Show();
	}

	void OnGUI () {
		if (chatManager == null) {
			chatManager = Resources.Load ("ChatManager") as ChatManager;
		}

		GUISkin editorSkin = Resources.Load ("EditorSkin") as GUISkin;

		GUI.skin = editorSkin;
		EditorGUILayout.BeginVertical ("Box");
		GUI.skin = null;
		Texture NPCChatTexture = Resources.Load ("NPCChatTexture") as Texture;
		GUIStyle inspectorStyle = new GUIStyle (GUI.skin.label);
		//inspectorStyle.fixedWidth = 256;
		inspectorStyle.fixedHeight = 32;

		EditorGUILayout.BeginHorizontal ();
		GUILayout.Label (NPCChatTexture, inspectorStyle);
		GUILayout.Label ("v1.73", editorSkin.customStyles [1]);
		EditorGUILayout.EndHorizontal ();




		GUI.skin = editorSkin;
		EditorGUILayout.BeginVertical ("Box");
		showGeneralSettings = EditorGUI.Foldout (EditorGUILayout.GetControlRect (), showGeneralSettings, "   >   Chat Manager Settings", true, editorSkin.customStyles [0]);
		EditorGUILayout.EndVertical ();
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

			chatManager.noteText = EditorGUILayout.TextArea (chatManager.noteText);
			if (showHelp)
				EditorGUILayout.LabelField ("Use this text area as a note section to help keep track of NPC Index 1Numbers", editorSkin.customStyles [3]);

			chatManager.targetNPC = EditorGUILayout.IntField ("Target NPC", chatManager.targetNPC);
			if (showHelp)
				EditorGUILayout.LabelField ("The index of Current Dialogue that will be changed when calling NewDialogue(int) on this Scriptable Object", editorSkin.customStyles [3]);

			chatManager.numberOfSlots = EditorGUILayout.IntSlider ("NPC Slots", chatManager.numberOfSlots, 1, 100);
			if (chatManager.numberOfSlots != chatManager.currentDialogue.Length) {
				System.Array.Resize (ref chatManager.currentDialogue, chatManager.numberOfSlots);
			}
			if (showHelp)
				EditorGUILayout.LabelField ("Number of NPC Conversation Indexes to use", editorSkin.customStyles [3]);

			GUI.skin = editorSkin;
			EditorGUILayout.BeginVertical ("Box");
			showNPCIndexes = EditorGUI.Foldout (EditorGUILayout.GetControlRect (), showNPCIndexes, "   >   NPC Conversation Indexes", true, editorSkin.customStyles [0]);
			EditorGUILayout.EndVertical ();
			GUI.skin = null;
			if (showNPCIndexes) {
				for (int i = 0; i < chatManager.currentDialogue.Length; i++) {
					chatManager.currentDialogue [i] = EditorGUILayout.IntField ("NPC Index " + i, chatManager.currentDialogue [i]);
				}
			}
			if (showHelp)
				EditorGUILayout.LabelField ("Determines which conversation the assigned NPC will use", editorSkin.customStyles [3]);

			chatManager.materialRef = (Material)EditorGUILayout.ObjectField ("Indicator Material", chatManager.materialRef, typeof(Material), false);
			if (showHelp)
				EditorGUILayout.LabelField ("Range notification material, used for visual debugging to determine if player is able to chat", editorSkin.customStyles [3]);
		}

		EditorGUILayout.EndVertical ();
	}
}