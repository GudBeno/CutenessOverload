using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
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
