using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] GameObject gunRay;
    GameObject camera;
    Vector3? debugSphereCenter;
    [SerializeField] GameObject debugSpherePrefab;
    GameObject debugSphere;
    [SerializeField] bool debugMode, drawPlayerRayImpactPoint, drawPlayerRay;
    [SerializeField] float debugSphereRadius = 0.2f;
    [SerializeField] float damage;
    Weapon currentWeapon;
    void Awake()
    {
        camera = gameObject.GetComponentInChildren<Camera>().gameObject;
        if(debugMode)
        {
            debugSphere = Instantiate(debugSpherePrefab);
        }
        currentWeapon = gameObject.AddComponent<Weapon>();
        currentWeapon.SetInitialValues(1, 0.2f, 5, 10, true);
    }

    // Update is called once per frame
    void Update()
    {
        ProducePlayerRay();
        PlayerFireLogic();
        currentWeapon.DecrementCooldown();
    }

    private void PlayerFireLogic()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(
                camera.transform.position,
                camera.transform.forward,
                out RaycastHit hit
            );
            currentWeapon.Fire(hit);

            LineRenderer line = Instantiate(gunRay).GetComponent<LineRenderer>();
            line.SetPosition(0, camera.transform.position);

            if(hit.collider == null)
            {
                line.SetPosition(1, camera.transform.position + 500 * camera.transform.forward);
            }
            else
            {
                debugSphereCenter = hit.point;
                line.SetPosition(1, hit.point);
            }
        }
    }

    private void ProducePlayerRay()
    {
        if (debugMode && drawPlayerRay)
        {
            Debug.DrawLine(camera.transform.position, camera.transform.position + 500 * camera.transform.forward);
        }

        else
        {
            debugSphereCenter = null;
        }

        if (debugSphereCenter != null)
        {
            if (debugMode && drawPlayerRayImpactPoint)
            {
                debugSphere.transform.position = (Vector3)debugSphereCenter;
            }
        }
    }
}