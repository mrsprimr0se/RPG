using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "New Weapon")]
public class Weaponinfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public float weaponCooldown;
}
