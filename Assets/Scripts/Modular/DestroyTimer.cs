using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public bool startTimerOnStart = true;
    public float killTime;

    private void Start()
    {
        if (startTimerOnStart)
            StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(IEKillTimer());
    }
    private IEnumerator IEKillTimer()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(gameObject);
    }
}
