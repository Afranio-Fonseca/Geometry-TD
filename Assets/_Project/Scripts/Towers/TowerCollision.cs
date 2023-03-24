using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCollision : MonoBehaviour
{
    [SerializeField] Tower tower;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            tower.TargetEntered(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            tower.TargetLeft(collision.transform);
        }
    }

}
