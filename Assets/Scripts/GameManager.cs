using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] visitors;

    /*
    public DisplayController displayController;
    public LedController ledController;
    public DoorController doorController;
    public AlarmController alarmController;

    public event Action<string, string, string, bool> OnCardScanned;

    private void Start()
    {
        OnCardScanned += HandleCardScan;
    }

    // Método que se llama cuando se escanea una tarjeta RFID
    private void HandleCardScan(string name, string lastName, string role, bool isResident)
    {
        displayController.ShowVisitorInfo(name, lastName, role);
        displayController.ShowResidentStatus(isResident);

        if (isResident)
        {
            ledController.TurnOnGreenLED();
            doorController.OpenDoor();
        }
        else
        {
            ledController.TurnOnRedLED();
            alarmController.ActivateAlarm();
        }
    }

    // Método para simular que pasamos la tarjeta
    public void ScanCard(string name, string lastName, string role, bool isResident)
    {
        OnCardScanned?.Invoke(name, lastName, role, isResident);
    }
    */
}
