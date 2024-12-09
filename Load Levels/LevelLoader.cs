using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    public Animator animator;
    public float animatorTime = 1f;

    // Update is called once per frame
    void Update()
    {
        // Check if all enemies are destroyed
        if (AllEnemiesDestroyed())
        {
            LoadNextLevel();
        }
    }

    bool AllEnemiesDestroyed()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //An Array to make sure there is no more enemies so the game will load the next scene.
        return enemies.Length == 0;
    }

    void LoadNextLevel() //A method to load the next scene.
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) //A method to display the animaton before playing the next scene.
    {
        animator.SetTrigger("Start"); // Play Animation

        yield return new WaitForSeconds(animatorTime); // Wait until the animation is finished

        SceneManager.LoadScene(levelIndex); // Load the next Scene
    }
}
