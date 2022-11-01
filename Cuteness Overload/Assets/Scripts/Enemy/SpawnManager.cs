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
    private GameObject player;
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
            spawnNum = Random.Range(1, 5);
            StartCoroutine(SpawnWait());
            Instantiate(enemy, spawns[spawnNum].transform.position, spawns[spawnNum].transform.rotation);
        }
    }

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
