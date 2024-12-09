using System.Collections;
using UnityEngine;

public class JeineWalkingToAttack : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 2.5f;
    FirstThief scr;
    [SerializeField] float attackRange = 2f;
    private bool canAttack = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = animator.GetComponent<Rigidbody2D>();
        scr = animator.GetComponent<FirstThief>();

        if (player == null) Debug.LogError("Player not found!");
        if (rb == null) Debug.LogError("Rigidbody2D not found!");
        if (scr == null) Debug.LogError("FirstThief component not found!");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || scr == null) return;

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // Flip the enemy if necessary
        if (rb.position.x > player.position.x && scr.isFacingRight)
        {
            scr.Flip();
        }
        else if (rb.position.x < player.position.x && !scr.isFacingRight)
        {
            scr.Flip();
        }

        // Handle attacking logic
        float distanceToPlayer = Vector2.Distance(rb.position, player.position);
        if (distanceToPlayer <= attackRange && canAttack)
        {
            animator.SetTrigger("FirstThief_Attacking");
            canAttack = false;
            scr.StartCoroutine(AttackCooldown());
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("FirstThief_Attacking"))
        {
            animator.ResetTrigger("FirstThief_Attacking");
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1f); // 1-second cooldown for attacking
        canAttack = true;
    }
}
