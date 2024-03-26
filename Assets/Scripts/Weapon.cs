using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private int damage;
    public float fireDelay, currentFireCooldown, recoil, damageFalloffPerUnit;
    private bool automaticFire;
    private bool releasedSinceLastFire = true;
    public void SetValues(int damage, float fireDelay, float recoil, float damageFalloffPerUnit, bool automaticFire)
    {
        this.damage = damage;
        this.fireDelay = fireDelay;
        currentFireCooldown = fireDelay;
        this.recoil = recoil;
        this.damageFalloffPerUnit = damageFalloffPerUnit;
        this.automaticFire = automaticFire;
    }

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
