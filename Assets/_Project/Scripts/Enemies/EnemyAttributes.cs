using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyAttributes : ScriptableObject
{
    public string enemyName;
    /// <summary>
    /// Health of this enemy when he is spawned
    /// </summary>
    public int startingHealth;
    /// <summary>
    /// Health added for each wave deep the player is in the current game
    /// </summary>
    public int healthPerLevel;
    /// <summary>
    /// multiplies the damage this enemy takes by log(armor, 100) if the tower doesn't ignore armor.
    /// </summary>
    [Range(0, 99)]
    public int armor;
    /// <summary>
    /// Speed at which this enemy moves through the path
    /// </summary>
    public float speed;
    /// <summary>
    /// Amount of gold the player receives when defeating this enemy
    /// </summary>
    public int bounty;
    /// <summary>
    /// additional gold for each wave deep the player is in the current game
    /// </summary>
    public int bountyPerLevel;
    /// <summary>
    /// Amount of points the player receives when defeating this enemy
    /// </summary>
    public int pointsWorthBase;
    /// <summary>
    /// additional points for each wave deep the player is in the current game
    /// </summary>
    public int pointsWorthPerLevel;
    /// <summary>
    /// How many units this enemy is worth in the wave counting
    /// </summary>
    public float weight;
}
