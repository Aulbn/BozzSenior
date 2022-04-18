using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale *= 1.5f;
            Debug.Log($"Timescale = {Time.timeScale}");
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale /= 1.5f;
            Debug.Log($"Timescale = {Time.timeScale}");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            Time.timeScale = 1f;
            Debug.Log($"Timescale = {Time.timeScale}");
        }
    }
}
