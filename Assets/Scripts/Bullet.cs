using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeBulletDamage { Player, Enemy}
public class Bullet : MonoBehaviour
{
    public GameObject Particle;
    public TypeBulletDamage typeBulletDamage;
    private void Start()
    {
        Destroy(this.gameObject,5);
    }
    private void OnTriggerEnter(Collider other)
    {

        Health healthComponent = other.gameObject.GetComponent<Health>();
        if (healthComponent != null)
        {
     
            if (typeBulletDamage == TypeBulletDamage.Enemy && healthComponent is HealthIA)
            {

                ((HealthIA)healthComponent).Damage(10);
            }
            else
            if (typeBulletDamage == TypeBulletDamage.Player && healthComponent is HealthPlayer)
            {
            
                ((HealthPlayer)healthComponent).Damage(10);

            }

            if (Particle != null)
            {
                GameObject blood = Instantiate(Particle, transform.position, Quaternion.identity);
                Destroy(blood, 3);
            }
        }
        Destroy(this.gameObject);
    }
     

}
