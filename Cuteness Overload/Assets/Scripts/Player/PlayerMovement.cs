using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 25f;
    [SerializeField]
    private float jumpHeight = 3000f;
    [SerializeField]
    private float dodgeSpeed = 50f;
    [SerializeField]
    private float sprintSpeed = 30f;
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
    private KeyCode sprint = KeyCode.LeftShift;

    private float distGround;
    private bool moveFor = false;
    private bool moveBack = false;
    private bool moveLeft = false;
    private bool moveRight = false;

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
        Dodge();
        Sprint();
    }

    public void NormalMove()
    {
        
        if (Input.GetKey(forward))
        {
            rb.AddForce(transform.forward * speed);//try rb.velocity
            moveFor = true;
        }
        if (Input.GetKeyUp(forward))
        {
            moveFor = false;
        }
        if (Input.GetKey(backward))
        {
            rb.AddForce(transform.forward * -speed);
            moveBack = true;
        }
        if (Input.GetKeyUp(backward))
        {
            moveBack = false;
        }
        if (Input.GetKey(left))
        {
            rb.AddForce(transform.right * -speed);
            moveLeft = true;
        }
        if (Input.GetKeyUp(left))
        {
            moveLeft = false;
        }
        if (Input.GetKey(right))
        {
            rb.AddForce(transform.right * speed);
            moveRight = true;
        }
        if (Input.GetKeyUp(right))
        {
            moveRight = false;
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

    public void Dodge()
    {
        if (Input.GetKeyDown(dodge))
        {
            if (moveFor)
            {
                rb.AddForce(transform.forward * dodgeSpeed);
            }
            if (moveBack)
            {
                rb.AddForce(transform.forward * -dodgeSpeed);
            }
            if (moveLeft)
            {
                rb.AddForce(transform.right * -dodgeSpeed);
            }
            if (moveRight)
            {
                rb.AddForce(transform.right * dodgeSpeed);
            }
        }
    }

    public void Sprint()
    {
        if (Input.GetKey(sprint))
        {
            if (moveFor)
            {
                rb.AddForce(transform.forward * sprintSpeed);
            }
            if (moveBack)
            {
                rb.AddForce(transform.forward * -sprintSpeed);
            }
            if (moveLeft)
            {
                rb.AddForce(transform.right * -sprintSpeed);
            }
            if (moveRight)
            {
                rb.AddForce(transform.right * sprintSpeed);
            }
        }
    }
}
