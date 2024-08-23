using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
     
        Health healthComponent = collision.gameObject.GetComponent<Health>();


        if (healthComponent != null)
        {

            healthComponent.Damage(10);


           
        }
        Destroy(this.gameObject);
    }
}
