using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timeText;
    public GameObject mainUI;
    public GameObject gameOverUI;
    public ScrollRect scroll;

    private float timeRemaining = 60;

    private bool scrollDrag = false, updatedDialogue = false;

    void Start()
    {
        Screen.SetResolution(561, 483, false);
    }

    void Update()
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

        if (scroll.IsActive() && updatedDialogue)
        {
            scroll.normalizedPosition = new Vector2(0, 0);
            updatedDialogue = false;
        }
    }

    public void UpdatedMessageViewer()
    {
        Debug.Log("updated");
        updatedDialogue = true;
    }

    public void IsDragging(bool val)
    {
        scrollDrag = val;
    }
}
