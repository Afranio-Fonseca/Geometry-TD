using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI display;
    [SerializeField] GameState gameState;

    public void UpdateDisplay()
    {
        display.text = "Money: " + gameState.currencyAmount;
    }
}
