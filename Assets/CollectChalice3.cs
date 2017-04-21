using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectChalice3 : MonoBehaviour
{
    public Renderer rend;
    bool cup3Collected = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && CupCollection.cupCount >= 3 && cup3Collected == false)
        {
            PuzzleWall.cupsDelivered += 1;
            rend.enabled = true;
            cup3Collected = true;
        }
    }
}
