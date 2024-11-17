using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayController : MonoBehaviour
{
    public TMP_Text displayText;
    public int verifyCode;
    public void GenerateVerifyCode() 
    { 
        verifyCode = Random.Range(1, 1024);
        displayText.text = "Código de Verificación: " + verifyCode;
    }
}
