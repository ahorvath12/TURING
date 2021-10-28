using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Title Screen")]
    public GameObject startScreen;

    [Header("Main Gameplay")]
    public Character[] characters;
    public Text timeText;
    public GameObject mainUI;
    public GameObject dialogueUI;
    public GameObject gameOverUI;
    public ScrollRect scroll;
    public Text dialogueText;

    [Header("Guess Stuff")]
    public GameObject outcomeUI;
    public Text outcomeText;
    public Text guessOutcomeUI;

    private int character;
    private float timeRemaining = 45;

    private bool startCounting = false;
    private bool scrollDrag = false, updatedDialogue = false;

    void Start()
    {
        startScreen.SetActive(true);
        GetComponent<DialogueController>().twineText = characters[Random.Range(0, characters.Length)].twineText;
    }

    void Update()
    {
        if (startCounting)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeText.text = Mathf.FloorToInt(timeRemaining % 60).ToString();
            }
            else
            {
                //end
                timeText.text = "0";
                gameOverUI.SetActive(true);
                dialogueUI.GetComponent<CanvasGroup>().interactable = false;
                startCounting = false;
            }
        }
        

        if (scroll.IsActive() && updatedDialogue)
        {
            scroll.normalizedPosition = new Vector2(0, 0);
            updatedDialogue = false;
        }
    }

    //Buttons
    public void StartButton()
    {
        mainUI.SetActive(true);
        gameOverUI.SetActive(false);
        startScreen.SetActive(false);
        dialogueUI.GetComponent<CanvasGroup>().interactable = true;
        startCounting = true;
        timeRemaining = 45;
        character = Random.Range(0, characters.Length);
        GetComponent<DialogueController>().twineText = characters[character].twineText;
        GetComponent<DialogueController>().InitializeDialogue();
        dialogueText.text = "";
    }

    public void ExitButton()
    {
        mainUI.SetActive(false);
        gameOverUI.SetActive(false);
        outcomeUI.SetActive(false);
        startScreen.SetActive(true);
        startCounting = false;

    }
    public void HelpButton()
    {

    }

    public void GuessHuman(bool guess)
    {
        gameOverUI.SetActive(false);
        outcomeUI.SetActive(true);

        guessOutcomeUI.text = characters[character].GetInfo();
        
        if (guess == characters[character].isHuman)
        {
            outcomeText.text = "Correct!";
        }
        else
        {
            outcomeText.text = "Wrong!";
        }
    }
    


    //Dialogue Viewer
    public void UpdatedMessageViewer()
    {
        updatedDialogue = true;
    }

    public void IsDragging(bool val)
    {
        scrollDrag = val;
    }
}
