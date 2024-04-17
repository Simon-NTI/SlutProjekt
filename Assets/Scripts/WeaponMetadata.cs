using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMetadata : MonoBehaviour
{
    public string name;
    public int damage, price;
    public float fireDelay, recoil;
    public bool automaticFire;
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
