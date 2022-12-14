using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 25f;
    [SerializeField]
    private float jumpHeight = 3000f;
    [SerializeField]
    private float dodgeSpeed = 7500f;
    [SerializeField]
    private float sprintSpeed = 27.5f;
    [SerializeField]
    private float jumpGravity = 50f;
    [SerializeField]
    private float staminaMax = 6000f;
    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private GameObject FPSCam;
    [SerializeField]
    private Text staminaText;

    public float stamina = 6000f;

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
    private bool canJump = false;
    private bool isSprinting = false;

    public bool death = false;

    public GameObject controlstab;
    bool controls;

    private void Start() //Gets the Components needed in the later functions
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Transform>();
        CamTrans = FPSCam.GetComponent<Transform>();
        controlstab.SetActive(false);
        controls = false;
    }

    private void Update() //Checks the distance constantly for the jumping. Runs the functions.
    {
        rb.angularVelocity = Vector3.zero;

        if (death == false)
        {
            distGround = player.transform.position.y - ground.transform.position.y;
            NormalMove();
            Jump();
            //        Rotate();
            Dodge();
            Sprint();
            StaminaRefill();
            // staminaText.text = stamina.ToString();
        }
    }

    public void NormalMove() //Movement function
    {
        
        if (Input.GetKey(forward))
        {
            rb.AddForce(transform.forward * speed); //try rb.velocity
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
    

    public void Jump() //Jump function, based on distance to the ground
    {
        if (canJump)
        {
            if (Input.GetKeyDown(jump))
            {
                rb.AddForce(transform.up * jumpHeight);
                rb.AddForce(transform.up * jumpGravity * Time.deltaTime);
                if (rb.velocity.y < 0)
                {
                    Physics.gravity = new Vector3(0, -15, 0);
                }
            }
        }
    }

 //   public void Rotate() //Rotates the player with the camera around the y axis
//    {
        // Vector3 EulerRotation = new Vector3(transform.eulerAngles.x, CamTrans.transform.eulerAngles.y, transform.eulerAngles.z);

        // transform.rotation = Quaternion.Euler(EulerRotation);


 //   }

    public void Dodge() //Dodge function, done by increasing speed
                        //Also added stamina deducted when dodging
    {
        if (stamina >= 1200)
        {
            if (Input.GetKeyDown(dodge))
            {
                if (moveFor)
                {
                    rb.AddForce(transform.forward * dodgeSpeed);
                    stamina = stamina - 1200;
                }
                if (moveBack)
                {
                    rb.AddForce(transform.forward * -dodgeSpeed);
                    stamina = stamina - 1200;
                }
                if (moveLeft)
                {
                    rb.AddForce(transform.right * -dodgeSpeed);
                    stamina = stamina - 1200;
                }
                if (moveRight)
                {
                    rb.AddForce(transform.right * dodgeSpeed);
                    stamina = stamina - 1200;
                }
            }
        }
    }

    public void Sprint() //Sprint function, done by increasing speed, but not as much as a dodge
                         //Also has stamina added to decrease whenever sprinting
    {
        if (stamina > 0)
        {
            if (Input.GetKey(sprint))
            {
                if (moveFor)
                {
                    rb.AddForce(transform.forward * sprintSpeed);
                    isSprinting = true;
                }
                if (moveBack)
                {
                    rb.AddForce(transform.forward * -sprintSpeed);
                    isSprinting = true;
                }
                if (moveLeft)
                {
                    rb.AddForce(transform.right * -sprintSpeed);
                    isSprinting = true;
                }
                if (moveRight)
                {
                    rb.AddForce(transform.right * sprintSpeed);
                    isSprinting = true;
                }
            }
            if (Input.GetKeyUp(sprint))
            {
                isSprinting = false;
            }
        }
    }

    private void StaminaRefill() //Controls stamina and it refilling
    {
        if (isSprinting == false)
        {
            if (stamina < staminaMax)
            {
                stamina = stamina + 2;
            }
        }
        if (stamina > staminaMax)
        {
            stamina = staminaMax;
        }
        if (stamina <= 0)
        {
            isSprinting = false;
        }
        if (isSprinting)
        {
            stamina--;
        }
    }

    private void OnCollisionStay(Collision collision) //Makes it so you can jump on anything tagged Ground or Obstacle
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
