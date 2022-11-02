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
    private float waitTime = 3f;

    private int spawnNum = 1;
    private GameObject[] enemies;
    private GameObject[] spawns = new GameObject[4];
    private int numEnemies;
    private int numSpawns = 0;
    private float timer = 0f;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                SpawnEnemy();
                if (numSpawns <= 15)
                {
                    numSpawns++;
                }
                else if (numSpawns >= 16)
                {
                    numSpawns = 15;
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

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(0.5f);
    }
}