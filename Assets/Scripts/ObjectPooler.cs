using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<Pool> poolList = null;
    private readonly Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    [System.Serializable]
    public class Pool
    {
        public string id = "";
        public Queue<GameObject> gameObjects = new Queue<GameObject>();
        public GameObject prefab;
        public int numberToSpawn = 0;
        public Transform parentObject = null;
    }

    public void Awake()
    {
        foreach(Pool p in poolList)
        {
            pools.Add(p.id, p);

            for(int i = 0; i < p.numberToSpawn; i++)
            {
                GameObject g = Instantiate(p.prefab);
                if(p.parentObject != null)
                {
                    g.transform.SetParent(p.parentObject.transform);
                }

                g.SetActive(false);

                p.gameObjects.Enqueue(g);
            }
        }
    }

    public GameObject SpawnGameObject(Vector2 pos, Quaternion rot, string poolId)
    {
        if (!pools.ContainsKey(poolId))
        {
            Debug.LogError("Object Pooler does not contain key " + poolId);
            return null;
        }

        Pool pool = pools[poolId];
        GameObject gameObject = pool.gameObjects.Dequeue();
        gameObject.transform.position = pos;
        gameObject.transform.rotation = rot;

        gameObject.SetActive(true);
        pool.gameObjects.Enqueue(gameObject);

        return gameObject;
    }
}
