using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    public void ActivateAlarm()
    {
        Debug.Log("�Alarma Activada! Persona sospechosa detectada.");
        // Aqu� iria el c�digo para controlar el hardware de Arduino para activar la alarma.
    }

    // M�todo para desactivar la alarma
    public void DeactivateAlarm()
    {
        Debug.Log("Alarma Desactivada.");
        // Aqu� iria el c�digo para controlar el hardware de Arduino para desactivar la alarma.

    }
}
