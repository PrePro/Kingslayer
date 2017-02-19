using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode()]
public class ChatBox : MonoBehaviour {
	
	[System.Serializable]
	public class ChatBoxComponents{
		[Header("Chat Box Images And Text")]
		public Text text;
		public Image image;
		[Space(15)]
		public Text headerText;
		public Image headerImage;
		public RawImage headerRawImage;
		public Camera renderTextureCamera;
		[Space(15)]
		public Image backgroundImage;
		public ButtonComponents[] buttonComponents;
	}
	
	[System.Serializable]
	public class ButtonComponents{
		public Button buttons;
		public Image buttonsImage;
		public Text buttonsText;
	}
	
	[System.Serializable]
	public class ChatBoxAndText{
		[Range(0,0.25f)] public float textSize;
		public Vector3 _Rotation;
		[HideInInspector] public Quaternion rotation = Quaternion.Euler(0, 0, 0);
		[HideInInspector] public float chatBoxPosX;
		[HideInInspector] public float chatBoxPosY;
		[HideInInspector] public float chatBoxWidth;
		[HideInInspector] public float chatBoxHeight;
		[Range(-1,1)] public float positionX;
		[Range(-1,1)] public float positionY;
		[Range(0,1)] public float width;
		[Range(0,1)] public float height;
	}
	
	[System.Serializable]
	public class ChatBoxHeader{
		[Header("Header Image")]
		public Vector3 _Rotation;
		[HideInInspector] public Quaternion rotation = Quaternion.Euler(0, 0, 0);
		[HideInInspector] public float chatBoxPosX;
		[HideInInspector] public float chatBoxPosY;
		[HideInInspector] public float chatBoxWidth;
		[HideInInspector] public float chatBoxHeight;
		[Range(-1,1)] public float positionX;
		[Range(-1,1)] public float positionY;
		[Range(0,1)] public float width;
		[Range(0,1)] public float height;
		[Header("Header Text")]
		[Range(0,0.25f)] public float textSize;
		public Vector3 _textRotation;
		[HideInInspector] public Quaternion textRotation = Quaternion.Euler(0, 0, 0);
		[HideInInspector] public float textPosX;
		[HideInInspector] public float textPosY;
		[HideInInspector] public float textWidthX;
		[HideInInspector] public float textHeightY;
		[Range(-1,1)] public float textPositionX;
		[Range(-1,1)] public float textPositionY;
		[Range(0,1)] public float textWidth;
		[Range(0,1)] public float textHeight;
	}
	
	[System.Serializable]
	public class ChatBoxBackground{
		public Vector3 _Rotation;
		[HideInInspector] public Quaternion rotation = Quaternion.Euler(0, 0, 0);
		[HideInInspector] public float chatBoxPosX;
		[HideInInspector] public float chatBoxPosY;
		[HideInInspector] public float chatBoxWidth;
		[HideInInspector] public float chatBoxHeight;
		[Range(-1,1)] public float positionX;
		[Range(-1,1)] public float positionY;
		[Range(0,1)] public float width;
		[Range(0,1)] public float height;
	}
	
	[System.Serializable]
	public class ChatBoxButtons{
		[Range(0,0.25f)] public float textSize;
		public Vector3 _Rotation;
		[HideInInspector] public Quaternion rotation = Quaternion.Euler(0, 0, 0);
		[HideInInspector] public float chatBoxPosX;
		[HideInInspector] public float chatBoxPosY;
		[HideInInspector] public float chatBoxWidth;
		[HideInInspector] public float chatBoxHeight;
		[Range(-1,1)] public float positionX;
		[Range(-1,1)] public float positionY;
		[Range(0,1)] public float width;
		[Range(0,1)] public float height;
	}
	
	[Space(10)]	public ChatBoxComponents chatBoxComponents;
	[Header("Rect Transform Controls")]
	[Space(10)]	public ChatBoxAndText chatBoxAndText;
	[Space(10)] public ChatBoxHeader chatBoxHeader;
	[Space(10)] public ChatBoxBackground chatBoxBackground;
	[Space(10)] public ChatBoxButtons[] chatBoxButtons;
	[Space(10)] public bool disableScriptRectControl;
	
