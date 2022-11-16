using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawn1;
    [SerializeField]
    private Transform spawn2;
    [SerializeField]
    private Transform spawn3;
    [SerializeField]
    private Transform spawn4;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject bee;
    [SerializeField]
    private float waitTime = 3f;

    private int spawnNum = 1;
    private GameObject[] enemies;
    private GameObject[] bees;
    private GameObject[] spawns = new GameObject[4];
    private int numEnemies;
    private int numBees;
    private int numSpawns = 0, nummax;
    private float timer = 0f;
    public float counter = 0;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Bearnemy");
        bees = GameObject.FindGameObjectsWithTag("BeeEnemy");
        nummax = 20;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > 60)
        {
            nummax = 25;
            waitTime = 2.5f;
        } 
        if (counter > 120)
        {
            nummax = 35;
            waitTime = 2;
        }
        if (counter > 180)
        {
            nummax = 45;
            waitTime = 1.5f;
        }
        if (counter > 240)
        {
            nummax = 60;
            waitTime = 1f;
        }


        numSpawns = GameObject.FindGameObjectsWithTag("Bearnemy").Length;
        if (GameObject.FindGameObjectsWithTag("Bearnemy").Length == 0)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                SpawnEnemy();
                SpawnBee();
                if (numSpawns < nummax)
                {
                    numSpawns++;
                }
                else if (numSpawns >= nummax)
                {
                    numSpawns = nummax;
                }
                timer = 0;
            }
        }
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i <= numSpawns; i++)
        {
            spawnNum = Random.Range(0, 3);
            if (spawnNum == 0)
            {
                StartCoroutine(SpawnWait());
                Instantiate(enemy, spawn1.transform.position, spawn1.transform.rotation);
            }
            if(spawnNum == 1)
            {
                StartCoroutine(SpawnWait());
                Instantiate(enemy, spawn2.transform.position, spawn2.transform.rotation);
            }
            if (spawnNum == 2)
            {
                StartCoroutine(SpawnWait());
                Instantiate(enemy, spawn3.transform.position, spawn3.transform.rotation);
            }
            if (spawnNum == 3)
            {
                StartCoroutine(SpawnWait());
                Instantiate(enemy, spawn4.transform.position, spawn4.transform.rotation);
            }
        }
    }

    public void SpawnBee()
    {
        if (numSpawns >= 7)
        {
            for (int i = 0; i <= (numSpawns - 6); i++)
            {
                spawnNum = Random.Range(0, 3);
                if (spawnNum == 0)
                {
                    StartCoroutine(SpawnWait());
                    Instantiate(bee, spawn1.transform.position, spawn1.transform.rotation);
                }
                if (spawnNum == 1)
                {
                    StartCoroutine(SpawnWait());
                    Instantiate(bee, spawn2.transform.position, spawn2.transform.rotation);
                }
                if (spawnNum == 2)
                {
                    StartCoroutine(SpawnWait());
                    Instantiate(bee, spawn3.transform.position, spawn3.transform.rotation);
                }
                if (spawnNum == 3)
                {
                    StartCoroutine(SpawnWait());
                    Instantiate(bee, spawn4.transform.position, spawn4.transform.rotation);
                }
            }
        }
    }

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
