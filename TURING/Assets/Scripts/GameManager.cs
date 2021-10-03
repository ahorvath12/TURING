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
    public GameObject gameOverUI;
    public ScrollRect scroll;
    public Text dialogueText;

    [Header("Guess Stuff")]
    public bool[] human;
    public GameObject[] guessOutcomes;

    private int character;
    private float timeRemaining = 60;

    private bool startCounting = false;
    private bool scrollDrag = false, updatedDialogue = false;

    void Start()
    {
        Screen.SetResolution(561, 483, false);
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
                mainUI.GetComponent<CanvasGroup>().interactable = false;
            }
        }
        

        if (scroll.IsActive() && updatedDialogue)
        {
            scroll.normalizedPosition = new Vector2(0, 0);
            updatedDialogue = false;
        }
    }

    //Basic Buttons
    public void StartButton()
    {
        mainUI.SetActive(true);
        gameOverUI.SetActive(false);
        startScreen.SetActive(false); 
        startCounting = true;
        timeRemaining = 60;
        character = Random.Range(0, twineStories.Length);
        GetComponent<DialogueController>().twineText = twineStories[character];
        GetComponent<DialogueController>().InitializeDialogue();
        dialogueText.text = "";
    }

    public void ExitButton()
    {
        mainUI.SetActive(false);
        gameOverUI.SetActive(false);
        startScreen.SetActive(true); 
        startCounting = false;
        //GetComponent<DialogueController>().twineText = twineStories[Random.Range(0, twineStories.Length)];
        //timeRemaining = 60;

    }
    public void HelpButton()
    {

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
