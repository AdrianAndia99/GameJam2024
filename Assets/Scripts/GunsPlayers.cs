using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGunData", menuName = "ScriptableObjects/GunData", order = 1)]
public class GunsPlayers : ScriptableObject
{
    public int bulletAcount;
    public float reloadTime;
    public float launchSpeed;
}
