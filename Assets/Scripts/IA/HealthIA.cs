using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIA : Health
{
    // Start is called before the first frame update
    void Start()
    {
        LoadComponent();
    }

    // Update is called once per frame
   
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
