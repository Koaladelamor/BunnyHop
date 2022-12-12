using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _gui;
    [SerializeField] private GameObject _beatBar;
    [SerializeField] private Image _dialogueImage;

    [SerializeField] private GameObject _arrowGUI;

    [SerializeField] private string[] dialogues;
    [SerializeField] private int currentDialogueIndex;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private float showDialogueTimer;
    [SerializeField] private float hideDialogueTimer;
    [SerializeField] private bool dialogueOnScreen;
    [SerializeField] private bool runDialogue;

    private Dictionary<int, float> dialoguesTime = new Dictionary<int, float>();

    // Start is called before the first frame update
    private void Start()
    {
        /*for (int i = 0; i < dialogues.Length; i++)
        {
            dialoguesTime.Add(i, 2);
        }*/

        InputManager.Instance.DisableAllInputs();
        _dialogueImage.enabled = false;

        dialoguesTime.Add(0, 3f);
        dialoguesTime.Add(1, 5f);
        dialoguesTime.Add(2, 8f);
        dialoguesTime.Add(3, 15f);
        dialoguesTime.Add(4, 8f);
        dialoguesTime.Add(5, 10f);
        dialoguesTime.Add(6, 12f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (runDialogue) 
        {
            if (!dialogueOnScreen)
            {
                showDialogueTimer -= Time.deltaTime;
                if (showDialogueTimer <= 0)
                {
                    showDialogueTimer = 2f;
                    _dialogueImage.enabled = true;
                    hideDialogueTimer = dialoguesTime[currentDialogueIndex];
                    dialogueText.text = dialogues[currentDialogueIndex];
                    dialogueOnScreen = true;

                    if (currentDialogueIndex == 2)
                    { // GUI and speed indicator dialogue
                        _gui.SetActive(true);
                        _arrowGUI.SetActive(true);
                    }
                    else if (currentDialogueIndex == 3) 
                    { // Activate input
                        InputManager.Instance.EnablePlayerInput();
                    }
                }
            }

            if (dialogueOnScreen)
            {
                hideDialogueTimer -= Time.deltaTime;
                if (hideDialogueTimer <= 0)
                {
                    dialogueText.text = "";
                    _dialogueImage.enabled = false;
                    dialogueOnScreen = false;

                    if (currentDialogueIndex == 2)
                    {
                        _arrowGUI.SetActive(false);
                    }

                    if (currentDialogueIndex < dialogues.Length - 1)
                    {
                        currentDialogueIndex++;
                    }
                    else if (currentDialogueIndex >= dialogues.Length - 1) 
                    { 
                        runDialogue = false;
                        Debug.Log("No more dialogues");
                    }
                }
            }
        }
    }
}
