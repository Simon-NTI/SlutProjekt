using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private int damage;
    public float fireDelay, currentFireCooldown, recoil;
    private bool automaticFire;
    private bool releasedSinceLastFire = true;
    public void SetValues(int damage, float fireDelay, float recoil, bool automaticFire)
    {
        this.damage = damage;
        this.fireDelay = fireDelay;
        currentFireCooldown = fireDelay;
        this.recoil = recoil;
        this.automaticFire = automaticFire;
    }

    public void SetValues(WeaponMetadata weaponMetadata)
    {
        damage = weaponMetadata.damage;
        fireDelay = weaponMetadata.fireDelay;
        currentFireCooldown = fireDelay;
        recoil = weaponMetadata.recoil;
        automaticFire = weaponMetadata.automaticFire;
    }

    public static WeaponMetadata[] weapons = new WeaponMetadata[4]
    {
        new("Pistol", 3, 100, 0.65f, 10, false, true),
        new("SMG", 2, 150, 6, 0.3f, true, false),
        new("Sniper", 5, 350, 20, 1f, false, false),
        new("Assault_Rifle", 4, 600, 10, 0.5f, true, false)
    };
    public static WeaponMetadata pistol => weapons[0];
    public static WeaponMetadata smg => weapons[1];
    public static WeaponMetadata sniper => weapons[2];
    public static WeaponMetadata assaultRifle => weapons[3];

    public void Update()
    {
        releasedSinceLastFire = !Input.GetMouseButton(0);
    }

    public void DecrementCooldown()
    {
        currentFireCooldown += Time.deltaTime;
        currentFireCooldown = Mathf.Clamp(currentFireCooldown, 0, fireDelay);
    }

    public bool Fire(RaycastHit hit)
    {
        if(currentFireCooldown >= fireDelay && (releasedSinceLastFire || automaticFire))
        {
            gameObject.SendMessage("IncreaseRecoilDebt", recoil, SendMessageOptions.DontRequireReceiver);
            currentFireCooldown -= fireDelay;

            if(hit.collider != null && hit.collider.CompareTag("enemy"))
            {
                int calculatedDamage = damage;
                hit.collider.transform.gameObject.SendMessage("RecieveDamage", calculatedDamage);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
