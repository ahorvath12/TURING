using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Title Screen")]
    public GameObject startScreen;

    [Header("Main Gameplay")]
    public TextAsset[] twineStories;
    public Text timeText;
    public GameObject mainUI;
    public GameObject dialogueUI;
    public GameObject gameOverUI;
    public ScrollRect scroll;
    public Text dialogueText;

    [Header("Guess Stuff")]
    public GameObject outcomeUI;
    public Text outcomeText;
    public bool[] human;
    public GameObject[] guessOutcomes;

    private int character;
    private float timeRemaining = 45;

    private bool startCounting = false;
    private bool scrollDrag = false, updatedDialogue = false;

    void Start()
    {
        //Screen.SetResolution(561, 483, false);
        startScreen.SetActive(true);
        GetComponent<DialogueController>().twineText = twineStories[Random.Range(0, twineStories.Length)];
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
        character = Random.Range(0, twineStories.Length);
        GetComponent<DialogueController>().twineText = twineStories[character];
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
        //GetComponent<DialogueController>().twineText = twineStories[Random.Range(0, twineStories.Length)];
        //timeRemaining = 60;

    }
    public void HelpButton()
    {

    }

    public void GuessHuman(bool guess)
    {
        gameOverUI.SetActive(false);
        outcomeUI.SetActive(true);

        for(int i = 0; i < twineStories.Length; i++)
        {
            if (i == character)
                guessOutcomes[i].SetActive(true);
            else
                guessOutcomes[i].SetActive(false);
        }
        
        if (guess == human[character])
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
