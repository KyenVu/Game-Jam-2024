using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    public float speed;

    private Animator animator;
    private Vector2 moveInput;
    private Vector2 lastMoveInput;
    private bool facingLeft = true; // Track the current facing direction

    private Vector3 originalScale; // Store the original scale

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Initialize the original scale
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        m_rigidbody.velocity = moveInput * speed;
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Set moveInput and lastMoveInput
        moveInput = new Vector2(moveX, moveY).normalized;
        if (moveX != 0 || moveY != 0)
        {
            lastMoveInput = moveInput;
        }

        // Handle character flipping for left and right movement
        if (moveX > 0 && facingLeft)
        {
            Flip(false); // Facing right
        }
        else if (moveX < 0 && !facingLeft)
        {
            Flip(true); // Facing left
        }

        // Update animator parameters
        SetDirection(moveInput, lastMoveInput);
    }

    public void SetDirection(Vector2 direction, Vector2 lastDirection)
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetFloat("LastMoveX", lastDirection.x);
        animator.SetFloat("LastMoveY", lastDirection.y);
        animator.SetFloat("MoveMagnitude", direction.magnitude);
    }

    private void Flip(bool faceLeft)
    {
        facingLeft = faceLeft;
        Vector3 scale = originalScale;
        scale.x = faceLeft ? Mathf.Abs(originalScale.x) : -Mathf.Abs(originalScale.x); // Set the scale based on the direction
        transform.localScale = scale;
    }
}
