using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isFacingRight = true;
    private int facingDirection = 1;

    public float jumpForce = 12;

    public float movementSpeed = 6;
    private float movementInputDirection;
    public bool isWalking;
    public bool isDead;
    public bool isFalling;

    public bool isGrounded;
    public Transform GCPosition;
    public float GCRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    public float hangTime = 0.2f;
    private float hangCounter;

    public Rigidbody2D rb;
    private Animator anim;
    public GameObject RunParticle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
        CheckMovement();
        CheckMovementDirection();
        CheckGrounded();

        #region Jump
        if (Input.GetButtonDown("Jump") && hangCounter > 0)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            HighJump();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        #endregion
    }

    public void CheckMovement()
    {
        if (isDead == false)
        {
            movementInputDirection = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            isWalking = true;

            if (rb.velocity.x == 0)
            {
                isWalking = false;
            }

            if (isGrounded)
            {
                hangCounter = hangTime;
            }
            else
            {
                hangCounter -= Time.deltaTime;
            }
        }

    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDead", isDead);
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (isGrounded == true && rb.velocity.x != 0)
        {
            RunParticle.SetActive(true);
        }
        else
        {
            RunParticle.SetActive(false);
        }

    }

    private void Flip()
    {
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

    }

    #region Jump 

    public void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GCPosition.position, GCRadius, whatIsGround);
    }

    public void Jump()
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void HighJump()
    {

        if (jumpTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }
    }

    #endregion

    public void StopMovement()
    {
        isDead = true;
        rb.velocity = new Vector2(0, 0);
    }

}
