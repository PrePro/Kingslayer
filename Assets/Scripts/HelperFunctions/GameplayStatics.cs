//======================================================================================================
// GameplayStatics.cs
// Description: Static helper functions for different algorithms that tend to be used often
// Author: Reynald Brassard
//======================================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameplayStatics
{
    public static bool IsFacing(Transform transform, Vector3 other, float accuracy = 0.8f)
    {
        if(Vector3.Distance(transform.position, other) < 0.5f)
        {
            accuracy = 0.6f;
            //Debug.Log(accuracy);
            Vector3 dir = Vector3.Normalize(other - transform.position);

            float checka = Vector3.Dot(transform.forward, dir);
            Debug.Log(checka);
            return checka > accuracy;

        }
        accuracy = (accuracy > 1.0f) ? 1.0f : accuracy;
        //Debug.Log(accuracy);
        Vector3 dirToOther = Vector3.Normalize(other - transform.position);
  
        float check = Vector3.Dot(transform.forward, dirToOther);
        //Debug.Log(check);
        return check > accuracy;


    }

    public static bool IsWithinRange2D(Transform transform, Vector3 other, float range, float buffer = 0.0f)
    {
        other.y = transform.position.y;
        return Vector3.Distance(transform.position, other) < (range - buffer);
    }
}
