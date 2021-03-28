using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 Flattened(this Vector3 thisVector)
    {
        return new Vector3(thisVector.x, 0, thisVector.z);
    }
}
