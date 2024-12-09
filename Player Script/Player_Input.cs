using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public Rigidbody2D myRigidBody; // Assign a rigid body to the player
    [SerializeField] float movement; // To move the player
    [SerializeField] const int playerSpeed = 4; // Player speed
    [SerializeField] bool isFacingRight = true; // A bool to check which direction the player is facing
    [SerializeField] bool isGrounded = false; // A bool to check whether the player is on the ground or not, it is false because it is only true if the player is on the ground
    public Animator animator; // The animator controller

    /// <summary>
    /// To handle Dash
    /// </summary>
    [SerializeField] float dashSpeed = 12f; // Dash speed
    [SerializeField] float dashDuration = 1f; // Dash duration
    [SerializeField] float dashCoolDown = 4f; // 1.5 seconds to be able to use it again
    [SerializeField] bool isDashing = false; // To check if the player is dashing or not
    [SerializeField] bool canDash = true; // To be able to dash again

    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    public AudioSource jumpSound;
    public AudioSource dashSound;

    

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>(); // Get the PlayerHealth component
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal"); // Get horizontal input for movement
        animator.SetFloat("Speed", Mathf.Abs(movement)); // Update animation based on movement speed

        Jump(); // Calling Jump Method
        Dashing(); // Calling Dashing Method
        
    }

    private void FixedUpdate()
    {
        
        if (isDashing)
            return; // Ignore regular movement during dash

            // Normal movement
            myRigidBody.velocity = new Vector2(playerSpeed * movement, myRigidBody.velocity.y);
      

        // Flip the player's direction based on movement
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
    }

    private void Flip()
    {
        // A method Flip to make sure the player is facing the right direction
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object the character collides with is tagged as "Ground" 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set isGrounded to true when touching the ground or obstacle
        }
    }

    private void Jump()
    {
        // A method Jump to jump by pressing space button
        bool isJumping = Input.GetKey(KeyCode.Space);
        animator.SetBool("IsJumping", isJumping); // Update jump animation
        if (isJumping && isGrounded) // Ensure the player is on the ground before jumping
        {
            jumpSound.Play();
            myRigidBody.velocity = Vector2.up * 6;
            
            isGrounded = false;
        }
    }

    /// <summary>
    /// Next part about dashing through the enemy without losing hp.
    /// </summary>
    

    private IEnumerator Dash()
    {
        canDash = false; // Disable dashing again until cooldown
        isDashing = true; // Set dashing flag to true
        playerHealth.isInvulnerable = true; // Make player invulnerable during dash

        // Ignore collisions with enemies during the dash
        Collider2D playerCollider = GetComponent<Collider2D>();
        LayerMask enemyLayer = LayerMask.GetMask("enemy"); // Ensure enemies are on a layer named "enemy"
        Collider2D[] enemyColliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D enemyCollider in enemyColliders) // Loop through all colliders
        {
            if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                Physics2D.IgnoreCollision(playerCollider, enemyCollider, true); // Temporarily disable collision
            }
        }

        animator.SetTrigger("Dash"); // Trigger the dash animation
        myRigidBody.velocity = new Vector2(dashSpeed * (isFacingRight ? 1 : -1), myRigidBody.velocity.y);

        yield return new WaitForSeconds(dashDuration); // Wait for the dash duration

        // Re-enable collisions with enemies after dashing
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                Physics2D.IgnoreCollision(playerCollider, enemyCollider, false); // Re-enable collision
            }
        }

        isDashing = false; // End dashing state
        playerHealth.isInvulnerable = false; // Remove invulnerability

        yield return new WaitForSeconds(dashCoolDown); // Wait for the cooldown period
        canDash = true; // Allow dashing again
    }

    private void Dashing()
    {
        if (Input.GetKey(KeyCode.S) && isGrounded && canDash) // If the player presses S, is on the ground, and can dash
        {
            dashSound.Play();
            StartCoroutine(Dash());
        }
    }

    
}
