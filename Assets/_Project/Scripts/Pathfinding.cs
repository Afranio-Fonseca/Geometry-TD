using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

public class Pathfinding : MonoBehaviour
{
    public SpriteShapeController path;
    Spline spl;
    Vector3 previousPoint;
    Vector3 nextPoint;
    float effectiveDistance;
    Vector3 rightTangent;
    Vector3 leftTangent;
    int targetPointIndex = 1;
    float time = 0;
    public float speed;

    void Start()
    {
        spl = path.spline;
        SetPointValues();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speed / effectiveDistance;
        if (time > 1)
        {
            targetPointIndex++;
            time = 0;
            SetPointValues();
        }
        transform.position = BezierUtility.BezierPoint(previousPoint, rightTangent, leftTangent, nextPoint, time) + path.transform.position;
    }

    void SetPointValues()
    {
        previousPoint = spl.GetPosition(targetPointIndex - 1);
        nextPoint = spl.GetPosition(targetPointIndex);
        rightTangent = previousPoint + spl.GetRightTangent(targetPointIndex - 1);
        leftTangent = nextPoint + spl.GetLeftTangent(targetPointIndex);
        effectiveDistance = Vector3.Distance(previousPoint, nextPoint);
    }
}
