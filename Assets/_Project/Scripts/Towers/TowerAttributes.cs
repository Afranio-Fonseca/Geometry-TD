using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TowerAttributes : ScriptableObject
{
    /// <summary>
    /// Icon that represents this specific tower
    /// </summary>
    public Sprite icon;
    /// <summary>
    /// Cost to build this tower on an empty site
    /// </summary>
    public int baseCost;
    /// <summary>
    /// Base cost to upgrade this tower, this value is multiplied by the current level of the tower to determine upgrade cost
    /// </summary>
    public int baseUpgradeCost;
    /// <summary>
    /// Distance the tower is able to target enemies, this range represent 1 unit in world space for every 10 on its value
    /// </summary>
    public float range;
    /// <summary>
    /// Amount of times this tower can shoot per second
    /// </summary>
    public float firingRate;
    /// <summary>
    /// Base damage that this tower deals to enemies
    /// </summary>
    public int baseDamage;
    /// <summary>
    /// Extra damage that is added to the base damage for each level the tower was upgraded
    /// </summary>
    public int damagePerUpgrade;
    /// <summary>
    /// Speed at which the projectile moves towards the target after being shot
    /// </summary>
    public float projectileSpeed;
    /// <summary>
    /// Monobehaviour with the projectile component to be instantiated in a object pooler
    /// </summary>
    public Projectile projectile;
    /// <summary>
    /// When the projectile ends it's trajectory instead of dealing damage only to a collided enemy it raycasts a circle around its position with a radius equal to this parameter.
    /// </summary>
    [Header("Special Properties")]    
    public float areaEffect = 0;
    /// <summary>
    /// If true the damage of the tower will not be reduced by the enemy's armor value
    /// </summary>
    public bool ignoreArmor = false;
}
