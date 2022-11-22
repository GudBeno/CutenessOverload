using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickups : MonoBehaviour
{
    [SerializeField]
    private GameObject healthCollectable;
    [SerializeField]
    private Transform collect1;
    [SerializeField]
    private Transform collect2;
    [SerializeField]
    private Transform collect3;
    [SerializeField]
    private Transform collect4;

    private float timeElapsed;
    private float timeDesired = 2;
    private int random;
    private GameObject[] pickups;

    private void Update()
    {
        pickups = GameObject.FindGameObjectsWithTag("HealthCollectable");
        if (pickups.Length == 0)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timeDesired)
            {
                Instantiate(healthCollectable, collect1.transform.position, collect1.transform.rotation);
                Instantiate(healthCollectable, collect2.transform.position, collect2.transform.rotation);
                Instantiate(healthCollectable, collect3.transform.position, collect3.transform.rotation);
                Instantiate(healthCollectable, collect4.transform.position, collect4.transform.rotation);
            }
        }
    }
}
