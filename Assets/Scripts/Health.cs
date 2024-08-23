using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public int healtAgent;        // Salud actual del agente
    public int healtAgentMax;    // Salud máxima del agente
    public bool active = false;  // Estado de actividad

    
    public bool IsDead { get => (healtAgent <= 0); }

  
    public virtual void Damage(int damage)
    {
        if (active) return;  

       
        healtAgent = Mathf.Clamp(healtAgent - damage, 0, healtAgentMax);

        
        if (IsDead)
        {
            Death(); 
        }
    }

    
    public virtual void LoadComponent()
    {
        healtAgent = healtAgentMax; 
    }

    
    public virtual void Death()
    {
        active = true;
        SceneManager.LoadScene("Level2");
    }
}
