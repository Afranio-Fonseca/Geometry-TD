using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour
{
    int health;
    [SerializeField] EnemyAttributes attributes;
    [SerializeField] SpriteHealthbar healthbar;
    [SerializeField] SpriteRenderer colorSpriteRenderer;
    public EnemyAttributes Attributes { get { return attributes; } }

    Element element;
    public Element Element { get { return element; } }

    [System.NonSerialized] public int level;

    bool stopped = false;

    #region Path parameters

    public SpriteShapeController path;
    public Spline spline;
    Vector3 previousPoint;
    Vector3 nextPoint;
    float effectiveDistance;
    Vector3 rightTangent;
    Vector3 leftTangent;
    int targetPointIndex = 1;
    float time = 0;

    #endregion

    public void Initialize(int _currentLevel, Element _element, SpriteShapeController _path)
    {
        level = _currentLevel;
        health = attributes.startingHealth + (attributes.healthPerLevel * level);
        healthbar.SetMaxHealth(health);
        element = _element;
        colorSpriteRenderer.color = _element.color;
        path = _path;
        spline = path.spline;
        targetPointIndex = 1;
        time = 0;
        SetWaypointValues();
    }

    public void StopAction()
    {
        stopped = true;
    }

    void Update()
    {
        if(stopped)
        {
            return;
        }
        time += Time.deltaTime * attributes.speed / effectiveDistance;
        if (time > 1)
        {
            if(targetPointIndex == spline.GetPointCount() -1)
            {
                GameManager.instance.EnemyReachedGoal();
                ReturnToPool();

            } else
            {
                targetPointIndex++;
                time = 0;
                SetWaypointValues();
            }
        }
        transform.position = BezierUtility.BezierPoint(previousPoint, rightTangent, leftTangent, nextPoint, time) + path.transform.position;
    }

    void SetWaypointValues()
    {
        previousPoint = spline.GetPosition(targetPointIndex - 1);
        nextPoint = spline.GetPosition(targetPointIndex);
        rightTangent = previousPoint + spline.GetRightTangent(targetPointIndex - 1);
        leftTangent = nextPoint + spline.GetLeftTangent(targetPointIndex);
        effectiveDistance = Vector3.Distance(previousPoint, nextPoint);
    }

    public void HitByTower(Tower tower)
    {
        GameManager.instance.TowerHitEnemy(tower, this);
    }

    public bool DamageEnemy(int damage)
    {
        health -= damage;
        healthbar.UpdateHealthbar(health);
        if (health <= 0)
        {
            ReturnToPool();
            return true;
        } else
        {
            return false;
        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
