using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeineFiring : MonoBehaviour
{
    Transform player;
    Animator animator;
    private float fireCooldown = 3f; // Cooldown for throwing fire
    [SerializeField] private float fireRange = 5f; // Range to trigger throwing

    private FirstThief firstThief; // Reference to enemy behavior

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        firstThief = GetComponent<FirstThief>();

        if (player == null) Debug.LogError("Player not found!");
        if (animator == null) Debug.LogError("Animator not found!");
        if (firstThief == null) Debug.LogError("FirstThief script not found!");
    }

    private void Update()
    {
        if (player == null || firstThief == null) return;

        // Handle throwing fire logic
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        fireCooldown -= Time.deltaTime;

        if (distanceToPlayer <= fireRange && fireCooldown <= 0f)
        {
            animator.SetTrigger("Throw"); // Trigger Throw animation
            fireCooldown = 3f; // Reset cooldown
            Fire(); // Perform the firing logic
        }
    }

    private void Fire()
    {
        Debug.Log("Enemy fires a projectile!");

        // Call the firing logic from the EnemyFiring script
        GetComponent<EnemyFiring>()?.Fire();
    }
}
