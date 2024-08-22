using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarPersonaje : MonoBehaviour
{
    public void SeleccionarPersonajeJugador1(string personaje)
    {
        switch (personaje)
        {
            case "cubo":
                PlayerPrefs.SetInt("cuboSelectJugador1", 1);
                PlayerPrefs.SetInt("esferaSelectJugador1", 0);
                PlayerPrefs.SetInt("cilindroSelectJugador1", 0);
                break;
            case "esfera":
                PlayerPrefs.SetInt("cuboSelectJugador1", 0);
                PlayerPrefs.SetInt("esferaSelectJugador1", 1);
                PlayerPrefs.SetInt("cilindroSelectJugador1", 0);
                break;
            case "cilindro":
                PlayerPrefs.SetInt("cuboSelectJugador1", 0);
                PlayerPrefs.SetInt("esferaSelectJugador1", 0);
                PlayerPrefs.SetInt("cilindroSelectJugador1", 1);
                break;
        }
        PlayerPrefs.Save(); // Asegura que los cambios se guarden inmediatamente
    }

    public void SeleccionarPersonajeJugador2(string personaje)
    {
        switch (personaje)
        {
            case "cubo":
                PlayerPrefs.SetInt("cuboSelectJugador2", 1);
                PlayerPrefs.SetInt("esferaSelectJugador2", 0);
                PlayerPrefs.SetInt("cilindroSelectJugador2", 0);
                break;
            case "esfera":
                PlayerPrefs.SetInt("cuboSelectJugador2", 0);
                PlayerPrefs.SetInt("esferaSelectJugador2", 1);
                PlayerPrefs.SetInt("cilindroSelectJugador2", 0);
                break;
            case "cilindro":
                PlayerPrefs.SetInt("cuboSelectJugador2", 0);
                PlayerPrefs.SetInt("esferaSelectJugador2", 0);
                PlayerPrefs.SetInt("cilindroSelectJugador2", 1);
                break;
        }
        PlayerPrefs.Save(); // Asegura que los cambios se guarden inmediatamente
    }

    public void Guardar()
    {
        // Guardar todas las selecciones (ya está manejado por las funciones de selección, pero puedes añadir lógica adicional aquí si es necesario)
        PlayerPrefs.Save(); // Asegura que los cambios se guarden inmediatamente
    }
}
