using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPooler[] enemyPools;
    [SerializeField] float spawnCooldown;
    [SerializeField] float waveCooldown;
    [SerializeField] Element[] elementsAvailable;
    [SerializeField] SpriteShapeController path;
    [SerializeField] float startingWaveLength;
    [SerializeField] float waveLengthGrowth;
    [SerializeField] float timeBetweenWaves;
    /// <summary>
    /// The size of the current wave
    /// </summary>
    float effectiveWaveLength { get { return startingWaveLength + (waveLengthGrowth * (currentWave - 1)); } }
    /// <summary>
    /// The number of the current wave
    /// </summary>
    int currentWave = 1;
    /// <summary>
    /// auxiliary variable for counting time
    /// </summary>
    float spawnTime = 0f;
    bool spawnEnabled = false;
    /// <summary>
    /// How many weight the enemies in the current wave add up to until this moment
    /// </summary>
    float currentWaveWeight = 0;
    float waveCooldownCounter;


    Element randomElement { get { return elementsAvailable[Random.Range(0, elementsAvailable.Length)]; } }

    public void StartSpawning()
    {
        spawnTime = spawnCooldown;
        currentWaveWeight = 0;
        waveCooldownCounter = waveCooldown;
        spawnEnabled = true;
    }

    public void StopPools()
    {
        foreach(EnemyPooler pool in enemyPools)
        {
            pool.StopPool();
        }
    }

    private void FixedUpdate()
    {
        if (!spawnEnabled) return;
        spawnTime -= Time.deltaTime;
        if(spawnTime <= 0 && currentWaveWeight < effectiveWaveLength)
        {
            //Gets random enemy from available pools and adds its weight to the wave weight
            EnemyPooler enemyPool = RandomPool();

            // spawns the enemy
            Enemy enemy = enemyPool.GetPooledObject();
            currentWaveWeight += enemy.Attributes.weight;
            enemy.Initialize(currentWave, randomElement, path);
            enemy.gameObject.SetActive(true);
            spawnTime = spawnCooldown;
        } else if(currentWaveWeight >= effectiveWaveLength)
        {
            waveCooldownCounter -= Time.deltaTime;
            if(waveCooldownCounter <= 0)
            {
                currentWave++;
                currentWaveWeight = 0;
                waveCooldownCounter = waveCooldown;
            }
        }
    }


    EnemyPooler RandomPool()
    {
        return enemyPools[Random.Range(0, enemyPools.Length)];
    }
}
