using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] Transform buttonContainer;
    [SerializeField] ConstructButton constructButtonPrefab;
    [SerializeField] Element[] availableElements;
    [SerializeField] ConstructButton upgradeButton;
    [SerializeField] GameState state;
    Dictionary<Element, ConstructButton> buttons;
    private Tower referenceTower;

    private void Awake()
    {
        buttons = new Dictionary<Element, ConstructButton>();
    }

    private void Update()
    {
        if (referenceTower && Input.GetMouseButtonDown(0))
        {
            if (Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) > 10.4f)
            {
                DisableButtons();
                referenceTower = null;
            }
        }
    }

    public void OpenMenu(Tower tower)
    {
        referenceTower = tower;
        SetUpgradeButtonState(GameManager.instance.CheckCurrencyAvailable(referenceTower.UpgradeCost));
        transform.position = referenceTower.transform.position;
        upgradeButton.gameObject.SetActive(true);
        foreach (Element element in availableElements)
        {
            if (buttons.ContainsKey(element))
            {
                buttons[element].gameObject.SetActive(true);
            }
            else
            {
                CreateEnchantmentnButton(element);
            }
        }
        ResetButtons();
    }

    void CreateEnchantmentnButton(Element element)
    {
        ConstructButton cb = Instantiate(constructButtonPrefab, buttonContainer);
        cb.image.color = element.color;
        buttons.Add(element, cb);
    }


    void ResetButtons()
    {
        if (referenceTower)
        {
            upgradeButton.SetCostText(referenceTower.UpgradeCost);
            SetUpgradeButtonState(GameManager.instance.CheckCurrencyAvailable(referenceTower.UpgradeCost));
            foreach (KeyValuePair<Element, ConstructButton> constructButton in buttons)
            {
                Element delegateElement = constructButton.Key;
                Tower delegateTower = referenceTower;
                constructButton.Value.button.onClick.AddListener(delegate { referenceTower.ChangeEnchantment(delegateElement); });
                constructButton.Value.gameObject.SetActive(true);
            }
        }
    }

    public void UpdatePurchaseAvailability()
    {
        if(referenceTower) SetUpgradeButtonState(GameManager.instance.CheckCurrencyAvailable(referenceTower.UpgradeCost));
    }

        void SetUpgradeButtonState(bool purchaseAvailable)
    {
        upgradeButton.button.interactable = purchaseAvailable;
        if(purchaseAvailable)
        {
            upgradeButton.button.onClick.RemoveAllListeners();
            upgradeButton.button.onClick.AddListener(referenceTower.Upgrade);
            upgradeButton.button.onClick.AddListener(ResetButtons);
        }
    }
    void DisableButtons()
    {
        upgradeButton.gameObject.SetActive(false);
        foreach (ConstructButton constructButton in buttons.Values)
        {
            constructButton.button.onClick.RemoveAllListeners();
            constructButton.gameObject.SetActive(false);
        }
    }

}
