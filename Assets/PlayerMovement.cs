using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private float moveHorizontal;
    private float moveVertical;
    [SerializeField] private bool isGrounded;
    // Empty game object placed at player feet
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

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
    }

    // Make the player move horizontally
    private void Run()
    {
        Vector2 newForce = new Vector2(moveHorizontal * speed, 0f);
        rb.AddForce(newForce, ForceMode2D.Impulse);
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
            Vector2 newForce = new Vector2(0f, jumpForce);
            rb.AddForce(newForce, ForceMode2D.Impulse); 
        }
    }

}
