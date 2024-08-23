using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class ShootController : MonoBehaviour
{
    public GunsPlayers Gun;
    public float launchSpeed = 50.0f;
    public GameObject bullet;
    public AnimationCurve speedCurve = AnimationCurve.Linear(0, 1, 1, 1);
    public TextMeshProUGUI bulletCountText; 
    public TextMeshProUGUI reloadTimerText;
    private int bulletCount = 10;
    private float reloadTime = 3.0f; 
    private float reloadTimer = 0.0f;
    private bool isReloading = false;

    private void Awake()
    {
        launchSpeed = Gun.launchSpeed;
        bulletCount = Gun.bulletAcount;
        reloadTime = Gun.reloadTime;

    }
    private void Update()
    {
        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                isReloading = false;
                reloadTimer = 0;
                bulletCount = Gun.bulletAcount; 
            }
            return; 
        }

        UpdateUI();

    }
    void SpawnBullet()
    {
        if (bulletCount <= 0)
        {
            StartReload();
            return;
        }

        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = Quaternion.identity;

        Vector3 localDirection = transform.TransformDirection(Vector3.forward);

        float modifiedLaunchSpeed = launchSpeed * speedCurve.Evaluate(Time.timeSinceLevelLoad % speedCurve[speedCurve.length - 1].time);
        Vector3 velocity = localDirection * modifiedLaunchSpeed;

        GameObject spawnedBullet = Instantiate(bullet, spawnPosition, spawnRotation);
        Rigidbody rigidbody = spawnedBullet.GetComponent<Rigidbody>();
        rigidbody.velocity = velocity;

        bulletCount--;
     
    }
    void UpdateUI()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = bulletCount + "/" + Gun.bulletAcount;
        }

        if (reloadTimerText != null)
        {
            if (isReloading)
            {
                reloadTimerText.text = "Reloading..." ;
            }
            else
            {
                reloadTimerText.text = "";
            }
        }
    }
    void StartReload()
    {
        isReloading = true;
        reloadTimer = reloadTime;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SpawnBullet();
            
        }
        UpdateUI();
    }

}

