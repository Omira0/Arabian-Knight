using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// To control the jumping attack its close to Attack script but some different making sure the player on ground before jumping, damage is 20 not 40 
/// </summary>
public class Player_JumpingAttack : MonoBehaviour
{
    //Global Varibals for attacking
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    [SerializeField] float attackRange = 0.5f;
    [SerializeField] int attackDamage = 20;

    [SerializeField] float attackRate = 2f;
    [SerializeField] float nextAttackTime = 1f;

    [SerializeField] bool isGrounded = false; // A bool to check whether the player is on the ground or not, it is false because it is only true if the player is on the ground
    //private bool isJumpAttacking = false;

    public Rigidbody2D rigidBody;
    



    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                //isJumpAttacking = true;
                isGrounded = false;

                animator.SetTrigger("JumpAttack");
                rigidBody.velocity = Vector2.up * 6;
                JumpAttack();
                    
                nextAttackTime = Time.time + 1f / attackRate;
                
                
            }
        }
    }

    private void JumpAttack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyBase enemyComponent = enemy.GetComponent<EnemyBase>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
                Debug.Log("We hit " + enemy.name);
            }
            else
            {
                Debug.LogWarning("Hit an object without an EnemyBase component: " + enemy.name);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object the character collides with is tagged as "Ground" 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set isGrounded to true when touching the ground or obstacle
        }
    }

}

