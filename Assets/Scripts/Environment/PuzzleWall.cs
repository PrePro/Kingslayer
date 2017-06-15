using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWall : MonoBehaviour
{
    Vector3 moveDown = new Vector3(0f, -0.02f, 0f);
    Vector3 moveUp = new Vector3(0f, +0.04f, 0f);
    bool puzzleComplete = false;
    int moveDownDistance;
    int moveUpDistance = 0;
    public static int cupsDelivered = 0;
    public static bool goBackUp = false;

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

        if (goBackUp == true && moveUpDistance < 250)
        {
            transform.position += moveUp;
            moveUpDistance += 1;
        }
        
	}

}
