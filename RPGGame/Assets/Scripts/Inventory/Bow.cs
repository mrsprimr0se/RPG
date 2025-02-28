using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private Weaponinfo weaponInfo;

    public void Attack()
    {
        Debug.Log("Bow Attack");
    }

    public Weaponinfo GetWeaponInfo()
    {
        return weaponInfo;
    }


}
