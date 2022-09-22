using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private float bulletspeed = 100f;
    [SerializeField]
    private float lifeTime = 2f;

    private float lifeAlive;

    private void Start()
    {
        lifeAlive = lifeTime;
    }

    private void Update()
    {
        transform.position += transform.forward * bulletspeed * Time.deltaTime;

        lifeAlive -= Time.deltaTime;
        if (lifeAlive <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
