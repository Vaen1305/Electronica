using System;
using System.IO.Ports;
using UnityEngine;

public class ArduinoInputHandler : MonoBehaviour
{
    public static ArduinoInputHandler Instance { get; private set; }

    private SerialPort arduinoPort;

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

    void Start()
    {
        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports)
        {
            try
            {
                arduinoPort = new SerialPort("COM3", 9600);
                arduinoPort.Open();
                // Intentar handshake con el Arduino
                arduinoPort.WriteLine("Unity");

                string response = arduinoPort.ReadLine().Trim(); // Leer respuesta
                Debug.Log(response);
                if (response == "Arduino")
                {
                    Debug.Log($"Conectado a Arduino en el puerto: {port}");
                    return; // Salir del bucle si se conecta correctamente
                }

                arduinoPort.Close(); // Cierra el puerto si no responde como esperado
                arduinoPort.ReadTimeout = 500;
            }
            catch (TimeoutException)
            {
                Debug.Log($"No se pudo conectar a {port}: Timeout.");
            }
            catch (Exception e)
            {
                Debug.Log($"No se pudo conectar a {port}: {e.Message}");
            }
        }

        Debug.Log("No se pudo conectar al Arduino.");

        // Suscripción a eventos con depuración
        OnMPUDataReceived += (roll, pitch, yaw) =>
            Debug.Log($"MPU6050 Data: Roll={roll}, Pitch={pitch}, Yaw={yaw}");

        OnHC_SR04DataReceived += (detected, distance) =>
            Debug.Log($"HC-SR04 Data: Detected={detected}, Distance={distance} cm");

        OnButtonStateChanged += (button, state) =>
            Debug.Log($"Button {button}: State={state}");

        OnPotentiometerChanged += (value) =>
            Debug.Log($"Potentiometer Value: {value}");
    }

    void Update()
    {
        if (arduinoPort != null && arduinoPort.IsOpen)
        {
            try
            {
                string message = arduinoPort.ReadLine();
                Debug.Log("Mensaje recibido: " + message);
                ProcessArduinoMessage(message);

            }
            catch (TimeoutException)
            {
                // Ignorar errores de tiempo de espera
            }
            catch (Exception e)
            {
                Debug.LogError("Error al leer desde Arduino: " + e.Message);
            }
        }
    }

    private void ProcessArduinoMessage(string message)
    {
        if (message.StartsWith("SR04:"))
        {
            // Procesar datos del HC-SR04
            string[] parts = message.Substring(5).Split(',');
            bool detected = parts[0] == "true";
            float distance = float.Parse(parts[1]);
            OnHC_SR04DataReceived?.Invoke(detected, distance);
        }
        else if (message.StartsWith("A:") || message.StartsWith("B:"))
        {
            // Procesar datos de pulsadores
            string button = message.Substring(0, 1);
            bool state = message.EndsWith("true");
            OnButtonStateChanged?.Invoke(button, state);
        }
        else if (message.StartsWith("Pot:"))
        {
            // Procesar datos del potenciómetro
            int value = int.Parse(message.Substring(4));
            OnPotentiometerChanged?.Invoke(value);
        }
        else if (message.Contains(","))
        {
            // Procesar datos del MPU6050
            string[] parts = message.Split(',');
            float roll = float.Parse(parts[0]);
            float pitch = float.Parse(parts[1]);
            float yaw = float.Parse(parts[2]);
            OnMPUDataReceived?.Invoke(roll, pitch, yaw);
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
        if (arduinoPort != null && arduinoPort.IsOpen)
        {
            arduinoPort.WriteLine(message);
            Debug.Log("Enviado a Arduino: " + message);
        }
        else
        {
            Debug.LogError("No hay conexión con el Arduino para enviar el número.");
        }
    }

    public void SetLEDState(bool state)
    {
        if (arduinoPort != null && arduinoPort.IsOpen)
        {
            string command = state ? "LED:true" : "LED:false";
            arduinoPort.WriteLine(command);
            Debug.Log("Enviado a Arduino: " + command);
        }
        else
        {
            Debug.LogError("No hay conexión con el Arduino para controlar el LED.");
        }
    }

    private void OnApplicationQuit()
    {
        if (arduinoPort != null && arduinoPort.IsOpen)
        {
            arduinoPort.Close();
            Debug.Log("Puerto cerrado.");
        }
    }
}
