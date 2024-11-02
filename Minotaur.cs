using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Minotaur : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy Died!");
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;

        this.enabled = false;
    }
    [SerializeField] float movement;
    public bool isFacingRight = true;
    [SerializeField] const float MinotaurSpeed = 2.5f;
    public Rigidbody2D myRigidBody;
    private void FixedUpdate()
    {
        myRigidBody.velocity = new Vector2(MinotaurSpeed * movement, myRigidBody.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
    }
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }
}