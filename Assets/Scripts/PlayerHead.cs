using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour {

    Movement movement;
    void Start()
    {
        movement = GetComponentInParent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.isCrouching)
        {
            transform.position = new Vector3(Player.Position.x, Player.Position.y, Player.Position.z);
        }
        else
        {
            transform.position = new Vector3(Player.Position.x, Player.Position.y + 2, Player.Position.z);
        }
    }
}
