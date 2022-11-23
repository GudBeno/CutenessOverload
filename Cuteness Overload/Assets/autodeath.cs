using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autodeath : MonoBehaviour
{
    public PlayerDamage damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            damage.playerHealth = 0;
        }
    }
}
