using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 325f;
    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private GameObject FPSCam;

    private Rigidbody rb;
    private Transform player;
    private Transform CamTrans;

    private KeyCode forward = KeyCode.W;
    private KeyCode backward = KeyCode.S;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode dodge = KeyCode.E;
    private KeyCode jump = KeyCode.Space;

    private float distGround;
    private bool CanJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Transform>();
        CamTrans = FPSCam.GetComponent<Transform>();
    }

    private void Update()
    {
        distGround = player.transform.position.y - ground.transform.position.y;
        NormalMove();
        Jump();
        Rotate();
    }

    public void NormalMove()
    {
        if (Input.GetKey(forward))
        {
            rb.AddForce(CamTrans.transform.forward * speed);
        }
        if (Input.GetKey(backward))
        {
            rb.AddForce(CamTrans.transform.forward * -speed);
        }
        if (Input.GetKey(left))
        {
            rb.AddForce(CamTrans.transform.right * -speed);
            //transform.Rotate(0, -Rotspeed, 0);
        }
        if (Input.GetKey(right))
        {
            rb.AddForce(CamTrans.transform.right * speed);
            //transform.Rotate(0, Rotspeed, 0);
        }
    }

    public void Jump()
    {
        if (distGround <= 1.25)
        {
            if (Input.GetKeyDown(jump))
            {
                rb.AddForce(transform.up * jumpHeight);
            }
        }
    }

    public void Rotate()
    {
        Vector3 EulerRotation = new Vector3(transform.eulerAngles.x, CamTrans.transform.eulerAngles.y, transform.eulerAngles.z);

        transform.rotation = Quaternion.Euler(EulerRotation);
    }

}
