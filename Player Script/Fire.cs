using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed = 20f; // Speed of the fire
    public Rigidbody2D rb; // Rigidbody2D for movement
    public AudioSource audioShoot; // Sound for shooting
    [SerializeField] private int fireDamage = 80; // Damage dealt by fire

    private void Start()
    {
        // Set the velocity of the fire projectile
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision with the player
        if (other.CompareTag("Player")) return;

        // Destroy the fire projectile
        Destroy(gameObject);

        // Check if the fire hit an enemy
        if (other.CompareTag("Enemy"))
        {
            // Try to get the enemy's health component
            FirstThiefHealth enemy = other.GetComponent<FirstThiefHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(fireDamage); // Apply damage
                Debug.Log($"Fire hit {other.name} and dealt {fireDamage} damage!");
            }
            
        }
    }

    private void OnBecameInvisible()
    {
        // Destroy the fire object when it leaves the screen
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Play the shoot sound when the fire collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (audioShoot != null)
            {
                audioShoot.Play();
            }
        }
    }
}
