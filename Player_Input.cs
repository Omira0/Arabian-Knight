using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Input : MonoBehaviour
{
    public Rigidbody2D myRigidBody; //Assign a rigid body to the player.
    [SerializeField] float movement; //To move the player.
    [SerializeField] const int playerSpeed = 4; //Player speed.
    [SerializeField] bool isFacingRight = true; //A bool to check which direction the player is facing.
    [SerializeField] bool isGrounded = false; //A bool to check whether the player is on the ground or not, it is false because it is only true if the player is on the ground.
    public Animator animator; //The animator controller. 

    //Global Varibals for attacking
    //public Transform attackPoint;
    //public float attackRange = 0.5f;
    //public LayerMask enemyLayers;

  

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        //Making the char going right and left

        animator.SetFloat("Speed", Mathf.Abs(movement));
        //Making the char animation Player_Movement works 

        Jump(); //Calling Jump Method.

        //Dashing(); //Calling Dashing Method.

        JumpAttacking(); //Calling JumpAttacking Method.

    }
    private void FixedUpdate()
    {
        //Assign a specific speed to the movement.
        myRigidBody.velocity = new Vector2(playerSpeed * movement, myRigidBody.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
    }
    private void Flip()
    {
        //A method Flip to make sure the player is facing the right direction.
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

        //A method Jump to jump by pressing space button.
        bool isJumping = Input.GetKey(KeyCode.Space);
        //Making the char to jump
        animator.SetBool("IsJumping", isJumping);
        //To make sure the player is on the ground before jumping.
        if (isJumping && isGrounded)
        {
            myRigidBody.velocity = Vector2.up* 6;
            isGrounded = false;
        }
    }



   /* private void Dashing()
    {
        // A method to dash by pressing the 'S' button.
        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            animator.SetTrigger("Dash"); // Use Trigger for one-time action
            myRigidBody.velocity = new Vector2(isFacingRight ? playerSpeed * 2 : -playerSpeed * 2, myRigidBody.velocity.y); // Dash in the facing direction
        }
    }*/

    private void JumpAttacking()
    {
        //A method JumpAttacking to jump and attack by pressing w button.
        bool isJumpAttacking = Input.GetKeyDown(KeyCode.W);
        animator.SetBool("IsJumpAttacking", isJumpAttacking);
        //To make sure the player is on the ground before jumping.
        if (isJumpAttacking && isGrounded)
        {
            myRigidBody.velocity = Vector2.up * 6;
            isGrounded = false;
        }
    }

}
