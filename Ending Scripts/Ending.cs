using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
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
                case 8:
                    Panel9();
                    break;
                case 9:
                    Panel10();
                    break;
                case 10:
                    Panel11();
                    break;
                case 11:
                    Panel12();
                    break;
                case 12:
                    Panel13();
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
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nIt's over... The Jinn has been defeated!"));
    }
    
    private void Panel2()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Indeed, my friend, you have achieved what many would consider impossible. Your courage and strength are a beacon of hope for this Golden Age.\"\r\n"));
    }
    private void Panel3()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nI couldn't have done it without your guidance, Sindbad. What now?"));
    }
    
    private void Panel4()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Now, we return to Baghdad, to the Caliph himself. He will want to honor your bravery and reward you for this extraordinary feat. Come, the city awaits the return of its hero.\"\r\n"));
    }
    
    private void Panel5()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Caliph:\r\n\"Rise, noble warrior. You have not only saved Baghdad but have protected the legacy of knowledge and wisdom that defines our era. Your name will be remembered forever as the Arabian Knight.\"\r\n"));
    }
    private void Panel6()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nIt is an honor, Your Grace. I only sought to do what was right."));
    }
    private void Panel7()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Caliph:\r\n\"Right you have done, and so much more. Take this, a gift worthy of a champion.\"\r\n"));
    }
    private void Panel8()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Caliph:\r\n\"With this sword and armor, you shall stand as a true Arabian Knight, protector of Baghdad and beyond. May they serve you well in future battles.\"\r\n"));
    }
    private void Panel9()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Well done," + PlayerNameHandler.Instance.playerName + " my friend. This is your moment. The Golden Age shines brighter today because of you.\"\r\n"));
    }

    private void Panel10()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Well done, my friend—or shall I say, Arabian Knight. This is your moment. The Golden Age shines brighter today because of you.\"\r\n"));
    }
    private void Panel11()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nAnd what about you, Sindbad?"));
    }
    private void Panel12()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Ah, my role was only to guide you, but you… you have written your story in the stars.\"\r\n"));
    }
    private void Panel13()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"I will miss you, my friend. Your adventure," + PlayerNameHandler.Instance.name + ", the Arabian Knight, will be forever immortalized within the tales of One Thousand and One Nights.\"\r\n"));
    }
   

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene
    }
    public void MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
