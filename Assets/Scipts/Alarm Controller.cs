using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    public void ActivateAlarm()
    {
        Debug.Log("¡Alarma Activada! Persona sospechosa detectada.");
        // Aquí iria el código para controlar el hardware de Arduino para activar la alarma.
    }

    // Método para desactivar la alarma
    public void DeactivateAlarm()
    {
        Debug.Log("Alarma Desactivada.");
        // Aquí iria el código para controlar el hardware de Arduino para desactivar la alarma.

    }
}
