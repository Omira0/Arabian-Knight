using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class Panel1Controller : MonoBehaviour
{
    [SerializeField] private float revealSpeed = 0.05f;
    private TextMeshProUGUI dilougeText;
    private int currentPanelIndex = 0;
    [SerializeField] private float wiatingTime = 1f;
    private Coroutine currentRevealCoroutine;
    public List<GameObject> panels;
    // Start is called before the first frame update
    void Start()
    {
        ShowPanel(0); // Start with Panel 1
    }


    public void showNextPanel()
    {
        ShowPanel(currentPanelIndex + 1);
    }

    private void ShowPanel(int panelIndex)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(false);
        }

        if (panelIndex >= 0 && panelIndex < panels.Count)
        {
            panels[panelIndex].SetActive(true);
            currentPanelIndex = panelIndex;

            dilougeText = panels[panelIndex].GetComponentInChildren<TextMeshProUGUI>();

            if(currentRevealCoroutine != null)
            {
                StopCoroutine(currentRevealCoroutine);
            }

            switch (currentPanelIndex) {
                case 0:
                    panel1();
                    break;
                case 1:
                    panel2();
                    break;

                default:
                    Debug.LogWarning("No method defined for this panel.");
                    break;
            }
        }
    }

    private IEnumerator RevealText(string message)
    {
        dilougeText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            dilougeText.text += letter;
            yield return new WaitForSeconds(revealSpeed);
        }
        yield return new WaitForSeconds(wiatingTime);

        
    }

    private void panel1()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("During my travels, I learned of a Greek monster called the Minotaur—half-man, half-bull, lurking in a deadly labyrinth."));
    }
    private void panel2()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("You must defeat it as your first trial. Prove your skill, and we’ll embark on our true mission together!"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
