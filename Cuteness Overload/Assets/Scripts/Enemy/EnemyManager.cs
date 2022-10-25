using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public float health = 45;
    private float ARDamage = 5f;
    private float SniperDamage = 55;
    private float ShotgunDamage = 45;
    [SerializeField]
    private Transform player;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SemiAutoBullet"))
        {
            health = health - ARDamage;
            Destroy(other);
        }
        if (other.gameObject.CompareTag("SniperBullet"))
        {
            health = health - SniperDamage;
            Destroy(other);
        }
        if (other.gameObject.CompareTag("ShotgunBullet"))
        {
            health = health - ShotgunDamage;
            Destroy(other);
        }
    }

    private void Update()
    {
        agent.destination = player.position;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
