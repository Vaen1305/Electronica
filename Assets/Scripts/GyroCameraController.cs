using UnityEngine;

public class GyroCameraController : MonoBehaviour
{
    // Rotación inicial de la cámara en Y
    private float initialYaw;

    // Valor actual de Yaw del giroscopio
    private float yaw;

    [SerializeField] private Camera cam;

    // Límite de rotación en grados
    [SerializeField] private float MinYaw = -40f;
    [SerializeField] private float MaxYaw = 40f;

    private void OnEnable()
    {
        // Suscribirse al evento que recibe los datos del giroscopio
        MyMessageListener.OnMPUDataReceived += OnGyroDataReceived;
    }
    private void OnDisable()
    {
        MyMessageListener.OnMPUDataReceived -= OnGyroDataReceived;
    }

    void Start()
    {
        // Guardamos la rotación inicial en el eje Y de la cámara
        initialYaw = cam.transform.eulerAngles.y;
    }

    void OnGyroDataReceived(float roll, float pitch, float newYaw)
    {
        // Solo nos interesa la rotación en Y (Yaw)
        yaw = Mathf.Clamp(newYaw, MinYaw, MaxYaw);
    }

    void Update()
    {
        // Actualizamos solo la rotación en Y de la cámara, restringiendo el rango
        float clampedYaw = Mathf.Clamp(initialYaw + yaw, initialYaw + MinYaw, initialYaw + MaxYaw);
        cam.transform.rotation = Quaternion.Euler(0, clampedYaw, 0);
    }
}
