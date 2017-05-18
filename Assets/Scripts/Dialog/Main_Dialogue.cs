using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;


public class Main_Dialogue : MonoBehaviour
{
    [Header("Canvas")]
    [Tooltip("This is where you add the main canvas prefab")]
    public Canvas dialog;

    public Image playerImage;
    public Text playerName;
    [Tooltip("The text the npc will say when you talk to him again")]
    public Text endText;
    [Tooltip("How many buttons you have in the canvas (Max 3)")]
    public Button[] mButtons;

    [Header("Main Text")]
    [Tooltip("If you need help ask Casey >.<")]
    public List<NpcText> mNpcText;


    private List<GameObject> mList;
    private PlayerStats playerStats;
    private bool running;
    private Text[] children;
    private int mHolder;
    private bool mEndTalk = false;
    private bool mDeleted = false;
    private int mIndex;

    public GameObject QuestPopUp;
    public GameObject PreviousQuest;
    public GameObject MiniMapIcon;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (running)// Makes sure Trigger is called once 
            {
                Debug.Log("Running");
                return;
            }
            playerStats = col.gameObject.GetComponent<PlayerStats>(); // Gets stats for player to change morality

            playerStats.DialogActive++;
            if (playerStats.DialogActive <= 1)
            {
                running = true;
            }
            else
            {
                return;
            }

            if (!mDeleted)
            {
                FillmList();
                GetFirstChildren(mIndex);
            }
        }
      

       
    }

    void Start()

    {
        mList = new List<GameObject>();
        for (int i = 0; i < mNpcText[mIndex].mbuttons; ++i)
        {
            mButtons[i].GetComponentInChildren<Text>().text = mNpcText[mIndex].buttonText[i];
        }
    }


    void Update()
    {
        if (running)
        {
            for (int i = 0; i < mNpcText.Capacity - 1; i++)
            {
                if (mNpcText[i].mbuttons != mNpcText[i + 1].canvas.Length)
                {
                    Debug.Log("YOU FUCKING aDSFAFDKGA");
                }
            }
            if (mEndTalk && !mDeleted) //Deletion
            {
                for (int i = 0; i < mNpcText.Capacity; ++i)
                {
                    for (int j = 0; j < mNpcText[i].canvas.Length; ++j)
                        Destroy(mNpcText[i].canvas[j].gameObject);
                }
                for (int i = 0; i < mButtons.Length; i++)
                {
                    Destroy(mButtons[i].gameObject);
                }
                mDeleted = true;
                if(QuestPopUp != null)
                {
                    PreviousQuest.SetActive(false);
                    QuestPopUp.SetActive(true);
                }
                if (MiniMapIcon != null)
                {
                    MiniMapIcon.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (!dialog.gameObject.activeSelf)

                {
                    dialog.gameObject.SetActive(true);
                    playerImage.gameObject.SetActive(true);
                    playerName.gameObject.SetActive(true);
                    mNpcText[0].Person.gameObject.SetActive(true);
                    mNpcText[0].name.gameObject.SetActive(true);
                }
            }
            TextUpdater(children);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            dialog.gameObject.SetActive(false);
            playerStats.DialogActive--;
        }

        running = false;

    }

    void TextUpdater(Text[] text) //Scrolls through the text in canvas
    {
        if (!mEndTalk)
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (mHolder != text.Length)
                {
                    if (mHolder - 1 != -1)
                    {
                        text[mHolder - 1].gameObject.SetActive(false); // Break
                    }
                    text[mHolder].gameObject.SetActive(true);
                    ++mHolder;

                }
                else
                {
                    for (int i = 0; i < mNpcText[mIndex].mbuttons; i++)
                    {
                        mButtons[i].gameObject.SetActive(true);
                    }
                }
            }
    }

    void FillmList()

    {
        for (int i = 0; i <= mNpcText.Capacity - 1; ++i)
        {
            for (int j = 0; j < mNpcText[i].canvas.Length; j++)
            {
                mList.Add(mNpcText[i].canvas[j].gameObject);
            }
        }

    }

    void GetFirstChildren(int i)

    {
        for (int j = 0; j < mNpcText[i].canvas.Length; j++)
        {
            mList[i].SetActive(true); //Set the canvas on
            children = mList[i].GetComponentsInChildren<Text>(true); // Putting the Children into array
            foreach (Text text in children) ; //Needs to be here if the text is disabled
        }
    }
    void GetChildrenMult(int i)
    {
        children = mNpcText[mIndex + 1].canvas[i].GetComponentsInChildren<Text>(true); // Putting the Children into array
        foreach (Text text in children) ; //Needs to be here if the text is disabled


    }

    #region ButtonClicks
    public void ButtonClick(int buttonIndex)
    {
        switch (buttonIndex)

        {
            case 0:
                playerStats.Morality += mNpcText[mIndex].morality[0];
                break;
            case 1:
                playerStats.Morality += mNpcText[mIndex].morality[1];
                break;
            case 2:
                playerStats.Morality += mNpcText[mIndex].morality[2];
                break;
            default:
                break;
        }

        if (mIndex + 1 == mNpcText.Capacity)
        {
            mEndTalk = true;
            mNpcText[mIndex].canvas[buttonIndex].gameObject.SetActive(false);
            foreach (Button b in mButtons)
            {
                b.gameObject.SetActive(false);
            }
            playerImage.gameObject.SetActive(false);
            playerName.gameObject.SetActive(false);
            endText.gameObject.SetActive(true);
        }
        else
        {
            mHolder = 1;

            Debug.Log(mList[mIndex].name);
            mList[mIndex].SetActive(false);
            mNpcText[mIndex].Person.gameObject.SetActive(false);
            mNpcText[mIndex].name.gameObject.SetActive(false);
            mNpcText[mIndex + 1].Person.gameObject.SetActive(true);
            mNpcText[mIndex + 1].name.gameObject.SetActive(true);
            for (int i = 0; i < mNpcText.Capacity; ++i)
            {
                for (int j = 0; j < mNpcText[mIndex].canvas.Length; j++)
                {
                    mNpcText[mIndex].canvas[j].gameObject.SetActive(false);
                }
            }
            mNpcText[mIndex + 1].canvas[buttonIndex].gameObject.SetActive(true);

            Array.Clear(children, 0, children.Length);
            GetChildrenMult(buttonIndex);
            children[0].gameObject.SetActive(true);
            ++mIndex;
            for (int i = 0; i < mNpcText[mIndex].mbuttons; ++i)
            {
                mButtons[i].GetComponentInChildren<Text>().text = mNpcText[mIndex].buttonText[i];
            }
            foreach (Button b in mButtons)
            {
                b.gameObject.SetActive(false);
            }

        }
    }
    #endregion


}
#region NpcText Class
[System.Serializable]
public class NpcText
{
    [Tooltip("Image of the person you are talking with")]
    public Image Person;
    [Tooltip("Name of the person you are talking with")]
    public Text name;
    [Tooltip("Canvas to be turned on")]
    public Canvas[] canvas;
    [Tooltip("The text in each button")]
    public string[] buttonText;
    [Tooltip("How many buttons you want (Max 3)")]
    public int mbuttons;
    [Tooltip("How much morality each button gives")]
    public int[] morality;

}
#endregion