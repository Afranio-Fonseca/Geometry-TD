using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    static int shotHash = Animator.StringToHash("shot");

    public int level = 0;
    public TowerAttributes Attributes { get { return attributes; } }
    TowerAttributes attributes;
    [SerializeField] CircleCollider2D targetDetector;
    [SerializeField] Transform rangeIndicator;
    [SerializeField] ProjectilePooler projectilePool;
    [SerializeField] SpriteRenderer towerRenderer;
    [SerializeField] Animator animator;
    [SerializeField] TowerTopView topView;
    List<Transform> availableTargets;
    float shotCooldown;
    Element currentElement;
    public Element CurrentElement { get { return currentElement; } }
    public int EffectiveTowerDamage { get { return attributes.baseDamage + attributes.damagePerUpgrade * (level - 1); } }
    public int UpgradeCost { get { return attributes.baseUpgradeCost * level; } }


    public void Initialize(TowerAttributes _attributes)
    {
        attributes = _attributes;
        float effectiveRange = _attributes.range / 10;
        targetDetector.radius = effectiveRange;
        rangeIndicator.localScale = new Vector3(effectiveRange, effectiveRange, 1);
        projectilePool.SetProjectileToPool(attributes.projectile);
        availableTargets = new List<Transform>();
        topView.SetTopSprite(attributes.icon);
        ResetShotCooldown();
    }
    private void OnMouseUpAsButton()
    {
        GameManager.instance.OpenUpgradeMenu(this);
    }

    private void Update()
    {
        if(shotCooldown <= 0)
        {
            if (availableTargets.Count > 0)
            {
                // Shoots the available target that entered first
                StartShot();
            }
        } else
        {
            shotCooldown -= Time.deltaTime;
        }
    }

    public void SetAvailableTarget(Transform targetTransform)
    {
        availableTargets.Add(targetTransform);
    }

    public void RemoveAvailableTarget(Transform targetTransform)
    {
        availableTargets.Remove(targetTransform);
    }

    void StartShot()
    {
        animator.SetTrigger(shotHash);
    }

    // method to be called by animationClip at the end of shooting animation
    public void Shoot()
    {
        // if still has at least 1 target shoots a projectile at the target that exists for the longest time
        if(availableTargets.Count > 0)
        {
            Projectile p = projectilePool.GetPooledObject();
            p.gameObject.SetActive(true);
            p.transform.position = transform.position;
            p.Initialize(availableTargets[0], attributes.projectileSpeed, this);
            ResetShotCooldown();
        }
    }

    void ResetShotCooldown()
    {
        shotCooldown = 1f / attributes.firingRate;
    }

    public void ChangeEnchantment(Element element)
    {
        currentElement = element;
        towerRenderer.color = element.color;
    }

    public void Upgrade()
    {
        if(GameManager.instance.SpendCurrency(UpgradeCost))
        {
            level++;
        }
    }

    public void TargetEntered(Transform target)
    {
        availableTargets.Add(target);
    }

    public void TargetLeft(Transform target)
    {
        availableTargets.Remove(target);
    }
}
