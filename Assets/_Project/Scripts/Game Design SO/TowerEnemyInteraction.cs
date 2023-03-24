using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemyInteraction : ScriptableObject
{
    public float elementAdvantageMultiplier;
    public float elementDisadvantageMultiplier;

    public int CalculateTowerDamage(Tower tower, Enemy enemy)
    {
        float elementModifier = 1f;
        if (tower.CurrentElement && Array.IndexOf(tower.CurrentElement.advantages, enemy.Element) > -1)
        {
            elementModifier = elementAdvantageMultiplier;
        }
        else if (tower.CurrentElement && Array.IndexOf(tower.CurrentElement.disadvantages, enemy.Element) > -1)
        {
            elementModifier = elementDisadvantageMultiplier;
        }
        float armorModifier = 1;
        if (!tower.Attributes.ignoreArmor && enemy.Attributes.armor > 0)
        {
            armorModifier = 1 - Mathf.Log(enemy.Attributes.armor, 100);
        }
        return Mathf.RoundToInt(tower.EffectiveTowerDamage * armorModifier * elementModifier);
    }
}
