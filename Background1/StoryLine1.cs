using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryLine1 : MonoBehaviour
{
    [SerializeField] private float revealSpeed = 0.05f;
    [SerializeField] private float waitingTime = 1f;
    public TMP_InputField nameInputField; // Input field for the player's name
    public GameObject inputPanel; // Panel containing the input field
    public List<GameObject> panels; // List of all panels in order

    private PlayerNameHandler pName; // Reference to the PlayerNameHandler script
    private int currentPanelIndex = 0; // Tracks the currently active panel
    private TextMeshProUGUI dialogueText1; // Reference to the current panel's TextMeshPro

    private bool hasShownPanel2 = false; // Flag to ensure Panel 2 is only shown once
    private Coroutine currentRevealCoroutine;

    private void Start()
    {
        // Dynamically find the PlayerNameHandler if not assigned
        if (pName == null)
        {
            pName = FindObjectOfType<PlayerNameHandler>();
        }


        ShowPanel(0); // Start with Panel 1
    }



    private void ShowPanel(int panelIndex)
    {
        // Deactivate all panels
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(false);
        }

        // Activate the specified panel
        if (panelIndex >= 0 && panelIndex < panels.Count)
        {
            panels[panelIndex].SetActive(true);
            currentPanelIndex = panelIndex; // Update the current panel index

            // Dynamically update the TextMeshPro reference for the current panel
            dialogueText1 = panels[panelIndex].GetComponentInChildren<TextMeshProUGUI>();

            if (currentRevealCoroutine != null)
            {
                StopCoroutine(currentRevealCoroutine);
            }

            // Call the corresponding method to display dialogue
            switch (currentPanelIndex)
            {
                case 0:
                    Panel1();
                    break;
                case 1:
                    // Panel 2 is for player input (no text animation)
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
                case 13:
                    Panel14();
                    break;



                default:
                    Debug.LogWarning("No method defined for this panel.");
                    break;
            }
        }
    }

    public void ShowNextPanel()
    {
        ShowPanel(currentPanelIndex + 1); // Transition to the next panel
    }

    private IEnumerator RevealText(string message)
    {
        dialogueText1.text = ""; // Clear the text at the start
        foreach (char letter in message.ToCharArray())
        {
            dialogueText1.text += letter; // Add letters one by one
            yield return new WaitForSeconds(revealSpeed); // Wait between letters
        }

        yield return new WaitForSeconds(waitingTime); // Wait after the text finishes

        // Automatically transition to Panel 2 only once
        if (!hasShownPanel2 && currentPanelIndex == 0)
        {
            ShowPanel(1); // Transition to Panel 2
            hasShownPanel2 = true; // Mark Panel 2 as shown
        }
    }

    private void Panel1()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Wake up, stranger! What is your name?"));
    }

    private void Panel3()
    {
        string PLName = pName.playerName;
        currentRevealCoroutine = StartCoroutine(RevealText("My name is " + PLName + "."));
    }

    private void Panel4()
    {
        string PLName = pName.playerName;
        currentRevealCoroutine = StartCoroutine(RevealText(PLName + ", you awaken in a time of great peril."));
    }

    private void Panel5()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Where am I?"));
    }

    private void Panel6()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("You are in Baghdad, the capital of the world, the jewel of the Abbasid Caliphate. " +
            "\nHere, wisdom and knowledge flourish. " +
            "\nBut darkness has crept into its corners."));
    }

    private void Panel7()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Who are you?"));
    }
    private void Panel8()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("I am Sindbad, a traveler who has ventured to the farthest corners of the world. " +
            "\nI have seen wonders and dangers beyond imagination. " +
            "Now, I seek to preserve the culture and tales that define us. \n" +
            "I have summoned you because a grave threat looms over the Golden Age."));
    }
    private void Panel9()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("What threat?"));
    }
    private void Panel10()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Legends you may have heard—Ali Baba and the Forty Thieves, Aladdin—they are no longer mere stories." +
            " \nA dark force has brought them to life, twisting them into shadows of their former selves. " +
            "\nThey threaten not only Baghdad but the very legacy of our people."));
    }
    private void Panel11()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("What can I do? I’m just one person."));
    }
    private void Panel12()
    {
        string PLName = pName.playerName;
        currentRevealCoroutine = StartCoroutine(RevealText("Ah, but you are no ordinary person. " +
            "\nThe Caliph has entrusted me to find a warrior with both courage and wisdom. " +
            "\nThat warrior is you, " + PLName + ". " +
            "\nYou must reclaim these stories, restore their balance, and protect the knowledge of our time."));
    }
    private void Panel13()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("How do I start?"));
    }
    private void Panel14()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Begin with me. Together, we will uncover the secrets hidden within the sands of time. " +
            "\nYour journey starts now!"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
