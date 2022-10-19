using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private float bulletspeed = 100f;
    [SerializeField]
    private float lifeTime = 4f;

    private float lifeAlive;

    private void Start()
    {
        lifeAlive = lifeTime;
    }

    private void Update() //Destroys the bullet after a certain time limit. Also is destroyed if it hits an enemy (in other script)
    {
        transform.position += transform.forward * bulletspeed * Time.deltaTime;

        lifeAlive -= Time.deltaTime;
        if (lifeAlive <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
