using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Attack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint; // Origin of attack
    public LayerMask enemyLayers; // Enemy detection layers
    public float attackRange = 0.5f; // Melee attack range
    public int attackDamage = 40; // Melee attack damage

    public Transform firePoint; // Fire projectile origin
    public GameObject firePrefab; // Prefab of the fire projectile

    private float attackRate = 2f; // Melee attack rate
    private float nextAttackTime = 0f;

    private float fireRate = 2f; // Cooldown for firing
    private float nextFireTime = 0f; // Timer for next fire

    public AudioSource attackSound;
    public AudioSource fireSound;

    private void Update()
    {
        // Melee attack logic
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse for melee attack
            {
                Attacking();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        // Firing logic with cooldown and scene restriction
        if (Time.time >= nextFireTime && SceneManager.GetActiveScene().name == "Level 4")
        {
            if (Input.GetMouseButtonDown(1)) // Right mouse for fire
            {
                Firing();
                nextFireTime = Time.time + fireRate; // Apply cooldown
            }
        }
    }

    private void Attacking()
    {
        animator.SetTrigger("Attack");
        attackSound.Play();

        // Detect enemies within the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyBase enemyComponent = enemy.GetComponent<EnemyBase>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
                Debug.Log($"Melee attack hit {enemy.name}");
            }
        }
    }

    private void Firing()
    {
        animator.SetTrigger("Throw");
        fireSound.Play();
        // Instantiate the fire projectile
        Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Fire projectile launched!");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        // Draw a wireframe sphere to visualize the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
