using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public float playerHealth = 8f; //This is the player's current health value
    public GameObject defaultscreen, deathscreen,ragdoll, ragcam, maincam, gunui;
    public PlayerShoot shooter;
    public PlayerMovement mover;
    public Rigidbody rb;
    

    [SerializeField]
    private float maxPlayerHealth = 8f; //This is the max amount of health the player can have

    [SerializeField]
    private Text healthText;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        deathscreen.SetActive(false);
        playerHealth = 8f;
        mover = GetComponent<PlayerMovement>();
        ragcam.SetActive(false);
        ragdoll.SetActive(false);
    }
    private void Update()
    {
        healthText.text = playerHealth.ToString();
        if(playerHealth <= 0)
        {
            death();
        }
    }

    private void OnTriggerEnter(Collider other) //This runs when entering a health collectable. Runs the Collect Health function
    {
        if (other.gameObject.CompareTag("HealthCollectable"))
        {
            CollectHealth();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("ARAmmoCollectable"))
        {
            shooter.CollectARAmmo();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("ShotgunAmmoCollectable"))
        {
            shooter.CollectSGAmmo();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("SniperAmmoCollectable"))
        {
            shooter.CollectSNAmmo();
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bearnemy"))
        {
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("BeeEnemy"))
        {
            playerHealth = playerHealth - 2;
            Destroy(collision.gameObject);
        }
    }

    public void CollectHealth() //Collect Health function. Randomises the amount of health and checks that you wont have more health than allowed.
    {
        if(playerHealth < maxPlayerHealth)
        {
            float healthCollected = (float)Math.Round(UnityEngine.Random.Range(1f, 8f), 0);
            float healthAllowed = maxPlayerHealth - playerHealth;
            if (healthCollected > healthAllowed)
            {
                playerHealth = playerHealth + healthAllowed;
            }
            else if (healthCollected < healthAllowed)
            {
                playerHealth = playerHealth + healthCollected;
            }
        }
    }

    public void TakeDamage() //Taking damage script, decreases the health.
    {
        playerHealth--;
        rb.AddForce(transform.up * 12000f);
    }
    public void death()
    {
        Time.timeScale = 0;
        rb.isKinematic = true;
        shooter.death = true;
        mover.death = true;
        defaultscreen.SetActive(false);
        gunui.SetActive(false);
        deathscreen.SetActive(true);

        ragdoll.SetActive(true);
        ragcam.SetActive(true);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.Confined;
        maincam.SetActive(false);


    }
}
