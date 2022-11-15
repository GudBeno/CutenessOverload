using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : MonoBehaviour
{
    [SerializeField]
    private float SGSpeed = 100f;
    [SerializeField]
    private float lifeTime = 4f;
    private Rigidbody rb;

    private float lifeAlive;

    private void Start()
    {
        lifeAlive = lifeTime;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update() //Destroys the bullet after a certain time limit. Also is destroyed if it hits an enemy (in other script)
    {
        //transform.position += transform.forward * SGSpeed * Time.deltaTime;

        rb.AddForce(transform.forward * SGSpeed * Time.deltaTime);

        lifeAlive -= Time.deltaTime;
        if (lifeAlive <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BeeEnemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
