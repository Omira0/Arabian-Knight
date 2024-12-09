using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur_Attack : MonoBehaviour
{
    // Minoatur_Attack
    public int attackDamage = 20; // Damage dealt to the player by the Minotaur's attack

    public Vector3 attackOffset; // Offset for the attack's origin
    public float attackRange = 1f; // Range of the Minotaur's attack
    public LayerMask attackMask; // LayerMask to detect valid targets for the attack

    public void Attack()
    {
        Vector3 pos = transform.position; // Starting position of the Minotaur
        pos += transform.right * attackOffset.x; // Adjust position horizontally based on attackOffset
        pos += transform.up * attackOffset.y; // Adjust position vertically based on attackOffset

        // Detect all objects within the attack range that match the specified LayerMask
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);
        foreach (Collider2D collider in hitColliders)
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.isInvulnerable) // Check if the player is not invulnerable
            {
                playerHealth.TakeDamage(attackDamage); // Apply damage to the player
                Debug.Log("Player is attacked!");
            }
            else
            {
                Debug.Log("Attack ignored: Player is invulnerable (likely dashing).");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the attack range in the Unity Editor
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
