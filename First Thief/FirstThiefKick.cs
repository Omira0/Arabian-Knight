using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstThiefKick : MonoBehaviour
{
    // Enemy_kick
    public int kickDamage = 10; // Damage dealt to the player by the enemy's kick

    public Vector3 KickOffset; // Offset for the kick's origin
    public float kickRange = 0.5f; // Range of the enemy's kick
    public LayerMask kickMask; // LayerMask to detect valid targets for the kick

    public void Kick()
    {
        Vector3 pos = transform.position; // Starting position of the enemy
        pos += transform.right * KickOffset.x; // Adjust position horizontally based on kickOffset
        pos += transform.up * KickOffset.y; // Adjust position vertically based on kickOffset

        // Detect all objects within the kick range that match the specified LayerMask
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, kickRange, kickMask);
        foreach (Collider2D collider in hitColliders)
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.isInvulnerable) // Check if the player is not invulnerable
            {
                playerHealth.TakeDamage(kickDamage); // Apply damage to the player
                Debug.Log("Player is Kicked!");
            }
            else
            {
                Debug.Log("Kick ignored: Player is invulnerable (likely dashing).");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the Unity Editor
        Vector3 pos = transform.position;
        pos += transform.right * KickOffset.x;
        pos += transform.up * KickOffset.y;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, kickRange);
    }
}
