using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Minotaur : EnemyBase
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
    override
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
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

        Destroy(gameObject, 1.25f);  // Adjust the delay as needed
    }
    
}