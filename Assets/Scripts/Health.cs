using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public int healtAgent;
    public int healtAgentMax;
    public bool active=false;
    public bool IsDead { get => (healtAgent <= 0); }
    public virtual void Damage(int damage)
    {
      
        healtAgent = Mathf.Clamp(healtAgent - damage, 0, healtAgentMax);
    
    }
    public virtual void LoadComponent()
    {

        healtAgent = healtAgentMax;
    }
    public virtual void Death()
    {
        active = true;
    }
}
