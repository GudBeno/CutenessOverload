using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemet : MonoBehaviour
{
    public Transform player;
    float mousex, mousey;
    public float mousesense = 100f;
    float xrotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mousex = Input.GetAxis("Mouse X") * mousesense * Time.deltaTime;
        mousey = Input.GetAxis("Mouse Y") * mousesense * Time.deltaTime;

        xrotation -= mousey;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
        player.Rotate(Vector3.up * mousex);
    }
}
