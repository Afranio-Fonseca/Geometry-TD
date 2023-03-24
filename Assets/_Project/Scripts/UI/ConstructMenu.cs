using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructMenu : MonoBehaviour
{
    [SerializeField] Transform buttonContainer;
    [SerializeField] ConstructButton constructButtonPrefab;
    [SerializeField] Sprite confirmationSprite;
    [SerializeField] Tower towerPrefab;
    Dictionary<TowerAttributes, ConstructButton> buttons;
    TowerSite currentSite;
    public GameEventListener currencyChangedEventListener { set { currencyChangedEventListener = value; } }



    private void Awake()
    {
        buttons = new Dictionary<TowerAttributes, ConstructButton>();
    }

    private void Update()
    {
        if(currentSite && Input.GetMouseButtonDown(0))
        {
            if (Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) > 10.4f)
            {
                DisableButtons();
                currentSite = null;
            }
        }            
    }

    public void OpenConstructionMenu(TowerSite site)
    {
        DisableButtons();
        currentSite = site;
        transform.position = currentSite.transform.position;
        foreach(TowerAttributes tower in site.availableTowers)
        {
            if(buttons.ContainsKey(tower))
            {
                buttons[tower].gameObject.SetActive(true);
            } else
            {
                CreateConstructionButton(tower);
            }
        }
        ResetButtons();
    }

    void CreateConstructionButton(TowerAttributes tower)
    {
        ConstructButton cb = Instantiate(constructButtonPrefab, buttonContainer);
        cb.confirmSprite = confirmationSprite;
        cb.towerSprite = tower.icon;
        cb.SetCostText(tower.baseCost);
        buttons.Add(tower, cb);
    }

    void SetPurchaseAvailable(ConstructButton constructButton, TowerAttributes tower)
    {
        constructButton.SetButtonState(ConstructButton.ConstructionButtonState.purchaseAvailable);
        constructButton.button.onClick.RemoveAllListeners();
        ConstructButton delegateButton = constructButton;
        TowerAttributes delegateTower = tower;
        constructButton.button.onClick.AddListener(delegate { ConstructionButtonPressed(delegateButton, delegateTower); });
    }

    void SetPurchaseUnavailable(ConstructButton constructButton)
    {
        constructButton.SetButtonState(ConstructButton.ConstructionButtonState.purchaseUnavailable);
    }

    void ConstructionButtonPressed(ConstructButton constructButton, TowerAttributes tower)
    {
        ResetButtons();
        constructButton.SetButtonState(ConstructButton.ConstructionButtonState.confirmingPurchase);
        currentSite.PreviewTower(tower);
        constructButton.button.onClick.RemoveAllListeners();
        ConstructButton delegateButton = constructButton;
        TowerAttributes delegateTower = tower;
        constructButton.button.onClick.AddListener(delegate { ConstructionConfirmed(delegateTower); });
    }

    void ConstructionConfirmed(TowerAttributes towerAttributes)
    {
        if(GameManager.instance.SpendCurrency(towerAttributes.baseCost))
        {
            Tower tower = Instantiate(towerPrefab, currentSite.transform.position, currentSite.transform.rotation);
            tower.Initialize(towerAttributes);
            DisableButtons();
            Destroy(currentSite.gameObject);
        }
    }

    void ResetButtons()
    {
        currentSite.EndPreview();
        UpdatePurchaseAvailability();
    }

    public void UpdatePurchaseAvailability()
    {
        foreach (KeyValuePair<TowerAttributes, ConstructButton> button in buttons)
        {
            if (button.Value.gameObject.activeInHierarchy)
            {
                if (GameManager.instance.CheckCurrencyAvailable(button.Key.baseCost))
                {
                    SetPurchaseAvailable(button.Value, button.Key);
                }
                else
                {
                    SetPurchaseUnavailable(button.Value);
                }
            }
        }
    }

    void DisableButtons()
    {
        if(currentSite) currentSite.EndPreview();
        foreach (ConstructButton constructButton in buttons.Values)
        {
            constructButton.button.onClick.RemoveAllListeners();
            constructButton.gameObject.SetActive(false);
        }
    }
}
