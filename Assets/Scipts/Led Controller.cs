using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedController : MonoBehaviour
{
    public void TurnOnGreenLED()
    {
        Debug.Log("LED Verde: Vecino Aproximándose");
        // Aqui iri el codigo para controlar el hardware de Arduino
    }
    public void TurnOnRedLED()
    {
        Debug.Log("LED Rojo: Persona No Vive Aquí");
        // Aqui iri el codigo para controlar el hardware de Arduino
    }
    public void TurnOffLED()
    {
        Debug.Log("LED Apagado");
        // Aqui iri el codigo para controlar el hardware de Arduino
    }
}
