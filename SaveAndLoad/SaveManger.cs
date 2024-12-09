using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/saveData.json";
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    public bool SaveExists()
    {
        return File.Exists(savePath); // Check if the save file exists
    }

    public void SaveGame()
    {
        SaveData data = new SaveData
        {
            sceneName = SceneManager.GetActiveScene().name // Save the current scene name
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved! Scene: " + data.sceneName);
    }

    public void LoadGame()
    {
        if (SaveExists())
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            SceneManager.LoadScene(data.sceneName); // Load the saved scene

            Debug.Log("Game Loaded! Scene: " + data.sceneName);
        }
        else
        {
            Debug.LogWarning("No save file found!");
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public string sceneName; // Name of the saved scene
    }
}