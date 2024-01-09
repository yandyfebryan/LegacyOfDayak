using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }

    public bool[] AttackInputs { get; private set; }

    private bool inDialogue = false; // Flag to check if in dialogue

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];

        cam = Camera.main;
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (inDialogue) return; // Ignore input if in dialogue

        AttackInputs[(int)CombatInputs.primary] = context.ReadValueAsButton();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (inDialogue) return; // Ignore input if in dialogue

        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y); 
    }

    public void SetInDialogue(bool isInDialogue)
    {
        inDialogue = isInDialogue;

        if (inDialogue)
        {
            ResetInputs();
        }
    }

    private void ResetInputs()
    {
        RawMovementInput = Vector2.zero;
        NormInputX = 0;
        NormInputY = 0;
        // Reset other relevant inputs as needed
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (inDialogue) return; // Ignore input if in dialogue

        JumpInput = context.ReadValueAsButton();
        JumpInputStop = context.canceled;
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (inDialogue) return; // Ignore input if in dialogue

        GrabInput = context.ReadValueAsButton();
    }

    public void UseJumpInput() => JumpInput = false;

    //Touch Input
    public void SetPrimaryAttack(bool isAttacking)
    {
        if (inDialogue) return; // Ignore input if in dialogue

        AttackInputs[(int)CombatInputs.primary] = isAttacking;
    }

    public void SetJump(bool isJumping)
    {
        if (inDialogue) return; // Ignore input if in dialogue

        JumpInput = isJumping;
    }

    public void SetMoveInput(Vector2 direction)
    {
        if (inDialogue) return; // Ignore input if in dialogue

        RawMovementInput = direction;
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
}

public enum CombatInputs
{
    primary
}
