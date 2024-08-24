using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        LoadComponent();
        
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void Fire()
    {
        
            if (FrameRate > Rate)
            {
                if (CantShoot())
                {
                    Shoot();
                }
                FrameRate = 0;
            }
            FrameRate += Time.deltaTime;
       
        
    }
    public override void Shoot()
    {
        base.Shoot();
        if (prefabBullet != null && PointShoot != null)
        {
            // Instanciar la bala
            GameObject bulletInst = Instantiate(prefabBullet, PointShoot.position, PointShoot.rotation);
            Bullet bullet = bulletInst.GetComponent<Bullet>();
            bullet.typeBulletDamage = TypeBulletDamage.Enemy;
            Rigidbody bulletRb = bulletInst.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                // Dirigir la bala hacia el jugador
                
                bulletRb.velocity = PointShoot.forward * impulse;
                CountBullet--;
            }
        }
    }
    public override void StopFire()
    {

    }
    public override void ReloadFire()
    {
        base.ReloadFire();
    }
}
