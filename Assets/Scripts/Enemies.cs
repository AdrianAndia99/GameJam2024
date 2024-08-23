using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemiesData", menuName = "ScriptableObjects/EnemiesData", order = 1)]
public class Enemies : ScriptableObject
{
    public float damage;
    public float Rotationspeed;
    public float Life;
}
