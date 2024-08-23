using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Particle;

    public void OnCollisionEnter(Collision collision)
    {
     
        Health healthComponent = collision.gameObject.GetComponent<Health>();


        if (healthComponent != null)
        {

            healthComponent.Damage(10);
            if(Particle != null)
            {
                
               GameObject blood = Instantiate(Particle, transform.position,Quaternion.identity);
                Destroy(blood, 3);
            }

           
        }
        Destroy(this.gameObject);
    }
}
