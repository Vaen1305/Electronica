using UnityEngine;
using System;

public class AuthenticationController : MonoBehaviour
{
    public static event Action<bool, bool> OnAuthenticationComplete;
    private int targetNumber;
    private Visitor currentVisitor;

    private void OnEnable()
    {
        MyMessageListener.OnPotentiometerChanged += CheckPotentiometerValue;
    }

    private void OnDisable()
    {
        MyMessageListener.OnPotentiometerChanged -= CheckPotentiometerValue;
    }

    void Start()
    {
        GenerateTargetNumber();
    }

    public void SetCurrentVisitor(Visitor visitor)
    {
        currentVisitor = visitor;
    }

    // Generate the target number
    public void GenerateTargetNumber()
    {
        targetNumber = UnityEngine.Random.Range(0, 1024);
        Debug.Log($"New target number: {targetNumber}");
        MyMessageListener.Instance.SendNumberToDisplay(targetNumber);
    }

    // Step 1: Check Potentiometer value after it's been set
    void CheckPotentiometerValue(int value)
    {
        if (value == targetNumber)
        {
            Debug.Log("Potentiometer value matched!");
            if (currentVisitor != null)
            {
                // Step 2: After matching, trigger the next step for button press
                ButtonController.Instance.EnableButtonPress(true);
            }
            else
            {
                Debug.LogError("Current visitor is not set!");
                OnAuthenticationComplete?.Invoke(true, false);
            }
        }
    }

    public static void TriggerAuthenticationComplete(bool isAuthenticated, bool isSuspicious)
    {
        OnAuthenticationComplete?.Invoke(isAuthenticated, isSuspicious);
    }
}