	void Start () {
		if(!disableScriptRectControl){		
			CalculateScale ();	
		}	
	}
	
	#if UNITY_EDITOR
	void Update () {	if (!disableScriptRectControl) {	CalculateScale ();	}	}
	#endif
	
	void CalculateScale(){
		///
		//Main Chat Box and Text Rect Constraints
		///
		
		float size = Screen.height * chatBoxAndText.textSize;
		chatBoxComponents.text.fontSize = (int)size;
		
		chatBoxAndText.chatBoxWidth = Screen.width * chatBoxAndText.width;
		chatBoxAndText.chatBoxHeight = Screen.height * chatBoxAndText.height;
		chatBoxAndText.chatBoxPosX = Screen.width * chatBoxAndText.positionX;
		chatBoxAndText.chatBoxPosY = Screen.height * chatBoxAndText.positionY;
		
		Vector2 temp1;
		temp1.x = chatBoxAndText.chatBoxWidth;
		temp1.y = chatBoxAndText.chatBoxHeight;
		chatBoxComponents.image.rectTransform.sizeDelta = temp1;
		chatBoxComponents.text.rectTransform.sizeDelta = temp1;
		
		Vector3 temp2;
		temp2.x = chatBoxAndText.chatBoxPosX;
		temp2.y = chatBoxAndText.chatBoxPosY;
		temp2.z = 0f;
		chatBoxComponents.image.rectTransform.localPosition = temp2;
		chatBoxComponents.text.rectTransform.localPosition = temp2;
		
		chatBoxAndText.rotation = Quaternion.Euler(chatBoxAndText._Rotation.x, chatBoxAndText._Rotation.y, chatBoxAndText._Rotation.z);
		chatBoxComponents.image.rectTransform.localRotation = chatBoxAndText.rotation;
		chatBoxComponents.text.rectTransform.localRotation = chatBoxAndText.rotation;
		///
		//Chat Box Header Rect Constraints
		///
		
		
		
		float size1 = Screen.height * chatBoxHeader.textSize;
		chatBoxComponents.headerText.fontSize = (int)size1;
		
		chatBoxHeader.chatBoxWidth = Screen.width * chatBoxHeader.width;
		chatBoxHeader.chatBoxHeight = Screen.height * chatBoxHeader.height;
		chatBoxHeader.chatBoxPosX = Screen.width * chatBoxHeader.positionX;
		chatBoxHeader.chatBoxPosY = Screen.height * chatBoxHeader.positionY;
		
		chatBoxHeader.textWidthX = Screen.width * chatBoxHeader.textWidth;
		chatBoxHeader.textHeightY = Screen.height * chatBoxHeader.textHeight;
		chatBoxHeader.textPosX = Screen.width * chatBoxHeader.textPositionX;
		chatBoxHeader.textPosY = Screen.height * chatBoxHeader.textPositionY;
		
		Vector2 temp3;
		temp3.x = chatBoxHeader.chatBoxWidth;
		temp3.y = chatBoxHeader.chatBoxHeight;
		chatBoxComponents.headerImage.rectTransform.sizeDelta = temp3;
		chatBoxComponents.headerRawImage.rectTransform.sizeDelta = temp3;
		
		Vector2 temp3text;
		temp3text.x = chatBoxHeader.textWidthX;
		temp3text.y = chatBoxHeader.textHeightY;
		chatBoxComponents.headerText.rectTransform.sizeDelta = temp3text;
		
		Vector3 temp4;
		temp4.x = chatBoxHeader.chatBoxPosX;
		temp4.y = chatBoxHeader.chatBoxPosY;
		temp4.z = 0f;
		chatBoxComponents.headerImage.rectTransform.localPosition = temp4;
		chatBoxComponents.headerRawImage.rectTransform.localPosition = temp4;
		
		Vector3 temp4text;
		temp4text.x = chatBoxHeader.textPosX;
		temp4text.y = chatBoxHeader.textPosY;
		temp4text.z = 0f;
		chatBoxComponents.headerText.rectTransform.localPosition = temp4text;
		
		chatBoxHeader.rotation = Quaternion.Euler(chatBoxHeader._Rotation.x, chatBoxHeader._Rotation.y, chatBoxHeader._Rotation.z);
		chatBoxComponents.headerImage.rectTransform.localRotation = chatBoxHeader.rotation;
		chatBoxComponents.headerRawImage.rectTransform.localRotation = chatBoxHeader.rotation;
		
		chatBoxHeader.textRotation = Quaternion.Euler(chatBoxHeader._textRotation.x, chatBoxHeader._textRotation.y, chatBoxHeader._textRotation.z);
		chatBoxComponents.headerText.rectTransform.localRotation = chatBoxHeader.textRotation;
		///
		//Chat Box Background Rect Constraints
		///
		chatBoxBackground.chatBoxWidth = Screen.width * chatBoxBackground.width;
		chatBoxBackground.chatBoxHeight = Screen.height * chatBoxBackground.height;
		chatBoxBackground.chatBoxPosX = Screen.width * chatBoxBackground.positionX;
		chatBoxBackground.chatBoxPosY = Screen.height * chatBoxBackground.positionY;
		
		Vector2 temp5;
		temp5.x = chatBoxBackground.chatBoxWidth;
		temp5.y = chatBoxBackground.chatBoxHeight;
		chatBoxComponents.backgroundImage.rectTransform.sizeDelta = temp5;
		
		Vector3 temp6;
		temp6.x = chatBoxBackground.chatBoxPosX;
		temp6.y = chatBoxBackground.chatBoxPosY;
		temp6.z = 0f;
		chatBoxComponents.backgroundImage.rectTransform.localPosition = temp6;
		
		chatBoxHeader.rotation = Quaternion.Euler(chatBoxBackground._Rotation.x, chatBoxBackground._Rotation.y, chatBoxBackground._Rotation.z);
		chatBoxComponents.backgroundImage.rectTransform.localRotation = chatBoxHeader.rotation;
		///
		//Chat Box Buttons
		///
		for (int i = 0; i < chatBoxButtons.Length; i++) {
			float size2 = Screen.height * chatBoxButtons[i].textSize;
			chatBoxComponents.buttonComponents[i].buttonsText.fontSize = (int)size2;
			
			chatBoxButtons[i].chatBoxWidth = Screen.width * chatBoxButtons[i].width;
			chatBoxButtons[i].chatBoxHeight = Screen.height * chatBoxButtons[i].height;
			chatBoxButtons[i].chatBoxPosX = Screen.width * chatBoxButtons[i].positionX;
			chatBoxButtons[i].chatBoxPosY = Screen.height * chatBoxButtons[i].positionY;
			
			Vector2 temp7;
			temp7.x = chatBoxButtons[i].chatBoxWidth;
			temp7.y = chatBoxButtons[i].chatBoxHeight;
			chatBoxComponents.buttonComponents[i].buttonsImage.rectTransform.sizeDelta = temp7;
			
			Vector3 temp8;
			temp8.x = chatBoxButtons[i].chatBoxPosX;
			temp8.y = chatBoxButtons[i].chatBoxPosY;
			temp8.z = 0f;
			chatBoxComponents.buttonComponents[i].buttonsImage.rectTransform.localPosition = temp8;
			
			chatBoxButtons[i].rotation = Quaternion.Euler (chatBoxButtons[i]._Rotation.x, chatBoxButtons[i]._Rotation.y, chatBoxButtons[i]._Rotation.z);
			chatBoxComponents.buttonComponents[i].buttonsImage.rectTransform.localRotation = chatBoxButtons[i].rotation;
			
		}
	}
}
