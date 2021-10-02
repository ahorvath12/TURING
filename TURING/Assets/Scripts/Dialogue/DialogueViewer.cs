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
        Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }

    private void OnNodeEntered(Node newNode)
    {
        txtMessage.text = newNode.text;
        KillAllChildren(parentOfResponses);

        //UnityAction typeResponsesAfterMessage = delegate {
            for (int i = newNode.responses.Count - 1; i >= 0; i--)
            {
                int currentChoiceIndex = i;
                var response = newNode.responses[i];
                var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
                responceButton.GetComponentInChildren<Text>().text = response.displayText;
            Debug.Log(response.displayText);
                responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
            }
        //};
    }

}