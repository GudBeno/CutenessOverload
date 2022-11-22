using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private float health;
    private float ARDamage = 15f;
    private float SniperDamage = 55f;
    private float ShotgunDamage = 10f;
    private int randomDrop;
    public GameObject explosion;
    public GameObject gamecon;
    public gamecontroller gamecontrol;

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
    private float distance;

    private void Start()
    {
        gamecon = GameObject.FindGameObjectWithTag("gamecontroller");
        gamecontrol = gamecon.GetComponent<gamecontroller>();
        agent = GetComponent<NavMeshAgent>();
        randomDrop = Random.Range(1, 100);
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        if (this.gameObject.CompareTag("Bearnemy"))
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
        WakingUp();
        if(health <= 0)
        {
            gamecontrol.killcount++;
            if (this.gameObject.CompareTag("Bearnemy"))
            {
                Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            }
            SpawnRandom();
            Destroy(gameObject);
        }
        distanceBee = Vector3.Distance(this.transform.position, player.transform.position);
        distance = Vector3.Distance(this.transform.position, player.transform.position);
    }


    public void SpawnRandom()
    {
        if(randomDrop <= 50)
        {
            Instantiate(healthCollect, new Vector3(this.transform.position.x, this.transform.position.y , this.transform.position.z), this.transform.rotation);
        }
        if(randomDrop >50 && randomDrop <= 70)
        {
            Instantiate(arAmmoCollect, new Vector3(this.transform.position.x, this.transform.position.y , this.transform.position.z), this.transform.rotation);
        }
        if(randomDrop > 70 && randomDrop <= 85)
        {
            Instantiate(snAmmoCollect, new Vector3(this.transform.position.x, this.transform.position.y , this.transform.position.z), this.transform.rotation);
        }
        if(randomDrop > 85)
        {
            Instantiate(shAmmoCollect, new Vector3(this.transform.position.x, this.transform.position.y , this.transform.position.z), this.transform.rotation);
        }
    }

    public void Bee()
    {
        if (this.gameObject.CompareTag("BeeEnemy"))
        {
            if(distanceBee <= 6f)
            {
                transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
                GetComponent<NavMeshAgent>().baseOffset = 2f;
            }
        }
    }

    public void WakingUp()
    {
        if(distance >= 25)
        {
            GetComponent<NavMeshAgent>().speed = 0f;
        }
        else
        {
            GetComponent<NavMeshAgent>().speed = 5f;
        }
    }
    public void destroy()
    {
        Destroy(this.gameObject);
    }
    public void ardamager()
    {
        health = health - ARDamage;
    }
}
