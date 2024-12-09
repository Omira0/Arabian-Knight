using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public GameObject firePrefab; // Prefab of the fire projectile
    public Transform firePoint; // The point where the fire is instantiated
    public float fireRate = 3f; // Time interval between shots
    public float fireSpeed = 5f; // Speed of the fire projectile
    private float nextFireTime = 0f; // Timer for firing

    private Animator animator; // Reference to the Animator
    private FirstThief firstThief; // Reference to the FirstThief class

    private void Start()
    {
        firstThief = GetComponent<FirstThief>();
        animator = GetComponent<Animator>();

        if (firstThief == null)
            Debug.LogError("FirstThief component is missing on the enemy!");
        if (animator == null)
            Debug.LogError("Animator component is missing on the enemy!");
    }

    private void Update()
    {
        // Handle firing every `fireRate` seconds
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate; // Reset the timer
        }

        // Flip the enemy if necessary
        if (firstThief != null &&
            (firstThief.myRigidBody.velocity.x < 0 && firstThief.isFacingRight ||
             firstThief.myRigidBody.velocity.x > 0 && !firstThief.isFacingRight))
        {
            firstThief.Flip();
        }
    }

    public void Fire()
    {
        if (firePrefab == null || firePoint == null)
        {
            Debug.LogError("FirePrefab or FirePoint is not assigned!");
            return;
        }

        // Trigger firing animation
        animator.SetTrigger("Throw");

        // Instantiate the fire projectile
        GameObject fire = Instantiate(firePrefab, firePoint.position, Quaternion.identity);

        // Determine the direction the enemy is facing
        float facingDirection = firstThief.isFacingRight ? 1f : -1f;

        // Flip the fire projectile if the enemy is facing left
        if (facingDirection < 0)
        {
            Vector3 fireScale = fire.transform.localScale;
            fireScale.x = Mathf.Abs(fireScale.x) * -1;
            fire.transform.localScale = fireScale;
        }

        // Apply velocity to the fire projectile
        Rigidbody2D fireRb = fire.GetComponent<Rigidbody2D>();
        if (fireRb != null)
        {
            fireRb.velocity = new Vector2(facingDirection * fireSpeed, 0);
        }
        else
        {
            Debug.LogWarning("Fire prefab is missing Rigidbody2D component!");
        }

        Debug.Log("Enemy fired a projectile!");
    }
}
