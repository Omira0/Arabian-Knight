using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PreLevel4 : MonoBehaviour
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
                case 13:
                    Panel14();
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
        currentRevealCoroutine = StartCoroutine(RevealText("Serpent Queen:\r\n\"You have bested me... a rare feat indeed. This temple holds more than treasures; it guards the secrets of forgotten power.\"\r\n"));
    } 
    private void Panel2()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Serpent Queen:\r\n\"Take this book. Its magic will grant you the power of fire—a weapon strong enough to face even the mightiest of foes. But beware... such power comes at a price.\"\r\n"));
    }
    private void Panel3()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"You've done well, my friend. The Serpent Queen's gift will prove invaluable in the trials ahead.\"\r\n"));
    }
    private void Panel4()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Now, the time has come to reveal the true reason for your journey. The Caliph himself entrusted me with a mission of utmost importance—a mission that now falls upon you.\"\r\n"));
    }
    private void Panel5()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nWhat mission? Why me?"));
    }
    private void Panel6()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"Far to the west lies a Jeine, a being of immense power and malice. For centuries, it has been bound by ancient magic. But that magic weakens, and the Jeine stirs once more. If it is not stopped, it will plunge the world into chaos.\"\r\n"));
    }
    private void Panel7()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"You have proven your strength and courage, and with the power of fire, you stand a chance. The Caliph believes you are the one who can end this threat.\"\r\n"));
    }
    private void Panel8()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nThen I’ll face this Jeine. Tell me where to go."));
    }
    private void Panel9()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Sindbad:\r\n\"The path will be perilous, but I will guide you. Prepare yourself, for the final trial awaits.\"\r\n"));
    }

    private void Panel10()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Jeine:\r\n\"Who dares disturb my slumber? Foolish mortal, do you not know who I am?\"\r\n"));
    }
    private void Panel11()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nI know exactly who you are—a force of destruction that has plagued this world for far too long. Your reign ends here."));
    }
    private void Panel12()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Jeine:\r\n\"Ends? You amuse me, little one. Do you truly believe you can challenge the might of a being older than time itself?\"\r\n"));
    }
    private void Panel13()
    {
        currentRevealCoroutine = StartCoroutine(RevealText($"{PlayerNameHandler.Instance.playerName}:\r\nI don’t believe—I know. Your tyranny ends today."));
    }
    private void Panel14()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Jeine:\r\n\"Very well, then. Let us see if you are worthy to face the wrath of a Jeine. Prepare yourself for oblivion!\"\r\n"));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene
    }
}
