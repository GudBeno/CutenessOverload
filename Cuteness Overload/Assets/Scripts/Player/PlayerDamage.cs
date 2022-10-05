using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float playerHealth = 8f;

    [SerializeField]
    private float maxPlayerHealth = 10f;

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthCollectable"))
        {
            CollectHealth();
            other.gameObject.SetActive(false);
        }
    }

    public void CollectHealth()
    {
        if(playerHealth > maxPlayerHealth)
        {
            float healthCollected = (float)(Math.Round(UnityEngine.Random.Range(1f, 8f)));
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

    public void TakeDamage()
    {

    }
}
