using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Minotaur_Walk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 2.5f;
    MinotaurMotor scr;
    public float attackRange = 2f;
    private bool canAttack = true; // Flag to prevent repeated attacks
    public float attackDistance = 1f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
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
        scr = animator.GetComponent<MinotaurMotor>();
        if (rb == null) Debug.LogError("Rigidbody2D not found on the object with the animator.");
        if (scr == null) Debug.LogError("Minotaur component not found!");
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (scr == null || player == null) return;
        Vector2 target = new Vector2(player.position.x -2, rb.position.y);
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
        //Debug.Log("Distance to Player: " + distance);
        // Attack logic
        if (distance <= attackRange + attackDistance && canAttack)
        {
            // Debug.Log("Minotaur within attack range, triggering attack.");
            animator.ResetTrigger("Minotaur_Slashing");
            animator.SetTrigger("Minotaur_Slashing");
            canAttack = false; // Prevent further attacks until reset
        }
        else if (distance > attackRange)
        {
            canAttack = true; // Reset the ability to attack if out of range
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Minotaur_Slashing");
        canAttack = true; // Reset on exit to allow new attacks in the next walk state
    }
}