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
    public static bool IsFacing(Transform transform, Vector3 other, float accuracy = 0.95f)
    {
        if (accuracy > 1.0f)
        {
            accuracy = 1.0f;
        }
        Vector3 dirToOther = Vector3.Normalize(other - transform.position);
        if(Vector2.Dot(transform.forward, dirToOther) > accuracy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsWithinRange2D(Transform transform, Vector3 other, float range, float buffer = 0.0f)
    {
        other.y = transform.position.y;
        return Vector3.Distance(transform.position, other) < (range - buffer);
    }
}
