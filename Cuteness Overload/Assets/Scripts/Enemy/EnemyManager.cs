using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health = 30;
    private float ARDamage = 15;
    private float SniperDamage = 55;
    private float ShotgunDamage = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SemiAutoBullet"))
        {
            health = health - ARDamage;
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
