using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI display;
    [SerializeField] GameState gameState;

    public void UpdateDisplay()
    {
        display.text = "Current score: " + gameState.Points;
    }
}
