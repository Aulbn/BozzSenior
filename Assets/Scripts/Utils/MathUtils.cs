using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils : MonoBehaviour
{
    public static int Mod(int value, int mod)
    {
        return (value % mod + mod) % mod;
    }
}
