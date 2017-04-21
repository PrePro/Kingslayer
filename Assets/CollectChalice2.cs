using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectChalice2 : MonoBehaviour
{
    public Renderer rend;
    bool cup2Collected = false;

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
        if (col.tag == "Player" && CupCollection.cupCount >= 2 && cup2Collected == false)
        {
            PuzzleWall.cupsDelivered += 1;
            rend.enabled = true;
            cup2Collected = true;
        }
    }
}
