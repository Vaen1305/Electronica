using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public void OpenDoor()
    {
        Debug.Log("La puerta está abierta, el visitante puede entrar.");
        // Aquí iria el código para controlar el hardware de Arduino para abrir la puerta
    }

    // Método para cerrar la puerta si la persona no vive allí
    public void CloseDoor()
    {
        Debug.Log("La puerta se cierra.");
        // Aquí iria el código para controlar el hardware de Arduino para cerrar la puerta
    }
}
