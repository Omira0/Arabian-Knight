using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameHandler : MonoBehaviour
{
    public static PlayerNameHandler Instance;
    public TMP_InputField nameInputField;  // Drag your InputField here in the Inspector
    public TextMeshProUGUI greetingText;  // Drag a Text object to display the name
    private string playerNameKey = "PlayerName";  // Key for saving the name
    private string defaultName = "Stranger";
    public string playerName { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad (gameObject);
        }
    }

    // Called when the player submits their name
    public void SaveName()
    {
         playerName = nameInputField.text.Trim();  // Remove any leading or trailing spaces

        if (string.IsNullOrEmpty(playerName))  // If the input is empty
        {
            playerName = defaultName;  // Assign the default name
        }

        PlayerPrefs.SetString(playerNameKey, playerName);  // Save the name
        PlayerPrefs.Save();

        Debug.Log($"Player name saved: {playerName}");
    }

    // Use this to load the player's name later
    public void LoadName()
    {
        if (PlayerPrefs.HasKey(playerNameKey))  // Check if a name exists
        {
            string savedName = PlayerPrefs.GetString(playerNameKey);
            greetingText.text = $"Welcome back, {savedName}!";  // Update text to show the name
            Debug.Log($"Player name loaded: {savedName}");
        }
        else
        {
            greetingText.text = "Welcome, Stranger!";
        }
    }
}
