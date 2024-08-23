using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : Health
{
    
    void Start()
    {
        LoadComponent();
    }

    
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void Damage(int damage)
    {
        if (active) return;
        base.Damage(damage);
        if (IsDead)
        {
            Death();
        }
    }
    public override void Death()
    {
        base.Death();
    }
}
