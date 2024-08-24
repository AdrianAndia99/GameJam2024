using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagazie : MonoBehaviour
{
    public TypeWeapon type;
    public int CountMagazine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            WeaponManager _WeaponManager = other.gameObject.GetComponent<WeaponManager>();
            Destroy(this.gameObject);
        }

    }
}
