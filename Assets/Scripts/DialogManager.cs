using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;


public class DialogManager : MonoBehaviour
{
    public Canvas dialog;
    public List<NpcText> npcText;
    private List<GameObject> mList;
    //public List<PlayerText> playerText;
    private PlayerStats playerStats;

    private bool Running;
    private Text[] Children;

    private int holder;

    struct Helper
    {
        Canvas holder;
        int adder;
    }

    void OnTriggerEnter(Collider col)
    {
        playerStats = col.GetComponent<PlayerStats>();
        Running = true;
        GetChildren(0);
    }

    void Start()
    {
        mList = new List<GameObject>();
    }

    void Update()
    {
        if (Running)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dialog.gameObject.activeSelf)
                {
                    Debug.Log("IsActive");
                    dialog.gameObject.SetActive(true);
                }
            }
            TextUpdater(Children);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            dialog.gameObject.SetActive(false);
        }
        Running = false;
    }

    void TextUpdater(Text[] text)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("TextUpdater");
            if (holder != text.Length)
            {
                if (holder - 1 != -1)
                {
                    text[holder - 1].gameObject.SetActive(false);
                }
                text[holder].gameObject.SetActive(true);
                ++holder;
            }
            else
            {
                //Turn on buttons
                foreach (Button button in npcText[0].button)
                {
                    button.gameObject.SetActive(true);
                }
            }
        }
    }

    //void GetFirstCanvas()
    //{
    //    for (int i = 0; i <= npcText.Capacity - 1; ++i)
    //    {
    //        for (int j = 0; j < npcText[i].canvas.Length; j++)
    //        {
    //            mList.Add(npcText[i].canvas[j].gameObject);
    //            mList[i].SetActive(true);
    //            Children = mList[i].GetComponentsInChildren<Text>(true);
    //            foreach (Text text in Children) ;
    //        }
    //    }
    //}

    void GetChildren(int i)
    {
        for (int j = 0; j < npcText[i].canvas.Length; j++)
        {
            mList.Add(npcText[i].canvas[j].gameObject);
            mList[i].SetActive(true);
            Children = mList[i].GetComponentsInChildren<Text>(true);
            foreach (Text text in Children) ;
        }
    }

    void SetChildren(int index)
    {
        Children = mList[index].GetComponentsInChildren<Text>(true);
        foreach (Text text in Children) ;
    }

    void AddmList(int index, int j)
    {
        mList.Add(npcText[index].canvas[j].gameObject);
    }

    int mListGetLength()
    {
        return mList.Capacity;
    }

    int mListChildGetLength(int index)
    {
        return mList[index].GetComponentsInChildren<Text>().Length;
    }

    void Test()
    {
        for (int i = 0; i < mListGetLength();)
        {
            AddmList(i, i); // 
            if (mListChildGetLength(i) == holder)
            {

            }
        }
    }

    #region ButtonClicks
    public void ButtonOneClick(int morailty)
    {
        npcText[1].canvas[0].gameObject.SetActive(true);
        //Delete Button
        //Spawn new canvas connected to this one
    }
    #endregion

}
#region NpcText Class
[System.Serializable]
public class NpcText
{
    public Canvas[] canvas;
    public Button[] button;
}
#endregion

//#region Answer Class
//[System.Serializable]
//public class PlayerText
//{
//    public Button[] text;
//}
//#endregion
