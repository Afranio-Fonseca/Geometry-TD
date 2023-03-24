using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructButton : MonoBehaviour
{
    public Image image;
    public Button button;
    [SerializeField] TextMeshProUGUI costText;
    [System.NonSerialized] public Sprite towerSprite;
    [System.NonSerialized] public Sprite confirmSprite;

    public void SetCostText(int value)
    {
        costText.transform.parent.gameObject.SetActive(true);
        costText.text = value.ToString();
    }

    public void SetButtonState(ConstructionButtonState state)
    {
        switch (state)
        {
            case ConstructionButtonState.disabled:
                button.onClick.RemoveAllListeners();
                image.color = new Color(1, 1, 1, 0);
                break;
            case ConstructionButtonState.purchaseUnavailable:
                image.sprite = towerSprite;
                button.interactable = false;
                button.onClick.RemoveAllListeners();
                break;
            case ConstructionButtonState.purchaseAvailable:
                image.sprite = towerSprite;
                button.interactable = true;
                break;
            case ConstructionButtonState.confirmingPurchase:
                image.sprite = confirmSprite;
                break;
        }
    }

    public enum ConstructionButtonState
    {
        disabled,
        purchaseAvailable,
        purchaseUnavailable,
        confirmingPurchase
    }
}
