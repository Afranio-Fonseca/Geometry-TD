using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI display;
    [SerializeField] GameState gameState;

    public void UpdateDisplay()
    {
        display.text = "HP: " + gameState.PlayerHealth;
    }
}
