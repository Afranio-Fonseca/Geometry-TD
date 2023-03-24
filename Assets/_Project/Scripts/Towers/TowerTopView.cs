using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTopView : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer outlineRenderer;

    public void SetTopSprite(Sprite _sprite)
    {
        spriteRenderer.sprite = _sprite;
        outlineRenderer.sprite = _sprite;
    }

    public void SetVisible(bool visible)
    {
        float alpha = visible ? 1 : 0;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        outlineRenderer.color = new Color(outlineRenderer.color.r, outlineRenderer.color.g, outlineRenderer.color.b, alpha);
    }
}
