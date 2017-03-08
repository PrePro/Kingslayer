using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System;


public class Main_Dialog : MonoBehaviour
{
    [Header("Canvas")]
    [Tooltip("This is where you add the main canvas prefab")]
    public Canvas dialog;

    public Image playerImage;
    public Text playerName;
    [Tooltip("The text the npc will say when you talk to him again")]
    public Text endText;
    [Tooltip("How many buttons you have in the canvas (Max 3)")]
    public Button[] mBottons;

    [Header("Main Text")]
    [Tooltip("If you need help ask Casey >.<")]
    public List<NpcText> npcText;

    private List<GameObject> mList;
    private PlayerStats playerStats;
    private bool running;
    private Text[] children;
    private int mHolder;
    private bool mEndTalk = false;
    private bool mDeleted = false;
    private int mIndex;

    void OnTriggerEnter(Collider col)
    {
        if (running) return; // Makes sure Trigger is called once 
        playerStats = col.GetComponent<PlayerStats>(); // Gets stats for player to change morality
        running = true;

        if(!mDeleted)
        {
            FillmList();
            GetFirstChildren(mIndex);    
        }
    }

    void Start()
    {
        mList = new List<GameObject>();
        for(int i = 0; i < npcText[mIndex].mbuttons; ++i)
        {
            mBottons[i].GetComponentInChildren<Text>().text = npcText[mIndex].buttonText[i];
        }
    }


    void Update()
    {
        if (running)
        {
            for (int i = 0; i < npcText.Capacity - 1; i++)
            {
               if(npcText[i].mbuttons != npcText[i+ 1].canvas.Length)
                {
                    Debug.Log("YOU FUCKING aDSFAFDKGA");
                }
            }
            if(mEndTalk && !mDeleted) //Deletion
            {
                for(int i =0; i < npcText.Capacity; ++i)
                {
                    for(int j =0; j < npcText[i].canvas.Length; ++j)
                    Destroy(npcText[i].canvas[j].gameObject);
                }
                for (int i = 0; i < mBottons.Length; i++)
                {
                    Destroy(mBottons[i].gameObject);
                }
                mDeleted = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (!dialog.gameObject.activeSelf)
                {
                    Debug.Log("IsActive");
                    dialog.gameObject.SetActive(true);
                    playerImage.gameObject.SetActive(true);
                    playerName.gameObject.SetActive(true);
                    npcText[0].Person.gameObject.SetActive(true);
                    npcText[0].name.gameObject.SetActive(true);
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
        }

        running = false;

    }

    void TextUpdater(Text[] text) //Scrolls through the text in canvas
    {
        if(!mEndTalk)
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
                for (int i = 0; i < npcText[mIndex].mbuttons; i++)
                {
                    mBottons[i].gameObject.SetActive(true);
                }
            }
        }
    }

    void FillmList()
    {
        for (int i = 0; i <= npcText.Capacity - 1; ++i)
        {
            for (int j = 0; j < npcText[i].canvas.Length; j++)
            {
                mList.Add(npcText[i].canvas[j].gameObject);
            }
        }
    }

    void GetFirstChildren(int i)
    {
        for (int j = 0; j < npcText[i].canvas.Length; j++)
        {
            mList[i].SetActive(true); //Set the canvas on
            children = mList[i].GetComponentsInChildren<Text>(true); // Putting the Children into array
            foreach (Text text in children) ; //Needs to be here if the text is disabled
        }
    }
    void GetChildrenMult(int i)
    {
        children = npcText[mIndex + 1].canvas[i].GetComponentsInChildren<Text>(true); // Putting the Children into array
        foreach (Text text in children) ; //Needs to be here if the text is disabled

    }

    #region ButtonClicks
    public void ButtonClick(int buttonIndex)
    {
        Debug.Log("ASDJAG");
        switch (buttonIndex)
        {
            case 0:
                playerStats.moralityAoe += npcText[mIndex].morality[0];
                break;
            case 1:
                playerStats.moralityAoe += npcText[mIndex].morality[1];
                break;
            case 2:
                playerStats.moralityAoe += npcText[mIndex].morality[2];
                break;
            default:
                break;
        }

        if (mIndex + 1 == npcText.Capacity)
        {
            Debug.Log("IF");
            mEndTalk = true;
            npcText[mIndex].canvas[buttonIndex].gameObject.SetActive(false);
            foreach (Button b in mBottons)
            {
                b.gameObject.SetActive(false);
            }
            playerImage.gameObject.SetActive(false);
            playerName.gameObject.SetActive(false);
            endText.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Button");
            mHolder = 1;

            Debug.Log(mList[mIndex].name);
            mList[mIndex].SetActive(false);
            npcText[mIndex].Person.gameObject.SetActive(false);
            npcText[mIndex].name.gameObject.SetActive(false);
            npcText[mIndex + 1].Person.gameObject.SetActive(true);
            npcText[mIndex + 1].name.gameObject.SetActive(true);
            for (int i =0; i < npcText.Capacity; ++i)
            {
                for (int j = 0; j < npcText[mIndex].canvas.Length; j++)
                {
                    npcText[mIndex].canvas[j].gameObject.SetActive(false);
                }
            }
            npcText[mIndex + 1].canvas[buttonIndex].gameObject.SetActive(true);

            Array.Clear(children, 0, children.Length);
            GetChildrenMult(buttonIndex);
            children[0].gameObject.SetActive(true);
            ++mIndex;
            for (int i = 0; i < npcText[mIndex].mbuttons; ++i)
            {
                mBottons[i].GetComponentInChildren<Text>().text = npcText[mIndex].buttonText[i];
            }
            foreach (Button b in mBottons)
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