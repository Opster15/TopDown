// Some stupid rigidbody based movement by Dani

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public HatObject hatObject;
    PlayerStats playerStats;
    public Transform orientation;
    public Transform player;
    public GameObject crosshair;

    InputManager inputManager;

    //Other
    private Rigidbody rb;
    private float nextDashTime;

    Vector3 resetPosition;

    //Movement
    private bool grounded;
    public LayerMask whatIsGround;

    private float threshold = 0.01f;
    private float maxSlopeAngle = 35f;
    public float counterMovement;

    bool isMoving;


    //Input
    float x, y;

    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
        resetPosition = player.position;
        inputManager = FindObjectOfType<InputManager>();
    }

    void Start()
    {
        hatObject = playerStats.hatObject;        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Reset()
    {
        player.position = resetPosition;
        hatObject = playerStats.hatObject;
    }

    public void BO3Reset()
    {
        player.position = resetPosition;
    }


    private void FixedUpdate()
    {        
        Movement();

    }

    private void Update()
    {
        MyInput();
        LookAtMouse();
        //Dash();
    }

    private void MyInput()
    {
        x = inputManager.m_movementInput.x * hatObject.movementMultiplier;
        y = inputManager.m_movementInput.y * hatObject.movementMultiplier;

        if(x == 0 && y == 0)
        {
            isMoving = false;
        }
        else if(x != 0 && y != 0)
        {
            isMoving = true;
        }
    }

    /*
    public void Dash()
    {
        Vector3 moveDir = new Vector3(-y, 0, x).normalized;

        if (Time.time > nextDashTime)
        {
            if (Input.GetKey("space"))
            {
                rb.AddForce(moveDir * hatObject.dashForce);
                nextDashTime = Time.time + hatObject.dashCooldown;
            }
        }

    }
    */

    void LookAtMouse()
    {
        Plane playerplane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(inputManager.m_mousePositionInput);
        float hitdist = 0.0f;

        if (playerplane.Raycast(ray, out hitdist))
        {
            Vector3 targetpoint = ray.GetPoint(hitdist);
            Quaternion targetrotation = Quaternion.LookRotation(targetpoint - transform.position);
            player.transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, 100000 * Time.deltaTime);
            crosshair.transform.position = targetpoint;
        }
        
    }


    private void Movement()
    {
        Vector3 moveDir = new Vector3(-y,0, x).normalized;
        //Extra gravity
        //rb.AddForce(Vector3.down * Time.deltaTime * 10);

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);


        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (x > 0 && xMag > hatObject.maxSpeed) x = 0;
        if (x < 0 && xMag < -hatObject.maxSpeed) x = 0;
        if (y > 0 && yMag > hatObject.maxSpeed) y = 0;
        if (y < 0 && yMag < -hatObject.maxSpeed) y = 0;

        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 0.02f;
            multiplierV = 0.02f;
        }


        //Apply forces to move player
        rb.AddForce(orientation.transform.forward * y * hatObject.moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * hatObject.moveSpeed * Time.deltaTime * multiplier);
    }




    private float desiredX;


    private void CounterMovement(float x, float y, Vector2 mag)
    {

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(hatObject.moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(hatObject.moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > hatObject.maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * hatObject.maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    /// <summary>
    /// Find the velocity relative to where the player is looking
    /// Useful for vectors calculations regarding movement and limiting movement
    /// </summary>
    /// <returns></returns>
    
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;

    /// <summary>
    /// Handle ground detection
    /// </summary>
    private void OnCollisionStay(Collision other)
    {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }

}