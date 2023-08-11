using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleSpawner : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> spawnPrefabList;
    [SerializeField]
    protected List<Transform> spawnPointList;

    [SerializeField]
    protected bool isSpawnByStart = false;
    [SerializeField]
    protected bool useRandomSpawn = false;
    [SerializeField]
    protected bool useAutoSpawnByTime = false;
    [SerializeField]
    protected AmountRangeFloat spawnTimeRange;

    protected float currentSpawnTime;

    protected virtual void Start()
    {
        if (isSpawnByStart)
        {
            Spawn();
        }

        if (useAutoSpawnByTime)
        {
            currentSpawnTime = spawnTimeRange.GetRandomAmount();
        }
    }

    protected virtual void Update()
    {
        if (useAutoSpawnByTime)
        {
            currentSpawnTime -= Time.deltaTime;

            if (currentSpawnTime <= 0)
            {
                currentSpawnTime = spawnTimeRange.GetRandomAmount();
                Spawn();
            }
        }
    }

    public virtual List<GameObject> Spawn()
    {
        var spawnObjectList = new List<GameObject>();
        if (useRandomSpawn)
        {
            var spawnPoint = GetRandomSpawnPoint();
            spawnObjectList.Add(CreateSpawnObject(spawnPoint.position, spawnPoint.rotation));
        }
        else
        {
            for (var i = 0; i < spawnPointList.Count; ++i)
            {
                var spawnPoint = spawnPointList[i];
                spawnObjectList.Add(CreateSpawnObject(spawnPoint.position, spawnPoint.rotation));
            }
        }

        return spawnObjectList;
    }

    protected virtual GameObject CreateSpawnObject()
    {
        return Instantiate(GetRandomPrefab());
    }

    protected virtual GameObject CreateSpawnObject(Vector3 position, Quaternion rotation)
    {
        return Instantiate(GetRandomPrefab(), position, rotation);
    }

    protected virtual Transform GetRandomSpawnPoint()
    {
        return spawnPointList[Random.Range(0, spawnPointList.Count)];
    }

    protected virtual Transform GetSpawnPoint(int index)
    {
        return spawnPointList[index];
    }

    protected virtual GameObject GetRandomPrefab()
    {
        return spawnPrefabList[Random.Range(0, spawnPrefabList.Count)];
    }

    #if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (var i = 0; i < spawnPointList.Count; ++i)
        {
            Gizmos.DrawCube(spawnPointList[i].position, Vector3.one);
        }
    }
    #endif
}
