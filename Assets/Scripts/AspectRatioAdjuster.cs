using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AspectRatioAdjuster : MonoBehaviour
{
    // The RectTransform to adjust
    public RectTransform targetRect;

    // The amount by which to adjust the position X if the aspect ratio isn't 16:9
    public float adjustmentValue = -100f;

    // The desired aspect ratio (e.g., 16:9)
    private const float TARGET_ASPECT_RATIO = 16f / 9f;

    // The initial position X of the RectTransform
    private float initialPosX;

    // The new Input System action
    private InputAction checkResolutionAction;

    private void Awake()
    {
        checkResolutionAction = new InputAction("CheckResolution", InputActionType.Button, "<Keyboard>/r");
        checkResolutionAction.performed += ctx => OnResolutionChanged();
    }

    private void Start()
    {
        // Get the initial position X
        initialPosX = targetRect.anchoredPosition.x;

        // Check the aspect ratio and adjust if necessary
        AdjustPositionBasedOnAspectRatio();
    }

    private void OnEnable()
    {
        checkResolutionAction.Enable();
    }

    private void OnDisable()
    {
        checkResolutionAction.Disable();
    }

    private void AdjustPositionBasedOnAspectRatio()
    {
        // Get the current screen's aspect ratio
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        // Check if the current aspect ratio is different from the desired one
        if (Mathf.Abs(currentAspectRatio - TARGET_ASPECT_RATIO) > 0.01f)
        {
            // Adjust the Pos X of the RectTransform by the defined adjustmentValue
            Vector2 adjustedPosition = targetRect.anchoredPosition;
            adjustedPosition.x = initialPosX + adjustmentValue;
            targetRect.anchoredPosition = adjustedPosition;
        }
        else
        {
            // Set to the initial position X
            Vector2 originalPosition = targetRect.anchoredPosition;
            originalPosition.x = initialPosX;
            targetRect.anchoredPosition = originalPosition;
        }
    }

    private void OnResolutionChanged()
    {
        AdjustPositionBasedOnAspectRatio();
    }
}
