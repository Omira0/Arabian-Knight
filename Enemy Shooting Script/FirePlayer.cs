using UnityEngine;

public class FirePlayer : MonoBehaviour
{
    public float speed = 20f; // Speed of the fire
    public Rigidbody2D rb; // Rigidbody2D for movement
    [SerializeField] private int fireDamage = 20; // Damage dealt by fire

    private void Start()
    {
        float facingDirection = transform.localScale.x > 0 ? 1f : -1f;
        rb.velocity = new Vector2(facingDirection * speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(fireDamage); // Apply damage to the player
                Debug.Log($"Enemy fire hit the player and dealt {fireDamage} damage!");
            }
            Destroy(gameObject); // Destroy the fire projectile
        }
        else if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy fire on hitting non-enemy objects
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy the fire if it leaves the screen
    }
}
