using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public static ButtonController Instance { get; private set; }

    private bool canPressButton = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        MyMessageListener.OnButtonStateChanged += HandleButtonPress;
    }

    private void OnDisable()
    {
        MyMessageListener.OnButtonStateChanged -= HandleButtonPress;
    }

    // Enable button presses only after potentiometer check
    public void EnableButtonPress(bool enable)
    {
        canPressButton = enable;
    }

    void HandleButtonPress(string button, bool isPressed)
    {
        if (!isPressed || !canPressButton) return; // Act only if button is pressed and allowed

        switch (button)
        {
            case "A":
                Debug.Log("Button A pressed: Grant entry.");
                AuthenticationController.TriggerAuthenticationComplete(true, false);
                break;
            case "B":
                Debug.Log("Button B pressed: Deny entry.");
                AuthenticationController.TriggerAuthenticationComplete(false, false);
                break;
            default:
                Debug.LogError($"Unknown button: {button}");
                break;
        }

        // Disable button presses after authentication attempt
        canPressButton = false;
    }
}
