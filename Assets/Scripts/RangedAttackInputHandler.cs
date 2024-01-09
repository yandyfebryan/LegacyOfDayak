using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RangedAttackInputHandler : MonoBehaviour
{
    private RangedAttack rangedAttack;
    [SerializeField] private Button rangedAttackButton;

    private bool inDialogue = false; // Flag to check if in dialogue

    private void Start()
    {
        rangedAttack = GetComponent<RangedAttack>();

        // Add EventTrigger for ranged attack button
        rangedAttackButton.gameObject.AddComponent<EventTrigger>().triggers = CreateTriggerList(PerformRangedAttack, null);
    }

    private List<EventTrigger.Entry> CreateTriggerList(UnityEngine.Events.UnityAction pointerDown, UnityEngine.Events.UnityAction pointerUp)
    {
        List<EventTrigger.Entry> entryList = new List<EventTrigger.Entry>();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((eventData) => { pointerDown?.Invoke(); });
        entryList.Add(pointerDownEntry);

        if (pointerUp != null)
        {
            EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
            pointerUpEntry.eventID = EventTriggerType.PointerUp;
            pointerUpEntry.callback.AddListener((eventData) => { pointerUp?.Invoke(); });
            entryList.Add(pointerUpEntry);
        }

        return entryList;
    }

    private void PerformRangedAttack()
    {
        if (inDialogue) return; // Prevent attack if in dialogue

        if (rangedAttack != null)
        {
            rangedAttack.FireProjectile();
        }
        else
        {
            Debug.LogWarning("RangedAttack component not found");
        }
    }

    public void SetInDialogue(bool isInDialogue)
    {
        inDialogue = isInDialogue;
    }
}
