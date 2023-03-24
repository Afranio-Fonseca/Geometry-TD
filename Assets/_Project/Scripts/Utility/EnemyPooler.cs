using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    [SerializeField] Enemy enemyToPool;
    [SerializeField] int startAmount;
    List<Enemy> pooledObjects;

    private void Start()
    {
        pooledObjects = new List<Enemy>();
        for (int c = 0; c < startAmount; c++)
        {
            InstantiatePoolObject();
        }
    }

    Enemy InstantiatePoolObject()
    {
        Enemy go = Instantiate(enemyToPool, transform).GetComponent<Enemy>();
        go.gameObject.SetActive(false);
        pooledObjects.Add(go);
        return go;
    }

    public Enemy GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return InstantiatePoolObject();
    }

    public void StopPool()
    {
        foreach(Enemy enemy in pooledObjects)
        {
            enemy.StopAction();
        }
    }
}