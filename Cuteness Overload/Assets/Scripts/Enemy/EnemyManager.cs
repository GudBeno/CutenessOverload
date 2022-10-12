using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(other);
        }
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
