using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TowerAttributes : ScriptableObject
{
    public Sprite icon;
    public int baseCost;
    public int baseUpgradeCost;
    public float range;
    public float firingRate;
    public int baseDamage;
    public int damagePerUpgrade;
    public float projectileSpeed;
    public Projectile projectile;
    [Header("Special Properties")]
    public float areaEffect = 0;
    public bool ignoreArmor = false;
}
