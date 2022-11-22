using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainsawtrigger : MonoBehaviour
{
    public PlayerShoot shooter;
    public bool deleter;
    // Start is called before the first frame update
    void Start()
    {
        deleter = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bearnemy"))
        {
            shooter.cankill = true;
            if (deleter)
            {
                Destroy(other.gameObject);
                deleter = false;
            }
        }
        else
        {
            shooter.cankill = false;
        }
    }
}
