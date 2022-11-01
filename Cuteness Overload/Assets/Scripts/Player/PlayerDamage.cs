using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public float playerHealth = 10f; //This is the player's current health value

    [SerializeField]
    private float maxPlayerHealth = 10f; //This is the max amount of health the player can have

    [SerializeField]
    private Text healthText;

    private void Update()
    {
        healthText.text = playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider other) //This runs when entering a health collectable. Runs the Collect Health function
    {
        if (other.gameObject.CompareTag("HealthCollectable"))
        {
            CollectHealth();
            other.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
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
    }
}
