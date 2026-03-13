using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    private Dictionary<GameObject, GameObject> pooledObjectOrigin = new Dictionary<GameObject, GameObject>();

    [Header("Pool Settings")]
    [SerializeField] private GameObject[] prefabToPool;
    [SerializeField] private int poolSize = 10;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (var prefab in prefabToPool)
        {
            InitializePool(prefab);

        }
    }
    

    private void InitializePool(GameObject prefab)
    {
        poolDictionary[prefab] = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject(prefab);
        }
    }

    private void CreateNewObject(GameObject prefab)
    {
        // Instantiate a new object from the prefab but set to inactive and parent it to the PoolManager
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);

        // Add the new object to the pool
        poolDictionary[prefab].Enqueue(obj);

        // Reference to the original prefab of the object
        pooledObjectOrigin[obj] = prefab;
    }

    public GameObject GetObject(GameObject prefab, Transform target)
    {
        // If the pool for the requested prefab doesn't exist, initialize it
        if (poolDictionary.ContainsKey(prefab) == false)
                InitializePool(prefab);

        // If the pool is empty, create a new object to ensure we always have one available
        int minimumPoolSize = 3; // You can adjust this value based on your needs
        if (poolDictionary[prefab].Count < minimumPoolSize)
        {
            CreateNewObject(prefab);
        }

        // Get an object from the pool
        GameObject objectToGet = poolDictionary[prefab].Dequeue();

        // Set parent to null, determine position and activate the object 
        objectToGet.transform.parent = null;
        objectToGet.transform.position = target.position;
        objectToGet.SetActive(true);

        return objectToGet;
    }

    public void ReturnObject (GameObject objToReturn, float delay = 0.001f)
    {
        StartCoroutine(ReturnToPool(objToReturn, delay));
    }

    private IEnumerator ReturnToPool (GameObject objToReturn, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        // Get the original prefab of the object being returned
        GameObject originalPrefab  = pooledObjectOrigin[objToReturn];
        
        objToReturn.SetActive(false);
        objToReturn.transform.parent = transform;

        // Add the returned object back to the pool
        poolDictionary[originalPrefab].Enqueue(objToReturn);
    }
}

