using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; // Tracks whether the game is paused
    public GameObject pauseMenuUI; // Assign the Pause Menu GameObject in the Inspector

    


    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("PauseMenu").Length > 1)
        {
            Destroy(gameObject); // Avoid duplicates
            return;
        }

        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    void Update()
    {
        // Toggle the pause menu when the player presses ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Resume the game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume normal time
        isPaused = false; // Update pause state
    }

    // Pause the game
    public void PauseGame()
    {
        Debug.Log("Game Paused");
        pauseMenuUI.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Freeze time
        isPaused = true; // Update pause state
    }

    // Load the Main Menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }

    // Save the game (example placeholder, actual saving logic depends on your game)
    public void SaveGame()
    {
        var saveManager = FindObjectOfType<SaveManager>();
        if (saveManager != null)
        {
            saveManager.SaveGame();
            Debug.Log("Game Saved!"); // Debug to confirm save
        }
        else
        {
            Debug.LogError("SaveManager not found! Ensure it exists in the scene.");
        }
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
