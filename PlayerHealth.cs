using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    public Animator animator;
    Player_Input freeze;
    
    

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt");
        if(health <= 0)
        {
            Die();
            health = 0;
        }
    }

    public void Die()
    {
        Debug.Log("Player Died!");
        animator.SetBool("isDead", true);
        Debug.Log("Player is Dead");

        this.enabled = false;
        Destroy(gameObject, 1.25f);

    }
}
