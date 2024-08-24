using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public int healtAgent;        // Salud actual del agente
    public int healtAgentMax;    // Salud máxima del agente
    public bool active = false;  // Estado de actividad
    public Image LiveUI;
    public Transform AimOffSet;
    public bool IsDead { get => (healtAgent <= 0); }

  
    public virtual void Damage(int damage)
    {

       

        healtAgent = Mathf.Clamp(healtAgent - damage, 0, healtAgentMax);

        UpdateBarLive();



    }

    void UpdateBarLive()
    {
        if (LiveUI != null)
        {
            float amountLive = (float)((float)healtAgent / (float)healtAgentMax);
            LiveUI.fillAmount = amountLive;
        }

    }
    public virtual void LoadComponent()
    {
        healtAgent = healtAgentMax;
        UpdateBarLive();
    }

    public virtual void reactivarHealth()
    {
        healtAgent = healtAgentMax;
        UpdateBarLive();
    }
    public virtual void Death()
    {
        active = true;
        
    }
}
