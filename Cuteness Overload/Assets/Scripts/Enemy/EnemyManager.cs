using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public float health = 45f;
    private float ARDamage = 10f;
    private float SniperDamage = 55f;
    private float ShotgunDamage = 45f;
    private int randomDrop;

    [SerializeField]
    private GameObject arAmmoCollect;
    [SerializeField]
    private GameObject snAmmoCollect;
    [SerializeField]
    private GameObject shAmmoCollect;
    [SerializeField]
    private GameObject healthCollect;

    [SerializeField]
    private Transform player;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomDrop = Random.Range(1, 4);
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
        if (other.gameObject.CompareTag("Chainsaw"))
        {
            health = 0;
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

    private void OnDestroy()
    {
        SpawnRandom();
    }

    public void SpawnRandom()
    {
        if(randomDrop == 1)
        {
            Instantiate(healthCollect, this.transform.position, this.transform.rotation);
        }
        if(randomDrop == 2)
        {
            Instantiate(arAmmoCollect, this.transform.position, this.transform.rotation);
        }
        if(randomDrop == 3)
        {
            Instantiate(snAmmoCollect, this.transform.position, this.transform.rotation);
        }
        if(randomDrop == 4)
        {
            Instantiate(shAmmoCollect, this.transform.position, this.transform.rotation);
        }
    }
}
