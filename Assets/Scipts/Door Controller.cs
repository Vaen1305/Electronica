using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public void OpenDoor()
    {
        Debug.Log("La puerta est� abierta, el visitante puede entrar.");
        // Aqu� iria el c�digo para controlar el hardware de Arduino para abrir la puerta
    }

    // M�todo para cerrar la puerta si la persona no vive all�
    public void CloseDoor()
    {
        Debug.Log("La puerta se cierra.");
        // Aqu� iria el c�digo para controlar el hardware de Arduino para cerrar la puerta
    }
}
