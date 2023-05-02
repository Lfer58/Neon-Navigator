using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Taken from https://learn.unity.com/tutorial/introduction-to-object-pooling
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;

    [SerializeField]
    private GameObject objectToPool;

    private int amountToPool = 7;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject temp = Instantiate(objectToPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
        // Creates a list of objects to be pooled as specified by the amountToPool variable
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject instanceObject in pooledObjects)
        {
            if (!instanceObject.activeInHierarchy)
            {
                return instanceObject;
            }
        }
        return null;
    }
}