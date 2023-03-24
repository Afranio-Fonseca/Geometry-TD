using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSite : MonoBehaviour
{
    public TowerAttributes[] availableTowers;
    [SerializeField] Transform rangeIndicator;
    [SerializeField] TowerTopView towerTop;


    private void OnMouseUpAsButton()
    {
        GameManager.instance.OpenConstructionMenu(this);
    }

    public void PreviewTower(TowerAttributes tower)
    {
        towerTop.SetTopSprite(tower.icon);
        towerTop.SetVisible(true);
        rangeIndicator.gameObject.SetActive(true);
        rangeIndicator.localScale = new Vector3(tower.range/10, tower.range/10, 1);
    }

    public void EndPreview()
    {
        towerTop.SetVisible(false);
        rangeIndicator.gameObject.SetActive(false);
    }
}
