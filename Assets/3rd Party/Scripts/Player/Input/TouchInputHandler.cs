using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchInputHandler : MonoBehaviour
{   
    [SerializeField] public Button leftButton;
    [SerializeField] public Button rightButton;
    [SerializeField] public Button jumpButton;
    [SerializeField] public Button attackButton;

    private PlayerInputHandler playerInputHandler;

    private void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();

        // Add listeners for the button press and release events
        leftButton.gameObject.AddComponent<EventTrigger>().triggers = CreateTriggerList(() => Move(-1), () => Move(0));
        rightButton.gameObject.AddComponent<EventTrigger>().triggers = CreateTriggerList(() => Move(1), () => Move(0));
        //jumpButton.gameObject.AddComponent<EventTrigger>().triggers = CreateTriggerList(Jump, () => playerInputHandler.SetJump(false));
        jumpButton.gameObject.AddComponent<EventTrigger>().triggers = CreateTriggerList(() => playerInputHandler.SetJump(true), () => playerInputHandler.SetJump(false));
        attackButton.gameObject.AddComponent<EventTrigger>().triggers = CreateTriggerList(Attack, () => playerInputHandler.SetPrimaryAttack(false));
    }

    private List<EventTrigger.Entry> CreateTriggerList(UnityEngine.Events.UnityAction pointerDown, UnityEngine.Events.UnityAction pointerUp)
    {
        List<EventTrigger.Entry> entryList = new List<EventTrigger.Entry>();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((eventData) => { pointerDown(); });
        entryList.Add(pointerDownEntry);

        if (pointerUp != null)
        {
            EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
            pointerUpEntry.eventID = EventTriggerType.PointerUp;
            pointerUpEntry.callback.AddListener((eventData) => { pointerUp(); });
            entryList.Add(pointerUpEntry);
        }

        return entryList;
    }


    private void Move(int direction)
    {
        // Set the movement input in the PlayerInputHandler
        playerInputHandler.SetMoveInput(new Vector2(direction, 0));
    }

    private void Jump()
    {
        // Set the jump input in the PlayerInputHandler
        playerInputHandler.SetJump(true);
    }

    private void Attack()
    {
        // Set the attack input in the PlayerInputHandler
        playerInputHandler.SetPrimaryAttack(true);
    }
}