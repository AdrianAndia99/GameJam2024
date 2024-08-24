using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HealthPlayer : Health
{
    PlayerMovement _PlayerMovement;
    PlayerInput playerinput;
    Collider[] colliders ;
    Rigidbody by;
    public CargarPersonaje _CargarPersonaje;

    void Start()
    {
        LoadComponent();
    }


    public override void LoadComponent()
    {
        base.LoadComponent();
        _PlayerMovement = GetComponent<PlayerMovement>();
        playerinput = GetComponent<PlayerInput>();
        by = GetComponent<Rigidbody>();
        colliders = GetComponents<Collider>();
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
        Desactivar();
        base.Death();
        StartCoroutine(Reactiver(5));
    }

    private void Desactivar()
    {
        _PlayerMovement.enabled = false;
        playerinput.enabled = false;
        foreach (var item in colliders)
        {
            item.enabled = false;
        }
        by.isKinematic = true;
        Debug.Log("Desactivar ");
    }
    IEnumerator Reactiver(float time)
    {
        Debug.Log("Reactiver ");
        yield return new WaitForSeconds(time);
        Activar();
    }
    private void Activar()
    {
        Transform spanw = _CargarPersonaje.GetNextSpawnPoint();
        Debug.Log("Activar ");
        transform.position = spanw.position;
        transform.rotation = spanw.rotation;

        _PlayerMovement.enabled = true;
        playerinput.enabled = true;
        foreach (var item in colliders)
        {
            item.enabled = true;
        }
        by.isKinematic = false;
        active = false;
        base.reactivarHealth(); 

    }
}
