using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayController : MonoBehaviour
{
    public TMP_Text displayText; 

    public void ShowVisitorInfo(string name, string lastName, string role)
    {
        displayText.text = $"Nombre: {name}\nApellido: {lastName}\nRol: {role}";
        Debug.Log("Mostrando información del visitante");
    }

    public void ShowResidentStatus(bool isResident)
    {
        if (isResident)
        {
            displayText.text += "\nEstado: Vive aquí";
        }
        else
        {
            displayText.text += "\nEstado: No vive aquí";
        }
    }

    public void ShowError()
    {
        displayText.text = "¡Alerta! Esta persona no vive aquí";
    }
}
