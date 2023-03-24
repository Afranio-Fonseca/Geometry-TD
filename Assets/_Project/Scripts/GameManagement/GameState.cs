using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : ScriptableObject
{
    [SerializeField] StartingParameteres startingParameteres;

    private int currency;
    public int currencyAmount { get { return currency; } }
    private int points;
    public int Points { get { return points; } }
    private int playerHealth;
    public int PlayerHealth { get { return playerHealth; } }

    public void Initialize()
    {
        currency = startingParameteres.startingCurrency;
        playerHealth = startingParameteres.startingPlayerHealth;
        points = 0;
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if(amount > currency)
        {
            return false;
        } else
        {
            currency -= amount;
            return true;
        }
    }
    public void AddPoints(int amount)
    {
        points += amount;
    }

    public bool DamagePlayer()
    {
        playerHealth--;
        return playerHealth > 0;
    }
}
