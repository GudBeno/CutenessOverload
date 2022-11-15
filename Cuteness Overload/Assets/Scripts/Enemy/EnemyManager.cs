using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private float health;
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

    private Transform playerTransform;
    private GameObject player;

    private NavMeshAgent agent;
    private float distanceBee;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomDrop = Random.Range(1, 4);
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        if (this.gameObject.CompareTag("Enemy"))
        {
            health = 45f;
        }
        if (this.gameObject.CompareTag("BeeEnemy"))
        {
            health = 10f;
        }
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
        agent.destination = playerTransform.position;
        Bee();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        distanceBee = Vector3.Distance(this.transform.position, player.transform.position);
    }

    private void OnDestroy()
    {
        SpawnRandom();
    }

    public void SpawnRandom()
    {
        if(randomDrop == 1)
        {
            Instantiate(healthCollect, new Vector3(this.transform.position.x, 0.5f, this.transform.position.z), this.transform.rotation);
        }
        if(randomDrop == 2)
        {
            Instantiate(arAmmoCollect, new Vector3(this.transform.position.x, 0.5f, this.transform.position.z), this.transform.rotation);
        }
        if(randomDrop == 3)
        {
            Instantiate(snAmmoCollect, new Vector3(this.transform.position.x, 0.5f, this.transform.position.z), this.transform.rotation);
        }
        if(randomDrop == 4)
        {
            Instantiate(shAmmoCollect, new Vector3(this.transform.position.x, 0.5f, this.transform.position.z), this.transform.rotation);
        }
    }

    public void Bee()
    {
        if (this.gameObject.CompareTag("BeeEnemy"))
        {
            if(distanceBee <= 5f)
            {
                transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
            }
        }
    }
}
