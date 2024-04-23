using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMetadata : MonoBehaviour
{
    public readonly string name;
    public readonly int damage, price;
    public readonly float fireDelay, recoil;
    public readonly bool automaticFire;
    public bool bought;

    public WeaponMetadata(string name, int damage, int price, float fireDelay, float recoil, bool automaticFire, bool bought)
    {
        this.name = name;
        this.damage = damage;
        this.price = price;
        this.fireDelay = fireDelay;
        this.recoil = recoil;
        this.automaticFire = automaticFire;
        this.bought = bought;
    }
}
