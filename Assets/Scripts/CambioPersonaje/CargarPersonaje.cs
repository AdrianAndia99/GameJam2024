using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CargarPersonaje : MonoBehaviour
{
    // Referencias a los personajes de ambos jugadores
    public GameObject cuboPersonajeJugador1;
    public GameObject esferaPersonajeJugador1;
    public GameObject cilindroPersonajeJugador1;

    public GameObject cuboPersonajeJugador2;
    public GameObject esferaPersonajeJugador2;
    public GameObject cilindroPersonajeJugador2;

    public Transform ParentObject;
    public Transform SpawnPlayer1;
    public Transform SpawnPlayer2;

    private void Start()
    {
        // Inicializa todos los personajes como inactivos al inicio
        /*cuboPersonajeJugador1.SetActive(false);
        esferaPersonajeJugador1.SetActive(false);
        cilindroPersonajeJugador1.SetActive(false);

        cuboPersonajeJugador2.SetActive(false);
        esferaPersonajeJugador2.SetActive(false);
        cilindroPersonajeJugador2.SetActive(false);*/

        // Obtener las selecciones guardadas para el Jugador 1
        int cuboJugador1 = PlayerPrefs.GetInt("cuboSelectJugador1");
        int esferaJugador1 = PlayerPrefs.GetInt("esferaSelectJugador1");
        int cilindroJugador1 = PlayerPrefs.GetInt("cilindroSelectJugador1");

        GameObject player1GO;
        GameObject player1Prefab = null;

        // Activar el personaje seleccionado para el Jugador 1
        if (cuboJugador1 == 1)
        {
            player1Prefab = cuboPersonajeJugador1;
        }
        else if (esferaJugador1 == 1)
        {
            player1Prefab = esferaPersonajeJugador1;
        }
        else if (cilindroJugador1 == 1)
        {
            player1Prefab = cilindroPersonajeJugador1;
        }

        player1GO = Instantiate(player1Prefab, parent: ParentObject);
        player1GO.SetActive(true);
        player1GO.transform.position = SpawnPlayer1.position; 

        // Obtener las selecciones guardadas para el Jugador 2
        int cuboJugador2 = PlayerPrefs.GetInt("cuboSelectJugador2");
        int esferaJugador2 = PlayerPrefs.GetInt("esferaSelectJugador2");
        int cilindroJugador2 = PlayerPrefs.GetInt("cilindroSelectJugador2");

        GameObject Player2GO;
        GameObject Player2Prefab = null;
        // Activar el personaje seleccionado para el Jugador 2
        if (cuboJugador2 == 1)
        {
            Player2Prefab = cuboPersonajeJugador2;
        }
        else if (esferaJugador2 == 1)
        {
            Player2Prefab = esferaPersonajeJugador2;
        }
        else if (cilindroJugador2 == 1)
        {
            Player2Prefab = cilindroPersonajeJugador2;
        }
        Player2GO = Instantiate(Player2Prefab, parent: ParentObject);
        Player2GO.SetActive(true);
        Player2GO.transform.position = SpawnPlayer2.position;
    }
}