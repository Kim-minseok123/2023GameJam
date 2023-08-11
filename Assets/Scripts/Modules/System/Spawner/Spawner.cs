using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    protected GameObject spawnPrefab;

    [SerializeField]
    protected Transform spawnPoint;

    [SerializeField]
    protected bool isSpawnByStart = false;

    protected virtual void Start()
    {
        if (isSpawnByStart)
        {
            Spawn();
        }
    }

    public virtual void Spawn()
    {
        var cloneObject = Instantiate(spawnPrefab);
        cloneObject.transform.position = GetSpawnPoint().position;
    }

    public virtual Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

}
