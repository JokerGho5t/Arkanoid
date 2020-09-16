using System;
using System.Collections.Generic;

using UnityEngine;

using JokerGho5t.ObjectPool;


public static class PoolManager
{
    public static bool logStatus = true;

    private static Transform root;

    private static Dictionary<GameObject, ObjectPool<GameObject>> Pools = new Dictionary<GameObject, ObjectPool<GameObject>>();
    private static Dictionary<GameObject, ObjectPool<GameObject>> instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();

    private static GameObject InstantiatePrefab(GameObject prefab)
    {
        var go = UnityEngine.Object.Instantiate(prefab);
        if (root != null) go.transform.parent = root;
        go.SetActive(false);
        return go;
    }

    #region Static API

    public static int GetCountObjectInPool(GameObject prefab)
    {
        int count = 0;

        if (Pools.ContainsKey(prefab))
        {
            count = Pools[prefab].CountInQueue;
        }
        else
        {
            Debug.LogWarning("No pool contains the object: " + prefab.name);
        }

        return count;
    }

    public static void SetRoot(Transform Root)
    {
        root = Root;
    }

    public static void PrintStatus()
    {
        foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> keyVal in Pools)
        {
            Debug.Log(string.Format("Object Pool for Prefab: {0} in queue {1}", keyVal.Key.name, keyVal.Value.CountInQueue));
        }
    }

    public static void WarmPool(GameObject prefab, int size)
    {
        if (Pools.ContainsKey(prefab))
        {
            throw new Exception("Pool for prefab " + prefab.name + " has already been created");
        }

        var pool = new ObjectPool<GameObject>(() => { return InstantiatePrefab(prefab); }, size);
        Pools[prefab] = pool;

        if (logStatus)
        {
            PrintStatus();
        }
    }

    public static GameObject SpawnObjectOriginalTransform(GameObject prefab)
    {
        if (!Pools.ContainsKey(prefab))
        {
            WarmPool(prefab, 1);
        }

        var pool = Pools[prefab];

        var clone = pool.GetItem();
        clone.SetActive(true);

        instanceLookup.Add(clone, pool);

        if (logStatus)
        {
            PrintStatus();
        }

        return clone;
    }

    public static GameObject SpawnObject(GameObject prefab)
    {
        return SpawnObject(prefab, Vector3.zero, Quaternion.identity);
    }

    public static GameObject SpawnObject(GameObject prefab, Vector3 position)
    {
        return SpawnObject(prefab, position, Quaternion.identity);
    }

    public static GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!Pools.ContainsKey(prefab))
        {
            WarmPool(prefab, 1);
        }

        var pool = Pools[prefab];

        var clone = pool.GetItem();
        clone.transform.position = position;
        clone.transform.rotation = rotation;
        clone.SetActive(true);

        instanceLookup.Add(clone, pool);

        if (logStatus)
        {
            PrintStatus();
        }

        return clone;
    }

    public static void ReleaseObject(GameObject clone)
    {
        clone.SetActive(false);

        if (instanceLookup.ContainsKey(clone))
        {
            instanceLookup[clone].ReleaseItem(clone);
            instanceLookup.Remove(clone);

            if (logStatus)
            {
                PrintStatus();
            }
        }
        else
        {
            Debug.LogWarning("No pool contains the object: " + clone.name);
        }
    }

    #endregion
}


