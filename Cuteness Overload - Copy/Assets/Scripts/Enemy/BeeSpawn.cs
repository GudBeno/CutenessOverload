using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawn : MonoBehaviour
{
    [SerializeField]
    private Transform spawn1;
    [SerializeField]
    private Transform spawn2;
    [SerializeField]
    private Transform spawn3;
    [SerializeField]
    private GameObject bee;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(bee, spawn1.transform.position, spawn1.transform.rotation);
        Instantiate(bee, spawn2.transform.position, spawn2.transform.rotation);
        Instantiate(bee, spawn3.transform.position, spawn3.transform.rotation);
        Destroy(this.gameObject);
    }
}
