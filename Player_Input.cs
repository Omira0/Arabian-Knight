using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Input : MonoBehaviour
{
    public Rigidbody2D myRigidBody; 
    [SerializeField] float movement;
    [SerializeField] const int playerSpeed = 4;
    [SerializeField] bool isFacingRight = true;
    //[SerializeField] bool isJumping;
    [SerializeField] bool isGrounded = false;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        //Making the char going right and left

        animator.SetFloat("Speed", Mathf.Abs(movement));
        //Making the char animation Player_Movement works 

        Jump();

        Attacking();

        Striking();

        Dashing();

        JumpAttacking();

    }
    private void FixedUpdate()
    {
        myRigidBody.velocity = new Vector2(playerSpeed * movement, myRigidBody.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }
    // Collision detection to check if the character is on an obstacle or ground
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
        bool isJumping = Input.GetKey(KeyCode.Space);
        //Making the char to jump
        animator.SetBool("IsJumping", isJumping);

        if (isJumping && isGrounded)
        {
            myRigidBody.velocity = Vector2.up* 6;
            isGrounded = false;
        }
    }

    private void Attacking()
    {
        
        bool isAttacking = Input.GetMouseButtonDown(0);
        animator.SetBool("IsAttacking", isAttacking);
    }
    private void Striking()
    {
        bool isStriking = Input.GetMouseButtonDown(1);
        animator.SetBool("IsStriking", isStriking);
    }

    private void Dashing()
    {
        bool isDashing = Input.GetKeyDown(KeyCode.S);
        animator.SetBool("IsDashing", isDashing);
    }
    
    private void JumpAttacking()
    {
        bool isJumpAttacking = Input.GetKeyDown(KeyCode.W);
        animator.SetBool("IsJumpAttacking", isJumpAttacking);

        if (isJumpAttacking && isGrounded)
        {
            myRigidBody.velocity = Vector2.up * 6;
            isGrounded = false;
        }
    }

}
