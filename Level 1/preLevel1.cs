using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PreLevel1 : MonoBehaviour
{
    [SerializeField] private float revealSpeed = 0.05f; // Speed of text reveal
    private TextMeshProUGUI dialogueText; // Dialogue text object
    private int currentPanelIndex = 0; // Current panel being displayed
    [SerializeField] private float waitingTime = 1f; // Time to wait after text reveal
    private Coroutine currentRevealCoroutine;
    public List<GameObject> panels; // List of panel GameObjects
    private void Start()
    {
        ShowPanel(0); // Start with the first panel
    }

    public void ShowNextPanel()
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

            dialogueText = panels[panelIndex].GetComponentInChildren<TextMeshProUGUI>();

            if (currentRevealCoroutine != null)
            {
                StopCoroutine(currentRevealCoroutine);
            }

            switch (currentPanelIndex)
            {
                case 0:
                    Panel1();
                    break;
                case 1:
                    Panel2();
                    break;
                case 2:
                    Panel3();
                    break;
                case 3:
                    Panel4();
                    break;
                default:
                    Debug.LogWarning("No method defined for this panel.");
                    break;
            }
        }
    }

    private IEnumerator RevealText(string message)
    {
        dialogueText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(revealSpeed);
        }
        yield return new WaitForSeconds(waitingTime);
    }

    private void Panel1()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"This is it—the legendary cave of Ali Baba. The treasure lies within, but the door will only open to those who know the secret words.\"\r\n"));
    }

    private void Panel2()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\n\"Secret words? How do I find them?\"\r\n"));
    }

    private void Panel3()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Look! A guard blocks the path. He won’t let you through without a fight.\"\r\n"));
    }

    private void Panel4()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Guard:\r\n\"You seek entry to the cave? Ha! Only the worthy may learn the secret. Prove your strength, and I might tell you.\"\r\n"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene
    }
}
