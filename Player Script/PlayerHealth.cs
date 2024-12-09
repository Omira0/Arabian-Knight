using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] public int currentHealth;
    public Animator animator;

    public bool isInvulnerable = false;

    public HealthBar healthBar;

    public AudioSource hurtSound;
    public AudioSource dieSound;

    private void Start()
    {
        currentHealth = health;
        healthBar.SetHealth(currentHealth);
    }




    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("Hurt");
        hurtSound.Play();
        if (currentHealth <= 0)
        {
            Die();
            currentHealth = 0;
        }
    }

    public void Die()
    {
        Debug.Log("Player Died!");
        dieSound.Play();
        animator.SetBool("isDead", true);

        Debug.Log("Player is Dead");

        // Disable the script to stop further actions
        this.enabled = false;

        // Start the coroutine to reload the scene
        StartCoroutine(ReloadSceneAfterDelay());
    }

    private IEnumerator ReloadSceneAfterDelay()
    {
        yield return new WaitForSeconds(1.25f); // Wait for 1.25 seconds
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

}
