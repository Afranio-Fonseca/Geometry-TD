using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    static int flyHash = Animator.StringToHash("Flying");
    static int hitHash = Animator.StringToHash("Hit");
    Transform target;
    float speed;
    Tower owner;
    public Tower Owner { get { return owner; } }
    Vector3 lastTargetPosition;

    [SerializeField] Animator animator;
    [SerializeField] Collider2D projectileCollider;

    private void OnEnable()
    {
        animator.Play(flyHash);
    }

    public void Initialize(Transform _target, float _speed, Tower _owner)
    {
        target = _target;
        speed = _speed;
        owner = _owner;
        lastTargetPosition = target.position;
        projectileCollider.enabled = true;
    }

    private void Update()
    {
        if (target.gameObject.activeInHierarchy)
        {
            lastTargetPosition = target.position;
        }
        if(projectileCollider.enabled) transform.position = Vector3.MoveTowards(transform.position, lastTargetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, lastTargetPosition) < 0.1f)
        {
            OnDestinationReached();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            OnDestinationReached(collision);
        }
    }

    void OnDestinationReached(Collider2D enemyHit = null)
    {
        projectileCollider.enabled = false;
        animator.SetTrigger(hitHash);
        if (owner.Attributes.areaEffect > 0)
        {
            RaycastHit2D[] damagedEnemies = Physics2D.CapsuleCastAll(transform.position, new Vector2(owner.Attributes.areaEffect, owner.Attributes.areaEffect), CapsuleDirection2D.Vertical, 0f, Vector2.zero, 0, GameManager.enemyLayerMask);
            foreach(RaycastHit2D damagedEnemy in damagedEnemies)
            {
                damagedEnemy.collider.SendMessage("HitByTower", owner);
            }
        }
        else if (enemyHit)
        {
            enemyHit.SendMessage("HitByTower", owner);
        }
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }



}
