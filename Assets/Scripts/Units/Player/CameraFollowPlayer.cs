//======================================================================================================
// CameraFollowPlayer.cs
// Description:
// Author: Casey Stewart
//======================================================================================================
using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

    public GameObject player;  
    private Vector3 offset;

    bool followPlayer;

    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        followPlayer = true;
    }

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (followPlayer == true)
        transform.position = player.transform.position + offset;
    }


}
