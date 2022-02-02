using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class Pool
    {
        public int index;
        public GameObject prefab;
        public int size;
    }

    #region Singleton

    protected override void Awake()
    {
        base.Awake();
    }
    
    #endregion

    public List<Pool> pools;
    public Dictionary<int, Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.index, objectPool);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(poolDictionary[0].Count);
        }
    }

    public GameObject SpawnFromPool(int index, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(index))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[index].Dequeue();
        if (objectToSpawn == null) return null;
        objectToSpawn.transform.gameObject.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledParticle = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledParticle != null) pooledParticle.OnObjectSpawn();

        poolDictionary[index].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}