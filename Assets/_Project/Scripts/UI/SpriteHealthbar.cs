using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHealthbar : MonoBehaviour
{
    [SerializeField] Transform lifeTransform;
    int maxHealth;
    
    public void SetMaxHealth(int value)
    {
        maxHealth = value;
        lifeTransform.transform.localScale = Vector3.one;
    }

    public void UpdateHealthbar(int healthValue)
    {
        lifeTransform.transform.localScale = new Vector3((float) healthValue / maxHealth, 1, 1);
    }
}
