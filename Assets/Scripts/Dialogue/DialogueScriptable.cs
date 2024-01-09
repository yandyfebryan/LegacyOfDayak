using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptable : ScriptableObject
{
    public DialogueLine[] dialogueLines;
    //public bool isUIDisplayed = true; // Whether the UI should be active for the entire dialogue
}

[System.Serializable]
public class DialogueLine
{
    public string characterNameEnglish; // English version of the character's name
    public string characterNameIndonesian; // Indonesian version of the character's name
    public string lineEnglish; // English version of the line
    public string lineIndonesian; // Indonesian version of the line
    public Sprite avatarSprite;
    public Color avatarColor = Color.white; // Default to white
}

