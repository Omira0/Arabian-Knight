using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameHandler : MonoBehaviour
{
    public static PlayerNameHandler Instance; // Singleton instance
    public TMP_InputField nameInputField; // Input field for player's name
    public TextMeshProUGUI greetingText; // Text object for displaying the name
    private string playerNameKey = "PlayerName"; // Key for saving the name in PlayerPrefs
    private string defaultName = "Stranger"; // Default name if no name is provided
    public string playerName { get; private set; } // Public property for accessing player's name

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        // Load the player's name or use the default
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            playerName = PlayerPrefs.GetString(playerNameKey);
        }
        else
        {
            playerName = defaultName; // Assign default name if no saved name is found
        }
    }

    // Called when the player submits their name
    public void SaveName()
    {
        if (nameInputField != null)
        {
            playerName = nameInputField.text.Trim(); // Trim whitespace from input

            if (string.IsNullOrEmpty(playerName))
            {
                playerName = defaultName; // Assign default name if input is empty
            }

            PlayerPrefs.SetString(playerNameKey, playerName); // Save the name
            PlayerPrefs.Save();

            Debug.Log($"Player name saved: {playerName}");
        }
        else
        {
            Debug.LogError("NameInputField is not assigned!");
        }
    }

    // Called to display the player's name later
    public void LoadName()
    {
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            playerName = PlayerPrefs.GetString(playerNameKey);

            if (greetingText != null)
            {
                greetingText.text = $"Welcome back, {playerName}!";
            }
        }
        else
        {
            playerName = defaultName;

            if (greetingText != null)
            {
                greetingText.text = "Welcome, Stranger!";
            }
        }

        Debug.Log($"Player name loaded: {playerName}");
    }
}
