using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public GameObject funciones;
    public void cambiar()
    {
        // Asegura que las selecciones se guarden antes de cambiar de escena
        funciones.GetComponent<GuardarPersonaje>().Guardar();
        // Cambiar a la escena de juego
        SceneManager.LoadScene("Juego");
    }
}
