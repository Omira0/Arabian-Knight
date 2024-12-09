using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PAfterLevel2 : MonoBehaviour
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
                case 4:
                    Panel5();
                    break;
                case 5:
                    Panel6();
                    break;
                case 6:
                    Panel7();
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
        currentRevealCoroutine = StartCoroutine(RevealText("'Strong Sound'" + "\r\nEnough!"));
    }
    private void Panel2()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Ali Baba:\r\n\"Impressive. I’ve been watching you, traveler. Few have the skill—or the courage—to defeat one of my guards, let alone two.\"\r\n"));
    }

    private void Panel3()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Ali Baba:\r\n\"I am sure you know, who am I\"\r\n"));
    }
    private void Panel4()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\n\"The legendry Ali Baba himself. And now what? Will you fight me yourself?\"\r\n"));
    }

    private void Panel5()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("(Thieves are laughing)" + "\nAli Baba:\r\n\"No need for that. You’ve proven yourself more than capable, and I respect your resolve. For that, I offer you this.\"\r\n"));
    }

    private void Panel6()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Ali Baba:\r\n\"Take this as a token of my respect and a reward for your bravery. But know this—your journey doesn’t end here. Greater challenges await you beyond this cave.\"\r\n"));
    }
    private void Panel7()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Good luck, traveler. You’ll need it.\""));
    }



    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene
    }
}
