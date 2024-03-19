using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public int Damage { get; set; }
    public float fireDelay, currentFireCooldown, recoil, damageFalloffPerUnit;
    public bool automaticFire;

    public void SetInitialValues(int damage, float fireDelay, float recoil, float damageFalloffPerUnit, bool automaticFire)
    {
        Damage = damage;
        this.fireDelay = fireDelay;
        currentFireCooldown = fireDelay;
        this.recoil = recoil;
        this.damageFalloffPerUnit = damageFalloffPerUnit;
        this.automaticFire = automaticFire;
    }

    public void DecrementCooldown()
    {
        currentFireCooldown -= Time.deltaTime;
    }

    public void Fire(RaycastHit hit)
    {
        if(currentFireCooldown < fireDelay)
        {
            gameObject.SendMessage("IncreaseRecoilDebt", recoil, SendMessageOptions.DontRequireReceiver);
            currentFireCooldown += fireDelay;
            
            try
            {
                print("Distance to hit: " + hit.distance);
            }
            catch
            {}

            if(hit.collider != null && hit.collider.CompareTag("enemy"))
            {

                hit.collider.transform.gameObject.SendMessage("RecieveDamage", Damage);
            }
        }
    }
}
