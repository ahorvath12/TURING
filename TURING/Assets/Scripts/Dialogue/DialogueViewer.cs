using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueObject;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;

public class DialogueViewer : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] Transform parentOfResponses;
    [SerializeField] Button prefab_btnResponse;
    [SerializeField] Text txtMessage;
    DialogueController controller;

    [DllImport("__Internal")]
    private static extern void openPage(string url);


    private void Start()
    {
        controller = GetComponent<DialogueController>();
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue();

        // Start dialogue
        var curNode = controller.GetCurrentNode();
        txtMessage.text = "";
    }


    public static void KillAllChildren(UnityEngine.Transform parent)
    {
        UnityEngine.Assertions.Assert.IsNotNull(parent);
        for (int childIndex = parent.childCount - 1; childIndex >= 0; childIndex--)
        {
            UnityEngine.Object.Destroy(parent.GetChild(childIndex).gameObject);
        }
    }

    private void OnNodeSelected(int indexChosen)
    {
        //Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }

    private void OnNodeEntered(Node newNode)
    {
        KillAllChildren(parentOfResponses);
        //Debug.Log(newNode.tags[0] != "START");
        if (newNode.tags[0] != "START")
        {
            StartCoroutine(WaitForResponse(newNode));
        }
        else
        {
            Debug.Log("Get responses");
            for (int i = newNode.responses.Count - 1; i >= 0; i--)
            {
                int currentChoiceIndex = i;
                var response = newNode.responses[i];
                var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
                responceButton.GetComponentInChildren<Text>().text = response.displayText;
                Debug.Log(response.displayText);
                responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
                responceButton.onClick.AddListener(delegate { UpdateMessageViewer(response.displayText); });
            }
        }


        //UnityAction typeResponsesAfterMessage = delegate {
            
        //};
    }

    private IEnumerator WaitForResponse(Node curNode)
    {
        yield return new WaitForSeconds(0.3f);
        txtMessage.text += "Player B: " + curNode.text + "\n";
        gm.UpdatedMessageViewer();
        for (int i = curNode.responses.Count - 1; i >= 0; i--)
            {
                int currentChoiceIndex = i;
                var response = curNode.responses[i];
                var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
                responceButton.GetComponentInChildren<Text>().text = response.displayText;
            //Debug.Log(response.displayText);
                responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
            responceButton.onClick.AddListener(delegate { UpdateMessageViewer(response.displayText); });
            //responceButton.onClick.AddListener(delegate { gm.UpdatedMessageViewer(); });
            }
        
    }


    private void UpdateMessageViewer(string response)
    {
        txtMessage.text += "Player A: " + response + "\n";
    }
}