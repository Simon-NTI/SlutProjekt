using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI equipedWeaponLabel;
    [SerializeField] GameObject gunRay;
    GameObject camera;
    [SerializeField] GameObject debugSpherePrefab;
    GameObject debugSphere;
    [SerializeField] bool debugMode = false;
    [SerializeField] float debugSphereRadius = 0.2f;
    [SerializeField] ParticleSystem impactParticles;
    Weapon weapon;
    void Awake()
    {
        camera = gameObject.GetComponentInChildren<Camera>().gameObject;
        if(debugMode)
        {
            debugSphere = Instantiate(debugSpherePrefab);
        }
        weapon = gameObject.AddComponent<Weapon>();
        weapon.SetValues(Weapon.pistol);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFireLogic();
        weapon.DecrementCooldown();
    }

    private void PlayerFireLogic()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        
        if(Input.GetMouseButton(0))
        {
            Physics.Raycast(
                camera.transform.position,
                camera.transform.forward,
                out RaycastHit hit
            );
            
            if(weapon.Fire(hit))
            {
                LineRenderer line = Instantiate(gunRay).GetComponent<LineRenderer>();
                line.SetPosition(0, camera.transform.position);
                line.SetPosition(1, camera.transform.position + 500 * camera.transform.forward);

                if(hit.collider != null)
                {
                    if(debugMode)
                    {
                        debugSphere.transform.position = hit.point;
                    }
                    // Instantiate a particle system where the player's shot impacts and point
                    // it in the same direction as the normal of the surface hit
                    Instantiate(impactParticles, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
    }

    private void EquipWeapon(WeaponMetadata weaponMetadata)
    {
        weapon.SetValues(weaponMetadata);
        equipedWeaponLabel.text = "Current Weapon: " + weapon.name.Replace("_", " ");
    }
}