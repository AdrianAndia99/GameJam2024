using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIA : Health
{
    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.tag == ("Bullet"))
        {
            Damage(10);  
            Destroy(collision.gameObject);
        }
    }
}
