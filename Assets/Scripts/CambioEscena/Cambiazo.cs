using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiazo : MonoBehaviour
{
    public void cambiazo(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
