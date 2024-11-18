using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText; // Texto para mostrar el tiempo restante
    public float gameDuration = 300f; // 5 minutos en segundos
    private float remainingTime;

    public static event Action OnGameOver;


    public GameObject visitorData;

    public Visitor[] visitors; // Lista de visitantes predefinidos
    private Visitor currentVisitor;

    private void OnEnable()
    {
        OnGameOver += EndGame;
        MyMessageListener.OnHC_SR04DataReceived += HandleDistanceData;
    }
    private void OnDisable()
    {
        OnGameOver -= EndGame;
        MyMessageListener.OnHC_SR04DataReceived -= HandleDistanceData;
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
        OnGameOver?.Invoke();
        Debug.Log("El juego ha terminado.");
    }

    void HandleDistanceData(bool detected, float distance)
    {
        if (detected && distance >= 5f && distance <= 15f)
        {
            Debug.Log($"Distancia dentro del umbral: {distance}");
            ShowVisitorData();
        }
        else
        {
            Debug.Log($"Distancia fuera del umbral: {distance}");
        }
    }
    void ShowVisitorData()
    {
        visitorData.SetActive(true);
    }

    void SpawnRandomVisitor()
    {
        if (visitors.Length == 0)
        {
            Debug.LogError("No hay visitantes definidos.");
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, visitors.Length);
        currentVisitor = visitors[randomIndex];
        Debug.Log($"Visitante actual: {currentVisitor}");
        // Aquí puedes mostrar la información del visitante en la UI
    }
}
