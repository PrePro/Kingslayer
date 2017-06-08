using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBroker : MonoBehaviour
{
    public Main_Dialogue MD;
    public GameObject StairOne;
    public GameObject StairTwo;
    private bool CallOnce = false;

    void Update()
    {
        if (MD.mEndTalk && !CallOnce)
        {
            StairOne.SetActive(true);
            StairTwo.SetActive(false);
            CallOnce = true;
        }
    }
}
