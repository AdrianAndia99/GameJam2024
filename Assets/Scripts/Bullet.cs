using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object" || collision.gameObject.tag == "Enemies" || collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

}
