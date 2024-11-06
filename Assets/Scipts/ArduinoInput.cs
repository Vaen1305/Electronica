using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Este método simularia el escaneo de una tarjeta RFID y cómo activaría el evento en Unity
    public void OnCardScanned(string name, string lastName, string role, bool isResident)
    {
        // Aquí simulariamos que el jugador escanea una tarjeta
        gameManager.ScanCard(name, lastName, role, isResident);
    }
}
