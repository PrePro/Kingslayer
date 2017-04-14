using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWall : MonoBehaviour
{
    Vector3 moveDown = new Vector3(0f, -0.02f, 0f);
    bool puzzleComplete = false;
    int moveDownDistance;
    public static int cupsDelivered = 0;

    public int totalCups = 0;

    void Start ()
    {
	}
	
	void Update ()
    {
        if(cupsDelivered >= totalCups)
        {
            puzzleComplete = true;
        }

	    if ( puzzleComplete == true && moveDownDistance < 285 )
        {
            transform.position += moveDown;
            moveDownDistance += 1;
        }
        
	}

}
