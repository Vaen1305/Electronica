using UnityEngine;
using System;

public class MyMessageListener : MonoBehaviour
{
    public static MyMessageListener Instance { get; private set; }

    private SerialController serialController;

    public static event Action<float, float, float> OnMPUDataReceived;
    public static event Action<bool, float> OnHC_SR04DataReceived;
    public static event Action<string, bool> OnButtonStateChanged;
    public static event Action<int> OnPotentiometerChanged;

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

    public void SendNumberToDisplay(int number)
    {
        if (number < 0 || number > 9999)
        {
            Debug.LogError("El número debe estar entre 0 y 9999.");
            return;
        }

        string message = "TM1637:" + number;
        serialController.SendSerialMessage(message);
        Debug.Log("Enviado a Arduino: " + message);
        
    }

    public void SetLEDState(bool state)
    {
        string command = state ? "LED:true" : "LED:false";
        serialController.SendSerialMessage(command);
        Debug.Log("Enviado a Arduino: " + command);
    }

    void OnMessageArrived(string msg)
    {
        Debug.Log("Arrived: " + msg);

        if (msg.StartsWith("SR04:"))
        {
            // Procesar datos del HC-SR04
            string[] parts = msg.Substring(5).Split(',');
            bool detected = parts[0] == "true";
            float distance = float.Parse(parts[1]);
            OnHC_SR04DataReceived?.Invoke(detected, distance);
        }
        else if (msg.StartsWith("A:") || msg.StartsWith("B:"))
        {
            // Procesar datos de pulsadores
            string button = msg.Substring(0, 1);
            bool state = msg.EndsWith("true");
            OnButtonStateChanged?.Invoke(button, state);
        }
        if (msg.StartsWith("Pot:"))
        {
            int value = int.Parse(msg.Substring(4));
            Debug.Log($"Valor del potenciómetro: {value}");
            OnPotentiometerChanged?.Invoke(value);
        }
        else if (msg.StartsWith("Gyro:"))
        {
            // Procesar datos del giroscopio (roll, pitch, yaw)
            string[] parts = msg.Substring(5).Split(',');
            float roll = float.Parse(parts[0]);
            float pitch = float.Parse(parts[1]);
            float yaw = float.Parse(parts[2]);
            OnMPUDataReceived?.Invoke(roll, pitch, yaw);
        }
    }
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }


}