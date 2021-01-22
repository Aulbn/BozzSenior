using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable] public struct DropObject
    {
        public GameObject prefab;
        public float dropRate;
    }

    public bool isSpawning;
    public Vector2 pauseInterval;

    public Transform[] spawnPoints;
    public DropObject[] dropObjects;

    private Coroutine SpawnLoop;

    private void Start()
    {
        SetActive(true);
    }

    private IEnumerator IESpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(pauseInterval.x, pauseInterval.y));
            if (isSpawning)
                Instantiate(GetDrop().prefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }

    private DropObject GetDrop()
    {
        float range = 0;
        foreach (DropObject o in dropObjects)
            range += o.dropRate;
        
        float seed = Random.Range(0, range);
        float current = 0;
        foreach (DropObject o in dropObjects)
        {
            if ((o.dropRate > current && o.dropRate < seed) || o.dropRate == seed)
                return o;

            current += o.dropRate;
        }

        return dropObjects[dropObjects.Length - 1];
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
            SpawnLoop = StartCoroutine(IESpawnLoop());
        else
        {
            if (SpawnLoop != null)
                StopCoroutine(SpawnLoop);
        }
    }
}
