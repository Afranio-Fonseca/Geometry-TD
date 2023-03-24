using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteShapeRandomizer : MonoBehaviour
{
    [SerializeField] float maxX = 8;
    [SerializeField] float maxY = 5;
    [SerializeField] float mapOffset = 1;
    [SerializeField] SpriteShapeController spriteShapeController;
    [SerializeField] TowerSite towerSite;

    Quadrant currentQuadrant;
    Quadrant topRight = new Quadrant(1, 1);
    Quadrant bottomRight = new Quadrant(1, -1);
    Quadrant topLeft = new Quadrant(-1, 1);
    Quadrant bottomLeft = new Quadrant(-1, -1);
    List<Quadrant> quadrants;
    bool initialized = false;

    private void OnEnable()
    {
    }

    private void Awake()
    {
        quadrants = new List<Quadrant>();
        topRight.adjacentQuadrants = new[] { topLeft, bottomRight };
        bottomRight.adjacentQuadrants = new[] { topRight, bottomLeft };
        topLeft.adjacentQuadrants = new[] { topRight, bottomLeft };
        bottomLeft.adjacentQuadrants = new[] { topLeft, bottomRight };
        quadrants.Add(topRight);
        quadrants.Add(bottomRight);
        quadrants.Add(topLeft);
        quadrants.Add(bottomLeft);
        Randomize();
        initialized = true;

    }

    void Randomize()
    {
        bool startX = Random.value < 0.5f;
        if (startX)
        {
            int xFactor = Random.value < 0.5f ? 1 : -1;
            float startingY = (Random.value - 0.5f) * (maxY * 2);
            Vector3 startPosition = new Vector3(xFactor * (maxX + mapOffset), startingY, 0);
            spriteShapeController.spline.SetPosition(0, startPosition);
            spriteShapeController.spline.SetRightTangent(0, new Vector3(xFactor, 0, 0));
            MarkQuadrantTaken(startPosition);
        }
        else
        {
            int yFactor = Random.value < 0.5f ? 1 : -1;
            float startingX = (Random.value - 0.5f) * (maxY * 2);
            Vector3 startPosition = new Vector3(startingX, yFactor * (maxY + mapOffset), 0);
            spriteShapeController.spline.SetPosition(0, startPosition);
            spriteShapeController.spline.SetRightTangent(0, new Vector3(0, yFactor, 0));
            MarkQuadrantTaken(startPosition);
        }
        for(int c = 1; c < 4; c++)
        {
            Vector3 nextPosition = NextPointPosition();
            spriteShapeController.spline.SetPosition(c, nextPosition);
            spriteShapeController.spline.SetRightTangent(c, new Vector3(currentQuadrant.xFactor, currentQuadrant.yFactor, 0));
            spriteShapeController.spline.SetLeftTangent(c, new Vector3(-currentQuadrant.xFactor, -currentQuadrant.yFactor, 0));
            Instantiate(towerSite, nextPosition + new Vector3(-currentQuadrant.xFactor * 2, -currentQuadrant.yFactor * 2, 0), Quaternion.identity);
        }
        bool endX = Random.value < 0.5f;
        if (endX)
        {
            float endingY = Random.value * maxY * currentQuadrant.yFactor;
            Vector3 endPosition = new Vector3(currentQuadrant.xFactor * (maxX + mapOffset), endingY, 0);
            spriteShapeController.spline.SetPosition(4, endPosition);
            spriteShapeController.spline.SetRightTangent(4, new Vector3(currentQuadrant.xFactor, 0, 0));
        }
        else
        {
            float endingX = Random.value * maxY * currentQuadrant.yFactor;
            Vector3 endPosition = new Vector3(endingX, currentQuadrant.yFactor * (maxY + mapOffset), 0);
            spriteShapeController.spline.SetPosition(4, endPosition);
            spriteShapeController.spline.SetRightTangent(4, new Vector3(0, currentQuadrant.yFactor, 0));
        }
    }

    void MarkQuadrantTaken(Vector3 position)
    {
        float xPos = position.x;
        float yPos = position.y;
        if (yPos > 0 && xPos > 0)
        {
            currentQuadrant = topRight;
            quadrants.Remove(topRight);
        }
        else if (yPos <= 0 && xPos > 0)
        {
            currentQuadrant = bottomRight;
            quadrants.Remove(bottomRight);
        }

        else if (yPos > 0 && xPos <= 0)
        {
            currentQuadrant = topLeft;
            quadrants.Remove(topLeft);
        }

        else if (yPos <= 0 && xPos <= 0)
        {
            currentQuadrant = bottomLeft;
            quadrants.Remove(bottomLeft);
        }

    }

    Vector3 NextPointPosition()
    {
        Quadrant nextQuadrant;
        if (quadrants.Count > 2)
        {
            nextQuadrant = currentQuadrant.adjacentQuadrants[Random.Range(0, 2)];
        } else
        {
            nextQuadrant = quadrants[Random.Range(0, quadrants.Count)];
        }
        Vector3 nextPosition = new Vector3(Random.Range(0, maxX) * nextQuadrant.xFactor, Random.Range(0, maxY) * nextQuadrant.yFactor, 0);

        MarkQuadrantTaken(nextPosition);

        return nextPosition;
    }

    struct Quadrant
    {
        public Quadrant[] adjacentQuadrants;
        public int yFactor;
        public int xFactor;

        public Quadrant(int _xFactor, int _yFactor)
        {
            adjacentQuadrants = new Quadrant[2];
            xFactor = _xFactor;
            yFactor = _yFactor;
        }

    }
}
