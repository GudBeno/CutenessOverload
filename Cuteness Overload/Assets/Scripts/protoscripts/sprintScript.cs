using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprintScript : MonoBehaviour
{
    [SerializeField] PlayerMovement playaMoveScript;
    [SerializeField] float speedBoost = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sprinting();
    }

    void Sprinting()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            print("Here is runnin");
            playaMoveScript.speed += speedBoost;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            print("Slowin down");
            playaMoveScript.speed -= speedBoost;
        }
    }
}
