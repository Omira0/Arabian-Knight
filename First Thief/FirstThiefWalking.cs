using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstThiefWalking : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 2.5f;
    FirstThief scr;
    [SerializeField] float attackRange = 2f;
    private bool canAttack = true; // Flag to prevent repeated attacks
    public float attackDistance = 1f;

    //Kick var
    [SerializeField] float kickRange =1.5f;
    public float kickDistance = 0.5f;
    public bool canKick = true;
    public float kickCooldown = 3f;

    private FirstThief firstThief; // Reference to the MonoBehaviour script to start the coroutine

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player = players[0].transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }

        rb = animator.GetComponent<Rigidbody2D>();
        scr = animator.GetComponent<FirstThief>();
        firstThief = animator.GetComponent<FirstThief>(); // Get the MonoBehaviour script for starting the coroutine

        if (rb == null) Debug.LogError("Rigidbody2D not found on the object with the animator.");
        if (scr == null) Debug.LogError("First Thief component not found!");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (scr == null || player == null) return;

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (rb.position.x > player.position.x && scr.isFacingRight)
        {
            scr.Flip();
        }
        else if (rb.position.x < player.position.x && !scr.isFacingRight)
        {
            scr.Flip();
        }

        float distance = Vector2.Distance(player.position, rb.position);
        // Debug.Log("Distance to Player: " + distance);

        // Attack logic with cooldown
        if (distance <= attackRange + attackDistance && canAttack && !firstThief.isOnCooldown)
        {
            //Debug.Log("Enemy within attack range, triggering attack.");
            animator.ResetTrigger("FirstThief_Attacking");
            animator.SetTrigger("FirstThief_Attacking");
            canAttack = false;
            firstThief.StartCoroutine(firstThief.AttackCooldown()); // Use the MonoBehaviour to start the coroutine
        }
        else if (distance > attackRange)
        {
            canAttack = true; // Reset the ability to attack if out of range
        }

        //Kicking logic
        if (distance <= kickRange + kickDistance && canKick && !firstThief.isOnCooldownk)
        {
            //Debug.Log("Enemy within kick range, triggering attack.");
            animator.ResetTrigger("Kick");
            animator.SetTrigger("Kick");
            canKick = false;
            firstThief.StartCoroutine(firstThief.KickCooldown()); // Use the MonoBehaviour to start the coroutine
        }
        else if (distance > kickRange)
        {
            canKick = true; // Reset the ability to kick if out of range
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("FirstThief_Attacking"))
        {
            animator.ResetTrigger("FirstThief_Attacking");
            canAttack = true; // Reset on exit to allow new attacks in the next walk state
        }
        else if (stateInfo.IsName("Kick"))
        {
            animator.ResetTrigger("Kick");
            canKick = true; // Reset on exit to allow new attacks in the next walk state
        }
    }
}
