using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstThief : MonoBehaviour
{
    // First Thief
    [SerializeField] private float movement;
    public bool isFacingRight = true;
    [SerializeField] private const float firstThiefSpeed = 2.5f;
    public Rigidbody2D myRigidBody;

    public bool isOnCooldown = false; // Cooldown flag for attacking
    public bool isOnCooldownk = false; //Cooldown flag for kicking

    private void FixedUpdate()
    {
        myRigidBody.velocity = new Vector2(firstThiefSpeed * movement, myRigidBody.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    public IEnumerator AttackCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(0.75f); // 0.75 second cooldown for attacking
        isOnCooldown = false;
        Debug.Log("Cooldown ended, ready to attack again.");
    }
    public IEnumerator KickCooldown()
    {
        isOnCooldownk = true;
        yield return new WaitForSeconds(2f); // 2 second cooldown for kicking
        isOnCooldownk = false;
        Debug.Log("Cooldown ended, ready to attack again.");
    }
}
