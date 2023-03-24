using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyAttributes : ScriptableObject
{
    public string enemyName;
    public int startingHealth;
    public int healthPerLevel;
    [Range(0, 99)]
    public int armor;
    public float speed;
    public int bounty;
    public int bountyPerLevel;
    public int pointsWorthBase;
    public int pointsWorthPerLevel;
    /// <summary>
    /// How many units this enemy is worth in the wave counting
    /// </summary>
    public float weight;
}
