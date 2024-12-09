using System.Collections;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Collider2D playerCollider;
    public Animator animator;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 5f; // Speed of the dash
    [SerializeField] private float dashDuration = 0.3f; // Duration of the dash
    [SerializeField] private float dashCooldown = 10f; // Cooldown before dashing again

    private bool canDash = true; // To track if the player can dash
    [SerializeField] private LayerMask enemyLayer; // Layer of enemies to ignore during the dash
    [SerializeField] private bool isFacingRight = true; // To determine the player's facing direction
    [SerializeField] private bool isGrounded = true; // To check if the player is on the ground

    void Awake()
    {
        // Get necessary components
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        // Debug logs for validation
        Debug.Log($"Player Layer: {gameObject.layer}");
        Debug.Log($"Enemy Layer: {LayerMask.NameToLayer("Enemy")}");
    }

    void Update()
    {
        // Trigger dash on "S" key press if grounded and allowed
        if (Input.GetKeyDown(KeyCode.S) && isGrounded && canDash)
        {
            PerformDash();
        }
    }

    public void PerformDash()
    {
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        canDash = false; // Disable further dashing

        // Check and validate layers
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (enemyLayer == -1)
        {
            Debug.LogError("Enemy layer does not exist! Please create a layer named 'Enemy'.");
            yield break; // Exit if the layer is invalid
        }

        // Ignore collisions with the enemy layer
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);

        float originalGravity = playerRb.gravityScale; // Save gravity scale
        playerRb.gravityScale = 0; // Disable gravity

        // Apply dash velocity based on the facing direction
        Vector2 dashVelocity = new Vector2(isFacingRight ? dashSpeed : -dashSpeed, 0);
        playerRb.velocity = dashVelocity;

        // Trigger dash animation
        if (animator != null)
        {
            animator.SetTrigger("Dash");
        }

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Reset velocity and gravity
        playerRb.velocity = Vector2.zero;
        playerRb.gravityScale = originalGravity;

        // Re-enable collisions with the enemy layer
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);

        // Cooldown before allowing the next dash
        yield return new WaitForSeconds(dashCooldown);
        canDash = true; // Re-enable dashing
    }
}
