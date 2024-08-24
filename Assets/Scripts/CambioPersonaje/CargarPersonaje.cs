using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
[System.Serializable]
public class CanvasData
{
    public Image live;
    public TMP_Text waveText;
    public TMP_Text enemiesRemainingText;
    public TMP_Text waitWave;
    public TMP_Text CountMagazing;
    public TMP_Text CountBullet;
    public CanvasData()
    { 
    
    }
    public void AddDataLive(HealthPlayer player)
    {
        player.LiveUI = live;


    }
}
public class CargarPersonaje : MonoBehaviour
{
    public static CargarPersonaje Instance { get; private set; }
    // Referencias a los personajes de ambos jugadores
    public GameObject cuboPersonajeJugador1;
    public GameObject esferaPersonajeJugador1;
    public GameObject cilindroPersonajeJugador1;

    public GameObject cuboPersonajeJugador2;
    public GameObject esferaPersonajeJugador2;
    public GameObject cilindroPersonajeJugador2;

    public Transform ParentObject;
    Transform SpawnPlayer1;
    Transform SpawnPlayer2;

    [Header("Canvas Data")]
    public CanvasData CanvasDataPlayer1 = new CanvasData();
    public CanvasData CanvasDataPlayer2 = new CanvasData();

    [SerializeField] private List<Transform> spawnPoints; // Lista de transformaciones para las posiciones
    public List<int> availableIndexes; // Índices disponibles para seleccionar


    private void Start()
    {

        // Inicializa la lista de índices disponibles
        availableIndexes = new List<int>();
        InitializeAvailableIndexes();
        SpawnPlayer1 = GetNextSpawnPoint();
        SpawnPlayer2 = GetNextSpawnPoint();
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

        player1GO = Instantiate(player1Prefab, SpawnPlayer1.position, SpawnPlayer1.rotation);
        player1GO.transform.parent = ParentObject;
        player1GO.SetActive(true);
        

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

        Player2GO = Instantiate(Player2Prefab, SpawnPlayer2.position, SpawnPlayer2.rotation);
        Player2GO.transform.parent = ParentObject;
        Player2GO.SetActive(true);

        HealthPlayer player1 = player1GO.GetComponent<HealthPlayer>();
        WeaponManager player1WeaponManager = player1GO.GetComponent<WeaponManager>();
        if (player1!=null)
        {
            CanvasDataPlayer1.AddDataLive(player1);
            player1._CargarPersonaje = this;
            if (player1WeaponManager != null)
                player1WeaponManager.CurrentWeapon.AddTextUI(CanvasDataPlayer1.CountBullet, CanvasDataPlayer1.CountMagazing);
        }

        HealthPlayer player2 = Player2GO.GetComponent<HealthPlayer>();
        WeaponManager player2WeaponManager = Player2GO.GetComponent<WeaponManager>();
        if (player2 != null)
        {
            CanvasDataPlayer2.AddDataLive(player2);
            player2._CargarPersonaje = this;

            if (player2WeaponManager != null)
                player2WeaponManager.CurrentWeapon.AddTextUI(CanvasDataPlayer2.CountBullet, CanvasDataPlayer2.CountMagazing);
        }



    }
    private void InitializeAvailableIndexes()
    {
        availableIndexes = new List<int>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            availableIndexes.Add(i);
        }
    }
    public  Transform GetNextSpawnPoint()
    {
        if (availableIndexes.Count == 0)
        {
            // Recarga los índices disponibles si no hay más puntos
            InitializeAvailableIndexes();
        }

        // Selecciona un índice aleatorio de los disponibles
        int randomIndex = Random.Range(0, availableIndexes.Count);
        int selectedIndex = availableIndexes[randomIndex];

        // Obtiene el transform correspondiente al índice seleccionado
        Transform spawnPoint = spawnPoints[selectedIndex];

        // Elimina el índice de la lista para evitar repeticiones
        availableIndexes.RemoveAt(randomIndex);

        return spawnPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var item in spawnPoints)
        {
            Gizmos.DrawWireSphere(item.position, 1f);
        }
        
         
    }
}