using TMPro;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public VisitorDataUI visitorDataUI;
    public GameObject[] visitorPrefabs; // Visitor GameObjects
    public GameObject spawnPoint; // Position to spawn visitors
    private GameObject currentVisitor;
    private Visitor currentVisitorData;
    private int score;
    private float remainingTime;

    public float gameDuration = 300f; // Total game duration
    public AuthenticationController authenticationController; // Reference to the AuthenticationController

    public static event Action OnGameOver;

    private void OnEnable()
    {
        OnGameOver += EndGame;
        MyMessageListener.OnHC_SR04DataReceived += HandleDistanceData;
        AuthenticationController.OnAuthenticationComplete += HandleAuthenticationResult;
    }

    private void OnDisable()
    {
        OnGameOver -= EndGame;
        MyMessageListener.OnHC_SR04DataReceived -= HandleDistanceData;
        AuthenticationController.OnAuthenticationComplete -= HandleAuthenticationResult;
    }

    void Start()
    {
        remainingTime = gameDuration;
        SpawnRandomVisitor();
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            OnGameOver?.Invoke();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void EndGame()
    {
        visitorDataUI.gameObject.SetActive(false);
        Debug.Log("The game has ended.");
        Debug.Log($"Final score: {score}");
    }

    void HandleDistanceData(bool detected, float distance)
    {
        if (detected && distance >= 5f && distance <= 15f)
        {
            Debug.Log($"Distance within range: {distance}");
            ShowVisitorData();
        }
        else
        {
            visitorDataUI.gameObject.SetActive(false);
        }
    }

    void HandleAuthenticationResult(bool isAuthenticated, bool isSuspicious)
    {
        if (isAuthenticated)
        {
            if (isSuspicious)
            {
                Debug.Log("Suspicious visitor authenticated. LED turned on.");
                MyMessageListener.Instance.SetLEDState(true);
                score--; // Penalty
            }
            else
            {
                Debug.Log("Regular visitor authenticated.");
                MyMessageListener.Instance.SetLEDState(false);
                score++; // Increment
            }
        }
        else
        {
            Debug.Log("Authentication failed.");
        }

        // Cycle to the next visitor
        visitorDataUI.gameObject.SetActive(false);
        SpawnRandomVisitor();
    }

    void ShowVisitorData()
    {
        visitorDataUI.gameObject.SetActive(true);
        visitorDataUI.UpdateVisitorData(currentVisitorData);
    }

    void SpawnRandomVisitor()
    {
        if (visitorPrefabs.Length == 0)
        {
            Debug.LogError("No visitor prefabs defined.");
            return;
        }

        // Destroy the current visitor if one exists
        if (currentVisitor != null)
        {
            Destroy(currentVisitor);
        }

        // Choose a random visitor prefab
        int randomIndex = UnityEngine.Random.Range(0, visitorPrefabs.Length);
        currentVisitor = Instantiate(visitorPrefabs[randomIndex], spawnPoint.transform.position, Quaternion.identity);

        // Set visitor data (assuming each prefab has a Visitor component)
        currentVisitorData = currentVisitor.GetComponent<Visitor>();
        Debug.Log($"New visitor: {currentVisitorData.FullName}");

        // Set the current visitor in the AuthenticationController
        authenticationController.SetCurrentVisitor(currentVisitorData);

        // Generate a new target number for authentication
        authenticationController.GenerateTargetNumber();
    }
}