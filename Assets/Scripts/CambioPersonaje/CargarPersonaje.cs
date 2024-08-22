using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarPersonaje : MonoBehaviour
{
    // Referencias a los personajes de ambos jugadores
    public GameObject cuboPersonajeJugador1;
    public GameObject esferaPersonajeJugador1;
    public GameObject cilindroPersonajeJugador1;

    public GameObject cuboPersonajeJugador2;
    public GameObject esferaPersonajeJugador2;
    public GameObject cilindroPersonajeJugador2;

    private void Start()
    {
        // Obtener las selecciones guardadas para el Jugador 1
        int cuboJugador1 = PlayerPrefs.GetInt("cuboSelectJugador1");
        int esferaJugador1 = PlayerPrefs.GetInt("esferaSelectJugador1");
        int cilindroJugador1 = PlayerPrefs.GetInt("cilindroSelectJugador1");

        // Activar el personaje seleccionado para el Jugador 1
        if (cuboJugador1 == 1)
        {
            Instantiate(cuboPersonajeJugador1);
            cuboPersonajeJugador1.SetActive(true);
        }
        else if (esferaJugador1 == 1)
        {
            Instantiate(esferaPersonajeJugador1);
            esferaPersonajeJugador1.SetActive(true);
        }
        else if (cilindroJugador1 == 1)
        {
            Instantiate(cilindroPersonajeJugador1);
            cilindroPersonajeJugador1.SetActive(true);
        }

        // Obtener las selecciones guardadas para el Jugador 2
        int cuboJugador2 = PlayerPrefs.GetInt("cuboSelectJugador2");
        int esferaJugador2 = PlayerPrefs.GetInt("esferaSelectJugador2");
        int cilindroJugador2 = PlayerPrefs.GetInt("cilindroSelectJugador2");

        // Activar el personaje seleccionado para el Jugador 2
        if (cuboJugador2 == 1)
        {
            Instantiate(cuboPersonajeJugador2);
            cuboPersonajeJugador2.SetActive(true);
        }
        else if (esferaJugador2 == 1)
        {
            Instantiate(esferaPersonajeJugador2);
            esferaPersonajeJugador2.SetActive(true);
        }
        else if (cilindroJugador2 == 1)
        {
            Instantiate(cilindroPersonajeJugador2);
            cilindroPersonajeJugador2.SetActive(true);
        }
    }
}