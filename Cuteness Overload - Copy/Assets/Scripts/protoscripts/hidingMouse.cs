using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidingMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Hides the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
