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
    bool canspawn = true;
    bool first = true, second = true, third = true, fourth = true, fifth = true, sixth = true, seventh = true, eigth = true, nineth = true, tenth = true;
    int whichspawn;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Bearnemy");
        bees = GameObject.FindGameObjectsWithTag("BeeEnemy");
        nummax = 20;
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter > 10 && counter < 11)
        {
            if (first)
            {
                firstspawner();
                secondspawner();
                first = false;
            }
        }
        if (counter > 30 && counter < 31)
        {
            if (second)
            {
                firstspawner();
                thirdspawner();
                second = false;
            }
        }
        if (counter > 50 && counter < 51)
        {
            if (third)
            {
                fourthspawner();
                thirdspawner();
                third = false;
            }

        }
        if (counter > 70 && counter < 71)
        {
            if (fourth)
            {
                firstspawner();
                secondspawner();
                thirdspawner();
                fourthspawner();
                fourth = false;
            }

        }
        if (counter > 80 && counter < 81)
        {
            if (fifth)
            {
                firstspawner();
                secondspawner();
                thirdspawner();
                fourthspawner();
                fifth = false;
            }

        }
        if (counter > 90 && counter < 91)
        {
            if (sixth)
            {
                firstspawner();
                secondspawner();
                thirdspawner();
                fourthspawner();
                sixth = false;
            }

        }
        if (counter > 95 && counter < 96)
        {
            if (seventh)
            {
                firstspawner();
                fourthspawner();
                seventh = false;
            }

        }
        if (counter > 100 && counter < 101)
        {
            if (eigth)
            {
                firstspawner();
                secondspawner();
                thirdspawner();
                fourthspawner();
                eigth = false;
            }

        }
        if (counter > 105 && counter < 106)
        {
            if (nineth)
            {
                secondspawner();
                thirdspawner();
                nineth = false;
            }

        }
        if (counter > 110 && counter < 111)
        {
            if (tenth)
            {
                firstspawner();
                secondspawner();
                thirdspawner();
                fourthspawner();
                tenth = false;
            }

        }
        if (counter > 120)
        {
            if (canspawn == true)
            {
                StartCoroutine(spawnconstant());
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
    void firstspawner()
    {
        int firstspawn = Random.Range(1, 10);
        if (firstspawn < 6)
        {
            Instantiate(enemy, spawn1.transform.position, spawn1.transform.rotation);
        }
        if (firstspawn >= 6)
        {
            Instantiate(bee, spawn1.transform.position, spawn1.transform.rotation);
        }
    }
    void secondspawner()
    {
        int secoundspawn = Random.Range(1, 10);
        if (secoundspawn < 6)
        {
            Instantiate(enemy, spawn2.transform.position, spawn2.transform.rotation);
        }
        if (secoundspawn >= 6)
        {
            Instantiate(bee, spawn2.transform.position, spawn2.transform.rotation);
        }
    }
    void thirdspawner()
    {
        int thirdspawn = Random.Range(1, 10);
        if (thirdspawn < 6)
        {
            Instantiate(enemy, spawn3.transform.position, spawn3.transform.rotation);
        }
        if (thirdspawn >= 6)
        {
            Instantiate(bee, spawn3.transform.position, spawn3.transform.rotation);
        }
    }
    void fourthspawner()
    {
        int fourthspawn = Random.Range(1, 10);
        if (fourthspawn < 8)
        {
            Instantiate(enemy, spawn4.transform.position, spawn4.transform.rotation);
        }
        if (fourthspawn >= 8)
        {
            Instantiate(bee, spawn4.transform.position, spawn4.transform.rotation);
        }
    }
    IEnumerator spawnconstant()
    {
        canspawn = false;
        switch (whichspawn)
        {
            case 1: firstspawner();
                whichspawn++;
                break;
            case 2:
                secondspawner();
                whichspawn++;
                break;
            case 3:
                thirdspawner();
                whichspawn++;
                break;
            case 4:
                fourthspawner();
                whichspawn = 1;
                break;
        }
        yield return new WaitForSeconds(0.5f);
        canspawn = true;
    }
}
