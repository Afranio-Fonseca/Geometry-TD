using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    [SerializeField] Projectile projectileToPool;
    [SerializeField] int startAmount;
    List<Projectile> pooledObjects;

    public void SetProjectileToPool(Projectile projectile)
    {
        projectileToPool = projectile;
        pooledObjects = new List<Projectile>();
        for (int c = 0; c < startAmount; c++)
        {
            InstantiatePoolObject();
        }
    }

    Projectile InstantiatePoolObject()
    {
        Projectile go = Instantiate(projectileToPool).GetComponent<Projectile>();
        go.gameObject.SetActive(false);
        pooledObjects.Add(go);
        return go;
    }

    public Projectile GetPooledObject()
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
}
