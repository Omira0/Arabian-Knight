using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class StoryLine2 : MonoBehaviour
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

            if (currentRevealCoroutine != null)
            {
                StopCoroutine(currentRevealCoroutine);
            }

            switch (currentPanelIndex)
            {
                case 0:
                    panel1();
                    break;
                case 1:
                    panel2();
                    break;
                case 2:
                    panel3();
                    break;
                case 3:
                    panel4();
                    break;
                case 4:
                    panel5();
                    break;
                case 5:
                    panel6();
                    break;
                case 6:
                    panel7();
                    break;
                case 7:
                    panel8();
                    break;
                case 8:
                    panel9();
                    break;
                case 9:
                    panel10();
                    break;
                case 10:
                    panel11();
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
        currentRevealCoroutine = StartCoroutine(RevealText("You have proven yourself to be a warrior of great strength and courage. Now, you must explore Baghdad, the heart of the Abbasid Caliphate." + "\nThe city holds secrets and treasures beyond imagination, but also dangers that test the bravest of souls."));
    }
    private void panel2()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("This is Baghdad, the city of a thousand wonders! Imagine—a place where the brilliance of scholars, poets, and traders shapes the destiny of the world."));
    }
    private void panel3()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Every corner hums with stories of innovation, trade, and unmatched splendor." + "\nLet us explore its wonders!"));
    }
    private void panel4()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("This is the Souq, Baghdad's bustling marketplace." + "\nMerchants from India, Persia, China, and Byzantium gather here, trading everything from spices and silk to stories and secrets."));
    }
    private void panel5()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("It’s said the silk you touch here might have crossed the Great Wall of China, and the gold you see may have been mined in Africa." + "\nCommerce is the lifeblood of Baghdad!"));
    }
    private void panel6()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("And here stands the House of Wisdom, the beacon of knowledge and enlightenment." + "\nScholars from all over the world come here to translate and preserve ancient texts."));
    }
    private void panel7()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Did you know? Al-Khwarizmi, the great mathematician, worked here." + "\nHis book on algebra gave the world a universal language for solving puzzles of numbers."));
    }
    private void panel8()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Look around you! Shelves filled with manuscripts on astronomy, medicine, and philosophy." + "\nHere, knowledge is treated as the most precious treasure."));
    }
    private void panel9()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Over there lies The Book of One Thousand and One Nights. Within it, legends come alive!" + "\nBut beware—it also holds dark secrets, like the tale of Ali Baba and his Forty Thieves."));
    }
    private void panel10()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("This book isn’t just stories anymore." + "\nA sinister force has brought these tales to life." + "\nAli Baba and his thieves are no longer legends—they are real, and they threaten Baghdad."));
    }
    private void panel11()
    {
        currentRevealCoroutine = StartCoroutine(RevealText("Your journey begins now." + "\nThe wisdom of this place is yours to protect, and the treasures hidden in these stories are yours to reclaim!"));
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
