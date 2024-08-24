using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> listaWeapon = new List<Weapon>();
    public Weapon CurrentWeapon;
    public bool Fire;
    public bool reloadRequested;
    // Start is called before the first frame update
    void Start()
    {
        ActivateWeapon(TypeWeapon.Rifle);
    }
    void ActivateWeapon(TypeWeapon type)
    {
        foreach (var item in listaWeapon)
        {
            if (item.type == type)
            {
                item.gameObject.SetActive(true);
                CurrentWeapon = item;
            }
                
            else
                item.gameObject.SetActive(false);
        }
    }
    Weapon GetWeaponActive()
    {
        foreach (var item in listaWeapon)
        {
            if (item.gameObject.activeSelf)
                return item;
        }
        return null;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Fire)
        {
            if (CurrentWeapon != null)
                CurrentWeapon.Fire();
        }
        if (reloadRequested)
        {
            if (CurrentWeapon != null)
                CurrentWeapon.ReloadFire();
            reloadRequested = false; // Reset reloadRequested after handling
        }

    }
    public void AddMAgazine(ItemMagazie item)
    { 
        if(CurrentWeapon!=null)
        {
            if (CurrentWeapon.type == item.type)
            {
                CurrentWeapon.AddMagazine(item.CountMagazine);
            }

        }
    
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            Fire = true;
        else
            if (context.canceled)
            Fire = false;


    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            reloadRequested = true;
        }


    }
    
}
