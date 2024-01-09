using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image avatarImage;
    public GameObject dialoguePanel; // A panel to group all dialogue UI elements
    public Button continueButton; // Reference to the continue button

    private Queue<DialogueLine> sentences; // Updated to hold DialogueLine objects
    private DialogueScriptable currentDialogue;

    public PlayerInputHandler playerInputHandler; // Reference to the PlayerInputHandler
    public RangedAttackInputHandler rangedAttackInputHandler; // Reference to the RangedAttackInputHandler
    public GameObject onScreenControls; // Reference to the on-screen controls canvas

    public Timer gameTimer; // Reference to the Timer script

    void Start()
    {
        sentences = new Queue<DialogueLine>();
        dialoguePanel.SetActive(false); // Hide dialogue panel at start
    }

    public void StartDialogue(DialogueScriptable dialogue)
    {
        // Control player and UI interactions
        TogglePlayerAndUIInteractions(false);

        // Pause the game timer when dialogue starts
        if (gameTimer != null)
        {
            gameTimer.PauseTimer();
        }

        dialoguePanel.SetActive(true); // Show the dialogue panel
        currentDialogue = dialogue;

        sentences.Clear();

        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = sentences.Dequeue();

        // Retrieve the stored language setting
        Language currentLanguage = PlayerPrefs.HasKey("LanguageSetting") 
                                    ? (Language)PlayerPrefs.GetInt("LanguageSetting") 
                                    : Language.English; // Default to English if not set

        // Choose the correct language for the character's name
        string characterNameToShow = currentLanguage == Language.English 
                                    ? currentLine.characterNameEnglish 
                                    : currentLine.characterNameIndonesian;
        nameText.text = characterNameToShow;

        // Update the avatar image and its color
        avatarImage.sprite = currentLine.avatarSprite;
        avatarImage.color = currentLine.avatarColor; // Set the avatar's color

        // Choose the correct language for the dialogue line
        string lineToShow = currentLanguage == Language.English 
                            ? currentLine.lineEnglish 
                            : currentLine.lineIndonesian;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(lineToShow));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        // Re-enable player and UI interactions
        TogglePlayerAndUIInteractions(true);

        dialoguePanel.SetActive(false); // Hide the dialogue panel

        if (gameTimer != null)
        {
            gameTimer.ResumeTimer();
        }

        Debug.Log("End of dialogue.");
    }

    private void TogglePlayerAndUIInteractions(bool enable)
    {
        playerInputHandler.SetInDialogue(!enable); // Enable/disable player inputs
        rangedAttackInputHandler.SetInDialogue(!enable); // Enable/disable ranged attacks

        // Enable/disable on-screen controls
        if (onScreenControls != null)
            onScreenControls.SetActive(enable);
    }
}
