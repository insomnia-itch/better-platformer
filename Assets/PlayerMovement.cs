using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField] private float speed = 9f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float linearDrag = 4f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravityScale = 5f;
    [SerializeField] private float fallGravityScale = 15f;
    private float moveHorizontal;
    private float moveVertical;
    private float jumpButtonPressedAt;
    private float jumpButtonPressedWindow = 0.5f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping = false;
    // Empty game object placed at player feet
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Determine Radius size by adding collider to groundCheck and editing size
        isGrounded = Physics2D.OverlapCircle(groundCheck.position , 0.05f, groundLayer);
    }

    private void FixedUpdate()
    {
        Run();

        // Fall();
    }

    // Make the player move horizontally
    private void Run()
    {
        Vector2 newForce = new Vector2(moveHorizontal * speed, 0f);
        rb.AddForce(newForce, ForceMode2D.Impulse);

        if ((moveHorizontal > 0 && !facingRight) || (moveHorizontal < 0 && facingRight))
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        bool changingDirections = (moveHorizontal > 0 && rb.velocity.x < 0) ||  (moveHorizontal < 0 && rb.velocity.x > 0);

        if (Mathf.Abs(moveHorizontal) < 0.4f || changingDirections)
        {
            rb.drag = linearDrag;
        } else{
            rb.drag = 0f;
        }
    }

    // Capture the horizontal input
    public void Move(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // If this was *just* pressed
        //   prevents action from calling back when button is released
        if (context.performed && isGrounded)
        {
            rb.gravityScale = gravityScale;
            jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            Vector2 newForce = Vector2.up * jumpForce;
            rb.AddForce(newForce, ForceMode2D.Impulse); 
            isJumping = true;
            jumpButtonPressedAt = 0;
        }


        if (isJumping)
        {
            jumpButtonPressedAt += Time.deltaTime;
            if (jumpButtonPressedAt < jumpButtonPressedWindow)
            {
                rb.gravityScale = fallGravityScale;
            }
            if (rb.velocity.y < 0)
            {
                isJumping = false;
                rb.gravityScale = gravityScale;
            }
        }
    }

    private void Fall()
    {
        if (rb.velocity.y > 0.1f)
        {
            rb.gravityScale = gravityScale;
        } else
        {
            rb.gravityScale = fallGravityScale;
        }
    }

    void Flip(){
        facingRight = !facingRight;
        // transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

}
