using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PreLevel3 : MonoBehaviour
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
                case 7:
                    Panel8();
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
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:" + "\n" +PlayerNameHandler.Instance.playerName + ", the treasures you’ve claimed so far are only a fraction of what this world holds." +
            " \nBeyond gold and jewels lies ancient power—hidden knowledge that few dare to seek."));
    }
    private void Panel2()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Deep in the jungle lies the Temple of the Serpent Queen. She is a guardian of ancient magic, her wisdom as vast as her cunning. Some say her power rivals that of legends themselves.\"\r\n"));
    }

    private void Panel3()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"But tread carefully. Her temple is no place for the faint of heart. Those who enter must face her trials, and none have ever left unchanged.\"\r\n"));
    }

    private void Panel4()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"She may hold the key to something far greater than riches. What lies ahead is more than just an adventure—it’s destiny.\"\r\n"));
    }

    private void Panel5()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nI am ready, Let's find her!"));
    }
    private void Panel6()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Serpent Queen:\r\n\"So, another seeker dares enter my domain. What brings you here, mortal? Gold? Power? Or sheer foolishness?\"\r\n"));
    }
    private void Panel7()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nI'm here for what you protect. Whatever lies within your temple, I will claim it."));
    }
    private void Panel8()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Serpent Queen: \r\n\"Brave words for one so small. But courage alone will not save you. Prove your worth, or be consumed by my wrath!\"\r\n"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene
    }
}
