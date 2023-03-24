using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    /// <summary>
    /// Hash to the Layermask of the Enemy layer
    /// </summary>
    public static int enemyLayerMask;

    /// <summary>
    /// Scriptable that defines how effective elements are over advantageous and disadvantageous elements
    /// </summary>
    [SerializeField] TowerEnemyInteraction interactionDesign;
    /// <summary>
    /// Game State holds the information of the most important variables in the game.
    /// </summary>
    [SerializeField] GameState gameState;
    /// <summary>
    /// Monobehaviour with the menu to build towers
    /// </summary>
    [SerializeField] ConstructMenu constructMenu;
    /// <summary>
    /// Monobehaviour with the menu to upgrade towers and enchant them
    /// </summary>
    [SerializeField] UpgradeMenu upgradeMenu;
    /// <summary>
    /// Does a fade in and fade out effect to the scene
    /// </summary>
    [SerializeField] FadingPanel fadingPanel;
    /// <summary>
    /// Panel that displays information to players new to the game
    /// </summary>
    [SerializeField] GameObject firstInfoPanel;
    /// <summary>
    /// Leaderboards monobehaviour
    /// </summary>
    [SerializeField] Leaderboards leaderboards;

    [Header("Game Events")]
    [SerializeField] GameEvent gameStateInitialized;
    [SerializeField] GameEvent currencyChangedEvent;
    [SerializeField] GameEvent playerDiedEvent;
    [SerializeField] GameEvent playerDamagedEvent;

    int finalScore;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        enemyLayerMask = LayerMask.GetMask("Enemy");
        instance = this;
        gameState.Initialize();
        
        fadingPanel.FadeOut(gameStateInitialized.Raise);
        if(PlayerPrefs.GetInt("InfoDisplayed") < 1)
        {
            firstInfoPanel.SetActive(true);
            PlayerPrefs.SetInt("InfoDisplayed", 1);
        }

    }

    public void EndGame()
    {
        finalScore = gameState.Points;
        leaderboards.gameObject.SetActive(true);
        leaderboards.OpenLeaderboards(finalScore);
    }

    public void RestartGame()
    {
        fadingPanel.FadeIn(Reload);
    }

    public void Reload()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public bool CheckCurrencyAvailable(int amount)
    {
        return gameState.currencyAmount >= amount;
    }

    public bool SpendCurrency(int amount)
    {
        bool success = gameState.SpendCurrency(amount);
        if(success)
        {
            currencyChangedEvent.Raise();
        }
        return success;
    }

    public void OpenConstructionMenu(TowerSite site)
    {
        constructMenu.OpenConstructionMenu(site);
    }

    public void OpenUpgradeMenu(Tower tower)
    {
        upgradeMenu.OpenMenu(tower);
    }

    public void TowerHitEnemy(Tower tower, Enemy enemy)
    {
        if (enemy.DamageEnemy(interactionDesign.CalculateTowerDamage(tower, enemy)))
        {
            gameState.AddCurrency(enemy.Attributes.bounty + (enemy.Attributes.bountyPerLevel * (enemy.level - 1)));
            gameState.AddPoints(enemy.Attributes.pointsWorthBase + (enemy.Attributes.pointsWorthPerLevel * (enemy.level - 1)));
            currencyChangedEvent.Raise();
        }
    }

    public void EnemyReachedGoal()
    {
        playerDamagedEvent.Raise();
        if (!gameState.DamagePlayer())
        {
            playerDiedEvent.Raise();
        }
    }
}
