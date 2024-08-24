using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum TypeWeapon { Rifle, LaunchGranade}
public class Weapon : MonoBehaviour
{
    protected float FrameRate=0;
    public float Rate;
    public TypeWeapon type;
    public Transform PointShoot;
    public GameObject prefabBullet;
    public float impulse;
    public int CountBullet;
    public int magazine;
    public int magazineMax;
    public ParticleSystem MuzzleFlash;
    public TMP_Text CountBullet_Text;
    public TMP_Text CountMagazine_Text;
    // Start is called before the first frame update
    public virtual void LoadComponent()
    {
         
         magazine = magazineMax;
         CountBullet=magazine;
    }
    public void AddTextUI(TMP_Text CountBullet_, TMP_Text CountMagazine_)
    {
        magazine = magazineMax;
        CountBullet = magazine;

        if (CountBullet_ != null)
            CountBullet_Text = CountBullet_;

        if (CountMagazine_ != null)
            CountMagazine_Text= CountMagazine_;

        UpdateTextUI();
    }
    public void UpdateTextUI()
    {
        if(CountBullet_Text!=null)
            CountBullet_Text.text = "Count Bullet: "+ CountBullet.ToString();

        if (CountMagazine_Text != null)
            CountMagazine_Text.text = "Count Magazine: " + magazine.ToString();
    }
    public bool CantShoot()
    {
        return CountBullet > 0;
    }
    public virtual void Fire()
    {
       
    }
    public virtual void Shoot()
    {
        if (MuzzleFlash != null)
            MuzzleFlash.Play();
        UpdateTextUI();
    }
    public virtual void StopFire()
    {
        FrameRate = 0;
        if (MuzzleFlash != null)
            MuzzleFlash.Stop();
    }
    public virtual void ReloadFire()
    {
        if(magazine>0)
        {
            magazine = Mathf.Clamp(magazine - 1, 0, magazineMax);
            CountBullet = magazineMax;
            UpdateTextUI();
        }
        
    }
    public virtual void AddMagazine(int count)
    {
        magazine += count;
        UpdateTextUI();
    }
}
