using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueScriptable dialogue;
    private bool dialogueTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!dialogueTriggered && other.CompareTag("Player"))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialogueTriggered = true;
        }
    }
}
